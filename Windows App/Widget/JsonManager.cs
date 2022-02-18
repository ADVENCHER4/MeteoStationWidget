using System.IO;
using System;
using System.Text.Json;

namespace Widget
{
    public class JsonManager
    {
        private readonly string FilePath = Path.Combine(Environment.CurrentDirectory, "settings.json");

        public void WriteData(SettingsModel settins)
        {
            var rawString = JsonSerializer.Serialize(settins);
            File.WriteAllText(FilePath, rawString);
        }

        public SettingsModel ReadData()
        {
            var rawString = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<SettingsModel>(rawString);
        }
    }
}