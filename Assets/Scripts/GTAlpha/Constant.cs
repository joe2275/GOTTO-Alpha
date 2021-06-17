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
        

        #endregion

        #region Static Properties

        public static float MaxDegreesDelta => _main.maxDegreesDelta;
        public static float MoveVelocityDelta => _main.moveVelocityDelta;

        #endregion

        #region Serialize Fields

        [SerializeField] private float maxDegreesDelta = 720.0f;
        [SerializeField] private float moveVelocityDelta = 3.0f;

        #endregion

        #region Public Functions

        public override void Load()
        {
            _main = this;
        }
        
        #endregion
    }
}