using System;
using UnityEngine;

namespace Manager
{
    public static class SettingsManager
    {
        
        private static Settings _settings;

        #region Properties

        public static float Sensitivity
        {
            get => _settings.Sensitivity;
            set => _settings.Sensitivity = value;
        }

        #endregion

        public static void Load()
        {
            if (PlayerPrefs.HasKey("Settings"))
            {
                _settings = JsonUtility.FromJson<Settings>(PlayerPrefs.GetString("Settings"));
            }
            else
            {
                _settings = new Settings();
            }
        }

        public static void Save()
        {
            PlayerPrefs.SetString("Settings", JsonUtility.ToJson(_settings));
            PlayerPrefs.Save();
        }
    }

    [Serializable]
    public class Settings
    {
        #region Serialized Fields

        [SerializeField] private float sensitivity = 0.1f;

        #endregion

        #region Properties

        public float Sensitivity
        {
            get => sensitivity;
            set => sensitivity = value;
        }

        #endregion
    }
}