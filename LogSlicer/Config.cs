using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace LogSlicer
{
    /// <summary>
    /// Provides additional abstraction layer for accessing app.config AppSettings
    /// </summary>
    static class Config
    {
        /// <summary>
        /// Load AppSettings key's value from app.config
        /// </summary>
        /// <param name="key">AppSettings key to look up value to</param>
        /// <param name="encrypted">Whether or not the data in app.config has been encrypted</param>
        /// <returns>Value stored in app.config key</returns>
        public static string Load(string key, bool encrypted = false)
        {
            string results = ConfigurationManager.AppSettings.Get(key);

            if (string.IsNullOrEmpty(results))
            {
                return string.Empty;
            }
            if (encrypted)
            {
                results = Encryption.Decrypt(results, true);
            }
            return results;
        }

        /// <summary>
        /// Saves a value to given key in the app.config file
        /// </summary>
        /// <param name="key">AppSettings key to save to</param>
        /// <param name="value">Value to save to AppSettings key</param>
        /// <param name="encrypted">If true, value will be encrypted before saved to AppSettings key.</param>
        /// <param name="overwrite">If true, previous key value will be deleted.  If false, new value will be added to AppSettings key</param>
        public static void Save(string key, string value, bool encrypted = false, bool overwrite = true)
        {
            if (overwrite)
            {
                Delete(key);
            }
            if (encrypted)
            {
                value = Encryption.Encrypt(value, true);
            }

            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            config.AppSettings.Settings.Add(key, value);
            config.Save(ConfigurationSaveMode.Minimal);
        }

        /// <summary>
        /// Deletes a key from app.config file
        /// </summary>
        /// <param name="key">Name of the AppSettings key to delete</param>
        private static void Delete(string key)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            config.AppSettings.Settings.Remove(key);
            config.Save(ConfigurationSaveMode.Minimal);
        }
    }
}
