using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Pogs.WinUI.Utilities
{
    internal class UserSettings
    {
        public const string LastDatabaseKey = "lastDatabaseName";
        public const string LastServerKey = "lastServerName";

        private static XElement _settingsElement;
        private static FileInfo _settingsFile;

        static UserSettings()
        {
            _settingsFile = new FileInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                @"Pogs\UserSettings.xml"));

            if (_settingsFile.Exists)
            {
                try
                {
                    _settingsElement = XElement.Load(_settingsFile.FullName);
                }
                catch (Exception ex)
                {
                    LogUtility.Log(String.Format("Could not load settings file. ({0})", ex.Message));
                    TryLoadDefaultSettings();
                }
            }
            else
            {
                TryLoadDefaultSettings();
            }

            if (_settingsElement == null)
                _settingsElement = new XElement("UserSettings");
        }

        private static void TryLoadDefaultSettings()
        {
            var defaultFile = new FileInfo(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath),
                   @"DefaultUserSettings.xml"));
            try
            {
                _settingsElement = XElement.Load(defaultFile.FullName);
            }
            catch (Exception ex)
            {
                LogUtility.Log(String.Format("Could not load default settings file. ({0})", ex.Message));
            }
        }

        /// <summary>
        /// Gets an XML element in the settings file that can be modified for storing complex settings.
        /// </summary>
        /// <param name="settingName"></param>
        /// <returns></returns>
        public static XElement GetOrCreateSettingElement(XName settingName)
        {
            var result = _settingsElement.Element(settingName);

            if (result == null)
            {
                result = new XElement(settingName);
                _settingsElement.Add(result);
            }

            return result;
        }

        internal static bool HasSetting(string p)
        {
            return _settingsElement.Attribute(p) != null;
        }

        /// <summary>
        /// Gets a simple named string setting from the settings file.
        /// </summary>
        /// <param name="settingName"></param>
        /// <returns></returns>
        public static string GetStringSetting(string settingName)
        {
            var attribute = _settingsElement.Attribute(settingName);
            if (attribute != null)
                return attribute.Value;

            return String.Empty;
        }

        /// <summary>
        /// Sets a simple named string setting in the settings file, and saves the changes.
        /// </summary>
        /// <param name="settingName"></param>
        /// <param name="value"></param>
        public static void SetStringSetting(string settingName, string value)
        {
            _settingsElement.SetAttributeValue(settingName, value);
            SaveSettings();
        }

        /// <summary>
        /// Gets a simple integral setting from the settings file.
        /// </summary>
        /// <param name="settingName"></param>
        /// <returns></returns>
        public static int GetIntSetting(string settingName)
        {
            int result;
            Int32.TryParse(GetStringSetting(settingName), out result);
            return result;
        }

        /// <summary>
        /// Sets a simple integral setting in the settings file, and saves the changes.
        /// </summary>
        /// <param name="settingName"></param>
        /// <param name="value"></param>
        public static void SetIntSetting(string settingName, int value)
        {
            SetStringSetting(settingName, value.ToString());
            SaveSettings();
        }

        /// <summary>
        /// Gets a simple boolean setting from the settings file.
        /// </summary>
        /// <param name="settingName"></param>
        /// <returns></returns>
        public static bool GetBooleanSetting(string settingName)
        {
            bool result;
            Boolean.TryParse(GetStringSetting(settingName), out result);
            return result;
        }

        /// <summary>
        /// Saves a boolean setting in the settings file, and saves the changes.
        /// </summary>
        /// <param name="settingName"></param>
        /// <param name="value"></param>
        public static void SetBooleanSetting(string settingName, bool value)
        {
            SetStringSetting(settingName, value.ToString());
            SaveSettings();
        }

        /// <summary>
        /// Commits and changes made to any custom settings elements to disk.
        /// </summary>
        public static void SaveSettings()
        {
            try
            {
                if (!_settingsFile.Directory.Exists)
                    _settingsFile.Directory.Create();

                _settingsElement.Save(_settingsFile.FullName, SaveOptions.None);
            }
            catch (Exception ex)
            {
                LogUtility.Log(String.Format("Could not save settings file. ({0})", ex.Message));
            }
        }
    }
}