using System;
using System.IO.Ports;
using System.Windows;

namespace Widget
{
    public class SerialReader
    {
        private JsonManager _jsonManager = new JsonManager();
        private  SerialPort _port;
        public string Temperature;
        public string Humidity;
        public string Pressure;

        public void OpenPort()
        {
            var settings = _jsonManager.ReadData();
            _port = new SerialPort(settings.Port, 9600);
            if (!_port.IsOpen)
            {
                _port.Open();
            }

            _port.DataReceived += new SerialDataReceivedEventHandler(GetSerialData);
        }

        private void GetSerialData(object sender, SerialDataReceivedEventArgs e)
        {
            ParseData(_port.ReadLine());
        }

        private void ParseData(string data)
        {
            if (string.IsNullOrEmpty(data))
                return;
            string[] parsedData = data.Split(' ');
            Temperature = parsedData[0].Substring(0, parsedData[0].IndexOf("."));
            Humidity = parsedData[1].Substring(0, parsedData[1].IndexOf("."));
            Pressure = parsedData[2].Substring(0, parsedData[2].IndexOf("."));
        }

        public void ClosePort(object sender, EventArgs e)
        {
            if (_port.IsOpen)
            {
                _port.Close();
            }
        }
    }
}