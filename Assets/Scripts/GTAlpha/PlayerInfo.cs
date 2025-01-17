﻿using UnityEngine;

namespace GTAlpha
{
    /// <summary>
    /// 플레이어의 저장되고 변하지 않는 정보를 저장하는 클래스
    /// </summary>
    [CreateAssetMenu(fileName = "New Player Information", menuName = "Information/Player", order = 0)]
    public class PlayerInfo : GlobalScriptableObject
    {
        private static PlayerInfo _main;

        #region Static Properties

        public static float MaxExpCoefficient => _main.maxExpCoefficient;
        public static float MaxExpPower => _main.maxExpPower;
        public static float VitalityIncrease => _main.vitalityIncrease;
        public static int VitalityLimitation => _main.vitalityLimitation;
        public static float EnduranceIncrease => _main.enduranceIncrease;
        public static int EnduranceLimitation => _main.enduranceLimitation;
        public static float StrengthIncrease => _main.strengthIncrease;
        public static int StrengthLimitation => _main.strengthLimitation;
        public static float ResistanceIncrease => _main.resistanceIncrease;
        public static int ResistanceLimitation => _main.resistanceLimitation;
        public static float MaximumHealthPointTimes => _main.maximumHealthPointTimes;
        public static float MaxHealthPointIncrease => _main.maxHealthPointIncrease;

        public static int BasePlayerVitality => _main.basePlayerVitality;
        public static int BasePlayerEndurance => _main.basePlayerEndurance;
        public static int BasePlayerStrength => _main.basePlayerStrength;
        public static int BasePlayerResistance => _main.basePlayerResistance;

        #endregion

        #region Serialized Fields

        [SerializeField] private float maxExpCoefficient = 100.0f;
        [SerializeField] private float maxExpPower = 1.5f;
        [SerializeField] private float vitalityIncrease = 0.04f;
        [SerializeField] private int vitalityLimitation = 10000;
        [SerializeField] private float enduranceIncrease = 0.1f;
        [SerializeField] private int enduranceLimitation = 2000;
        [SerializeField] private float strengthIncrease = 0.01f;
        [SerializeField] private int strengthLimitation = 10000;
        [SerializeField] private float resistanceIncrease = 0.01f;
        [SerializeField] private int resistanceLimitation = 10000;
        [SerializeField] private float maximumHealthPointTimes = 4f;
        [SerializeField] private float maxHealthPointIncrease = 1.4f;

        [SerializeField] private int basePlayerVitality = 10;
        [SerializeField] private int basePlayerEndurance = 10;
        [SerializeField] private int basePlayerStrength = 10;
        [SerializeField] private int basePlayerResistance = 10;

        #endregion

        public override void Load()
        {
            _main = this;
        }
    }
}