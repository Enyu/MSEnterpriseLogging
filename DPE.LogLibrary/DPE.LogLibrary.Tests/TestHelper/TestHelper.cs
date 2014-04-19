using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

namespace DPE.LogLibrary.Tests.TestHelper
{
   public static class TestHelper
    {
        public static void AddKeyValueInAppConfig(string newKey, string newValue)
        {
            if (ConfigurationManager.AppSettings.Cast<string>().Contains(newKey))
            {
                RemoveKeyFromAppConfig(newKey);
            }

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Add(newKey, newValue);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public static void RemoveKeyFromAppConfig(string newKey)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Remove(newKey);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public static string ReadFile(string path)
        {
            if (!File.Exists(path))
            {
                return null;
            }

            var sb = new StringBuilder();
            using (var sr = new StreamReader(path))
            {
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    sb.AppendLine(line);
                }
            }
            return sb.ToString();
        }

        public static void RemoveFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

    }
}
