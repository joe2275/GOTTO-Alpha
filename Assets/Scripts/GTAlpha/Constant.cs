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

        public static int AnimationState { get; } = Animator.StringToHash("State");
        public static int AnimationChanged { get; } = Animator.StringToHash("Changed");
        public static int AnimationFront { get; } = Animator.StringToHash("Front");
        public static int AnimationRight { get; } = Animator.StringToHash("Right");
        public static int AnimationUp { get; } = Animator.StringToHash("Up");


        #endregion

        #region Static Properties

        public static float MaxDegreesDelta => _main.maxDegreesDelta;
        public static float MoveVelocityDeltaOnGround => _main.moveVelocityDeltaOnGround;
        public static float MoveVelocityDeltaOnAir => _main.moveVelocityDeltaOnAir;
        public static float JumpAcceleration => _main.jumpAcceleration;
        public static float GroundCriteria => _main.groundCriteria;

        #endregion

        #region Serialize Fields

        [SerializeField] private float maxDegreesDelta = 720.0f;
        [SerializeField] private float moveVelocityDeltaOnGround = 5.0f;
        [SerializeField] private float moveVelocityDeltaOnAir = 1.0f;
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