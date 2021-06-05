using System;
using UnityEngine;

namespace GTAlpha
{
    [Serializable]
    public class SaveData
    {
        #region Core Area

        private static SaveData _current;
        private static int _currentKey = -1;

        public static int CurrentKey
        {
            get => _currentKey;
            set
            {
                if (value < 0)
                {
                    _currentKey = -1;
                    return;
                }

                _currentKey = value;
                string keyString = _currentKey.ToString();
                if (PlayerPrefs.HasKey(keyString))
                {
                    _current = JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString(keyString));
                }
                else
                {
                    _current = new SaveData();
                }
            }
        }

        public static SaveData Get(int key)
        {
            string keyString = key.ToString();
            return PlayerPrefs.HasKey(keyString)
                ? JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString(keyString))
                : null;
        }

        public static bool Delete(int key)
        {
            string keyString = key.ToString();
            if (PlayerPrefs.HasKey(keyString))
            {
                PlayerPrefs.DeleteKey(key.ToString());
                return true;
            }

            return false;
        }

        public static bool Save()
        {
            if (_currentKey < 0)
            {
                return false;
            }

            PlayerPrefs.SetString(_currentKey.ToString(), JsonUtility.ToJson(_current));
            PlayerPrefs.Save();

#if (UNITY_IOS || UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX)
#elif UNITY_ANDROID
#endif
            return true;
        }

        #endregion

        #region Serialized Fields

        [SerializeField] private PlayerData playerData = new PlayerData();
        [SerializeField] private InventoryData inventoryData = new InventoryData();

        #endregion

        public static void Load()
        {
            PlayerData.Current = _current.playerData;
            InventoryData.Current = _current.inventoryData;
        }
    }
}