using System.Configuration;
using System.Linq;

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
    }
}
