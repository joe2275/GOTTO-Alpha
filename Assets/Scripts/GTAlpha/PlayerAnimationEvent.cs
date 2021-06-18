using System;
using UnityEngine;

namespace GTAlpha
{
    public class PlayerAnimationEvent : MonoBehaviour
    {
        #region Fields

        private Player mPlayer;

        #endregion

        #region Properties

        public bool IsEndOfAnimation { get; set; }

        #endregion


        #region Animation Events

        private void Animation_EndOfAnimation()
        {
            IsEndOfAnimation = true;
        }

        #endregion


        #region Private Funcitons

        private void Awake()
        {
            mPlayer = GetComponentInParent<Player>();
        }

        #endregion
    }
}