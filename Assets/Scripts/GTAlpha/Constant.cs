using UnityEngine;

namespace GTAlpha
{
    [CreateAssetMenu(fileName = "New Constant", menuName = "Game Constant", order = 0)]
    public class Constant : GlobalScriptableObject
    {
        #region Private Static Fields
        
        private static Constant _main;
        
        #endregion

        #region Public Static Properties
        
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

        public static int PlayerVitality => _main.playerVitality;
        public static int PlayerEndurance => _main.playerEndurance;
        public static int PlayerStrength => _main.playerStrength;
        public static int PlayerResistance => _main.playerResistance;
        
        #endregion

        #region Serialize Fields

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

        [SerializeField] private int playerVitality = 10;
        [SerializeField] private int playerEndurance = 10;
        [SerializeField] private int playerStrength = 10;
        [SerializeField] private int playerResistance = 10;

        #endregion

        #region Public Methods

        public override void Load()
        {
            _main = this;
        }
        
        #endregion
    }
}