using UnityEngine;

namespace GTAlpha
{
    [CreateAssetMenu(fileName = "New Constant", menuName = "Game Constant", order = 0)]
    public class Constant : GlobalScriptableObject
    {
        #region Static Fields
        
        private static Constant _main;
        
        #endregion

        #region Const Fields


        #endregion

        #region Static Properties
        
        public static int AnimationState { get; } = Animator.StringToHash("State");
        public static int AnimationFront { get; } = Animator.StringToHash("Front");
        public static int AnimationRight { get; } = Animator.StringToHash("Right");
        public static int AnimationUp { get; } = Animator.StringToHash("Up");
        public static int AnimationForm { get; } = Animator.StringToHash("Form");
        public static int AnimationKey { get; } = Animator.StringToHash("Key");
        public static int AnimationAttack { get; } = Animator.StringToHash("Attack");

        public static int NormalState => _main.normalState;
        public static int HitState => _main.hitState;
        public static int DieState => _main.dieState;
        public static int AttackState => _main.attackState;

        public static float MaxRotationDelta => _main.maxRotationDelta;
        public static float MovementInertiaDeltaOnGround => _main.movementInertiaDeltaOnGround;
        public static float MovementInertiaDeltaOnAir => _main.movementInertiaDeltaOnAir;
        public static float JumpAcceleration => _main.jumpAcceleration;
        public static float GroundCriteria => _main.groundCriteria;

        public static int AttackPerfectMinTimeMs => _main.attackPerfectMinTimeMs;
        public static int AttackPerfectMaxTimeMs => _main.attackPerfectMaxTimeMs;
        public static int AttackGoodMinTimeMs => _main.attackGoodMinTimeMs;
        public static int AttackGoodMaxTimeMs => _main.attackGoodMaxTimeMs;
        public static int AttackBadMinTimeMs => _main.attackBadMinTimeMs;
        public static int AttackBadMaxTimeMs => _main.attackBadMaxTimeMs;
        public static int AttackMissMinTimeMs => _main.attackMissMinTimeMs;
        public static int AttackMissMaxTimeMs => _main.attackMissMaxTimeMs;
        public static float AttackPerfectGradient => _main.attackPerfectGradient;
        public static float AttackGoodGradient => _main.attackGoodGradient;
        public static float AttackBadGradient => _main.attackBadGradient;
        public static float AttackMissGradient => _main.attackMissGradient;

        #endregion

        #region Serialize Fields

        [SerializeField] private int normalState = 0;
        [SerializeField] private int hitState = 1;
        [SerializeField] private int dieState = 2;
        [SerializeField] private int attackState = 3;

        [SerializeField] private float maxRotationDelta = 720.0f;
        [SerializeField] private float movementInertiaDeltaOnGround = 5.0f;
        [SerializeField] private float movementInertiaDeltaOnAir = 1.0f;
        [SerializeField] private float jumpAcceleration = 30.0f;
        [SerializeField] private float groundCriteria = 1.0f;

        [SerializeField] private int attackPerfectMinTimeMs = 17;
        [SerializeField] private int attackPerfectMaxTimeMs = 75;
        [SerializeField] private int attackGoodMinTimeMs = 30;
        [SerializeField] private int attackGoodMaxTimeMs = 100;
        [SerializeField] private int attackBadMinTimeMs = 45;
        [SerializeField] private int attackBadMaxTimeMs = 135;
        [SerializeField] private int attackMissMinTimeMs = 60;
        [SerializeField] private int attackMissMaxTimeMs = 180;
        [SerializeField] private float attackPerfectGradient = 1.01f;
        [SerializeField] private float attackGoodGradient = 1.011f;
        [SerializeField] private float attackBadGradient = 1.012f;
        [SerializeField] private float attackMissGradient = 1.013f;

        #endregion

        #region Public Functions

        public override void Load()
        {
            _main = this;
        }
        
        #endregion
    }
}