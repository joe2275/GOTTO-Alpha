using UnityEngine;

namespace GTAlpha
{
    [CreateAssetMenu(fileName = "New Constant", menuName = "Constants/Game Constant", order = 0)]
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
        public static int AnimationEvade { get; } = Animator.StringToHash("Evade");

        public static float PlayerMoveSpeed => _main.playerMoveSpeed;
        public static float PlayerEvadeRotationTime => _main.playerEvadeRotationTime;
        public static float MaxRotationDelta => _main.maxRotationDelta;
        public static float MaxRotationDeltaWhileAttack => _main.maxRotationDeltaWhileAttack;
        public static float MovementInertiaDeltaOnGround => _main.movementInertiaDeltaOnGround;
        public static float MovementInertiaDeltaOnAir => _main.movementInertiaDeltaOnAir;
        public static float JumpAcceleration => _main.jumpAcceleration;
        public static float GroundCriteria => _main.groundCriteria;

        #endregion

        #region Serialize Fields

        [SerializeField] private float playerMoveSpeed = 3.0f;
        [SerializeField] private float playerEvadeRotationTime = 0.5f;
        [SerializeField] private float maxRotationDelta = 720.0f;
        [SerializeField] private float maxRotationDeltaWhileAttack = 360.0f;
        [SerializeField] private float movementInertiaDeltaOnGround = 5.0f;
        [SerializeField] private float movementInertiaDeltaOnAir = 1.0f;
        [SerializeField] private float jumpAcceleration = 30.0f;
        [SerializeField] private float groundCriteria = 1.0f;

        #endregion

        #region Public Functions

        public override void Load()
        {
            _main = this;
        }
        
        #endregion
    }
}