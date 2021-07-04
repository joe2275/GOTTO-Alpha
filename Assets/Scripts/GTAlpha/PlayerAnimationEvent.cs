using UnityEngine;

namespace GTAlpha
{
    public class PlayerAnimationEvent : MonoBehaviour
    {
        #region Serialized Fields

        [SerializeField] private ParticleSystem attackMotionParticle;

        #endregion
        
        #region Fields

        private Player mPlayer;

        #endregion

        #region Properties
        
        

        #endregion


        #region Animation Events


        #endregion
        
        #region Private Funcitons

        private void Awake()
        {
            mPlayer = GetComponentInParent<Player>();
        }

        #endregion
    }
}