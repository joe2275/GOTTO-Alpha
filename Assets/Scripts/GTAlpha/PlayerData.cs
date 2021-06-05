using System;
using System.Collections.Generic;
using UnityEngine;

namespace GTAlpha
{
    [Serializable]
    public class PlayerData
    {
        public static PlayerData Current { get; set; }
        
        #region Serialized Fields

        [SerializeField] private int level = 1;
        [SerializeField] private int exp;

        [SerializeField] private int possessions;

        [SerializeField] private int vitality;
        [SerializeField] private int endurance;
        [SerializeField] private int strength;
        [SerializeField] private int resistance;

        #endregion

        #region Public Static Properties

        public static int Level
        {
            get => Current.level;
            set => Current.level = Mathf.Max(value, 1);
        }

        public static int MaxExp => (int) (PlayerInfo.MaxExpCoefficient * Mathf.Pow(Current.level, PlayerInfo.MaxExpPower));

        public static int Exp
        {
            get => Current.exp;
            set
            {
                int maxExp = MaxExp;
                while (value >= maxExp)
                {
                    value -= maxExp;
                    Current.level++;
                    maxExp = MaxExp;
                }

                Current.exp = value;
            }
        }

        public static int Possessions
        {
            get => Current.possessions;
            set => Current.possessions = Mathf.Max(value, 0);
        }

        public static int Promotion => Current.level - (Current.vitality + Current.endurance + Current.strength + Current.resistance + 1);

        public static int Vitality
        {
            get => Current.vitality;
            set
            {
                int promotion = Promotion;
                int diff = value - Current.vitality;

                if (diff > promotion)
                {
                    return;
                }

                Current.vitality = value;
            }
        }

        public static int Endurance
        {
            get => Current.endurance;
            set
            {
                int promotion = Promotion;
                int diff = value - Current.endurance;

                if (diff > promotion)
                {
                    return;
                }

                Current.endurance = value;
            }
        }

        public static int Strength
        {
            get => Current.strength;
            set
            {
                int promotion = Promotion;
                int diff = value - Current.strength;

                if (diff > promotion)
                {
                    return;
                }

                Current.strength = value;
            }
        }

        public static int Resistance
        {
            get => Current.resistance;
            set
            {
                int promotion = Promotion;
                int diff = value - Current.resistance;

                if (diff > promotion)
                {
                    return;
                }

                Current.resistance = value;
            }
        }


        #endregion
    }
}