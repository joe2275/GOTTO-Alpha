using System;
using UnityEngine;

namespace GTAlpha
{
    [Serializable]
    public class PlayerData
    {
        #region Core Area

        private static PlayerData _current;
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
                    _current = JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString(keyString));
                }
                else
                {
                    _current = new PlayerData();
                }
            }
        }

        public static PlayerData Get(int key)
        {
            string keyString = key.ToString();
            return PlayerPrefs.HasKey(keyString)
                ? JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString(keyString))
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

        #region Public Static Properties

        public static int Level
        {
            get => _current.mLevel;
        }

        public static int MaxExp => (int) (Constant.MaxExpCoefficient * Mathf.Pow(_current.mLevel, Constant.MaxExpPower));

        public static int Exp
        {
            get => _current.mExp;
            set
            {
                int maxExp = MaxExp;
                while (value >= maxExp)
                {
                    value -= maxExp;
                    _current.mLevel++;
                    maxExp = MaxExp;
                }

                _current.mExp = value;
            }
        }

        public static int Property
        {
            get => _current.mProperty;
            set => _current.mProperty = Mathf.Max(value, 0);
        }

        public static int Promotion => _current.mLevel - (_current.mVitality + _current.mEndurance + _current.mStrength + _current.mResistance + 1);

        public static int Vitality
        {
            get => _current.mVitality;
            set
            {
                int promotion = Promotion;
                int diff = value - _current.mVitality;

                if (diff > promotion)
                {
                    return;
                }

                _current.mVitality = value;
            }
        }

        public static int Endurance
        {
            get => _current.mEndurance;
            set
            {
                int promotion = Promotion;
                int diff = value - _current.mEndurance;

                if (diff > promotion)
                {
                    return;
                }

                _current.mEndurance = value;
            }
        }
        
        public static int Strength
        {
            get => _current.mStrength;
            set
            {
                int promotion = Promotion;
                int diff = value - _current.mStrength;

                if (diff > promotion)
                {
                    return;
                }

                _current.mStrength = value;
            }
        }
        
        public static int Resistance
        {
            get => _current.mResistance;
            set
            {
                int promotion = Promotion;
                int diff = value - _current.mResistance;

                if (diff > promotion)
                {
                    return;
                }

                _current.mResistance = value;
            }
        }

        #endregion

        #region Private Fields

        private int mLevel = 1;
        private int mExp;

        private int mProperty;

        private int mVitality;
        private int mEndurance;
        private int mStrength;
        private int mResistance;

        #endregion
    }
}