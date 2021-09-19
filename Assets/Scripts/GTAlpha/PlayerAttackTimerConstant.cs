using System.Collections;
using UnityEngine;

namespace GTAlpha
{
    [CreateAssetMenu(fileName = "New Player Attack Timer Const", menuName = "Constants/Attack Timer Constant", order = 0)]
    public class PlayerAttackTimerConstant : GlobalScriptableObject
    {
        #region Static Fields

        private static PlayerAttackTimerConstant _main;

        #endregion
        
        #region Serialized Fields

        [SerializeField] private int attackRecordTimeMs = 180;

        [SerializeField] private int attackPerfectMinTimeMs = 17;
        [SerializeField] private int attackPerfectMaxTimeMs = 75;
        [SerializeField] private int attackGoodMinTimeMs = 30;
        [SerializeField] private int attackGoodMaxTimeMs = 100;
        [SerializeField] private int attackBadMinTimeMs = 45;
        [SerializeField] private int attackBadMaxTimeMs = 135;
        [SerializeField] private float attackPerfectGradient = 1.01f;
        [SerializeField] private float attackGoodGradient = 1.011f;
        [SerializeField] private float attackBadGradient = 1.012f;

        #endregion

        #region Static Properties

        public static int AttackRecordTimeMs => _main.attackRecordTimeMs;
        public static int AttackPerfectMinTimeMs => _main.attackPerfectMinTimeMs;
        public static int AttackPerfectMaxTimeMs => _main.attackPerfectMaxTimeMs;
        public static int AttackGoodMinTimeMs => _main.attackGoodMinTimeMs;
        public static int AttackGoodMaxTimeMs => _main.attackGoodMaxTimeMs;
        public static int AttackBadMinTimeMs => _main.attackBadMinTimeMs;
        public static int AttackBadMaxTimeMs => _main.attackBadMaxTimeMs;
        public static float AttackPerfectGradient => _main.attackPerfectGradient;
        public static float AttackGoodGradient => _main.attackGoodGradient;
        public static float AttackBadGradient => _main.attackBadGradient;

        #endregion

        public override void Load()
        {
            _main = this;
        }
    }

    public enum AttackAccuracy
    {
        Miss, Bad, Good, Perfect
    }
}