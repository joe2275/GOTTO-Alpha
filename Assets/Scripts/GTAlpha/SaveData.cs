using System;
using UnityEngine;

namespace GTAlpha
{
    /// <summary>
    /// 저장되어야 하는 게임의 모든 플레이어 관련 저장 데이터
    /// </summary>
    [Serializable]
    public class SaveData
    {
        #region Core Area

        private static SaveData _current;
        private static int _currentKey = -1;
        
        /// <summary>
        /// 현재 사용하고 있는 저장 데이터 번호 (0 미만의 경우 어떠한 데이터도 사용하고 있지 않음)
        /// </summary>
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
        
        /// <summary>
        /// 전달된 key를 이용하여 해당하는 저장 데이터를 반환하는 함수
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static SaveData Get(int key)
        {
            string keyString = key.ToString();
            return PlayerPrefs.HasKey(keyString)
                ? JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString(keyString))
                : null;
        }

        /// <summary>
        /// 전달된 key를 이용하여 해당하는 저장 데이터를 삭제하는 함수
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 현재 사용되고 있는 저장 데이터를 저장시키는 함수
        /// </summary>
        /// <returns></returns>
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

        // 저장되는 모든 데이터를 선언한 Serialized Fields
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