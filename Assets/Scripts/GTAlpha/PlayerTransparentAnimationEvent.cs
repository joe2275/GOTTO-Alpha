using System;
using UnityEngine;

namespace GTAlpha
{
    public class PlayerTransparentAnimationEvent : MonoBehaviour
    {
        #region Fields

        private Player mPlayer;
        
        #endregion

        #region Properties

        public bool IsEndOfTransparentMotions { get; set; }

        #endregion

        #region Animation Events

        private void Animation_EndOfMotions()
        {
            IsEndOfTransparentMotions = true;
        }

        #endregion

        #region Private Functions

        private void Awake()
        {
            mPlayer = GetComponentInParent<Player>();
        }

        #endregion
    }
}