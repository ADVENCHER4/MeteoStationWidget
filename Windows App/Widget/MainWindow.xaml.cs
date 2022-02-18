using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;
using System.Diagnostics;


namespace Widget
{
    public partial class MainWindow
    {
        private DispatcherTimer _clockTimer = new DispatcherTimer();
        private DispatcherTimer _sensorsTimer = new DispatcherTimer();
        private DispatcherTimer _windowHideTimer = new DispatcherTimer();
        private SerialReader _reader = new SerialReader();
        private SettingsModel _settings;
        private JsonManager _jsonManager = new JsonManager();
        private bool _canMove;
        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(int hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        public const int HWND_BOTTOM = 0x1;
        public const uint SWP_NOSIZE = 0x1;
        public const uint SWP_NOMOVE = 0x2;
        public const uint SWP_SHOWWINDOW = 0x40;
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr window, int index, int value);

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr window, int index);

        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_TOOLWINDOW = 0x00000080;

        private IntPtr Handle
        {
            get
            {
                return new WindowInteropHelper(this).Handle;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            SetupTimers();
            Loaded += OnLoad;
            Closed += _reader.ClosePort;
            Activated += OnActivated;
        }
        private void OnActivated(object sender, EventArgs e)
        {
            ShoveToBackground();
        }
        private void OnLoad(object sender, RoutedEventArgs e)
        {
            _reader.OpenPort();
            SetupWindowStyle();
            HideFromAltTab(Handle);
        }

        private void SetupWindowStyle()
        {
            _settings = _jsonManager.ReadData();
            Left = _settings.Xpos;
            Top = _settings.Ypos;
            foreach (System.Windows.Controls.Label item in MainGrid.Children)
            {
                item.Foreground = (Brush)System.ComponentModel.TypeDescriptor
                    .GetConverter(typeof(Brush)).ConvertFromInvariantString(_settings.Color);
                item.FontFamily = new FontFamily(_settings.Font);
            }

        }
        private void SetupTimers()
        {
            _clockTimer.Tick += SetTime;
            _clockTimer.Interval = TimeSpan.FromMilliseconds(1000);
            _clockTimer.Start();
            _sensorsTimer.Tick += SetSensorsValues;
            _sensorsTimer.Interval = TimeSpan.FromMilliseconds(2000);
            _sensorsTimer.Start();
        }

        private void SetSensorsValues(object sender, EventArgs e)
        {
            TemperatureLabel.Content = $"{_reader.Temperature}°C";
            HumidityLabel.Content = $"{_reader.Humidity}%";
            PressureLabel.Content = $"{_reader.Pressure} mm";
        }

        private void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            if (_canMove)
                DragMove();
        }

        private void SetTime(object sender, EventArgs e)
        {
            var date = DateTime.Now.ToString("HH:mm");
            TimeLabel.Content = date;
        }

        private void SwitchMovementState(object sender, RoutedEventArgs e)
        {
            if (_canMove)
            {
                _settings.Xpos = Left;
                _settings.Ypos = Top;
                _jsonManager.WriteData(_settings);
            }
            _canMove = !_canMove;
            EnableMovementItem.IsChecked = _canMove;
        }
        public static void HideFromAltTab(IntPtr Handle)
        {
            SetWindowLong(Handle, GWL_EXSTYLE, GetWindowLong(Handle, GWL_EXSTYLE) | WS_EX_TOOLWINDOW);
        }

        private void ShoveToBackground()
        {
            SetWindowPos((int)this.Handle, HWND_BOTTOM, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_SHOWWINDOW);
        }

        private void ApplySettings(object sender, RoutedEventArgs e)
        {
            SetupWindowStyle();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OpenSettings(object sender, RoutedEventArgs e)
        {
            Process.Start("C:\\Windows\\System32\\notepad.exe", Path.Combine(Environment.CurrentDirectory, "settings.json"));
        }
    }
}