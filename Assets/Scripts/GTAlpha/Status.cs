using UnityEngine;

namespace GTAlpha
{
    public abstract class Status
    {
        #region Private Fields

        private int mHealthPoint;
        private int mStaminaPoint;

        #endregion
        
        #region Public Properties

        public abstract int MaxHealthPoint { get; }

        public int HealthPoint
        {
            get => mHealthPoint;
            set => mHealthPoint = Mathf.Clamp(value, 0, MaxHealthPoint);
        }

        public abstract int MaxStaminaPoint { get; }

        public int StaminaPoint
        {
            get => mStaminaPoint;
            set => mStaminaPoint = Mathf.Clamp(value, 0, MaxStaminaPoint);
        }

        public abstract int OffensivePower { get; }
        public abstract int DefensivePower { get; }
        public abstract float MoveSpeed { get; }

        #endregion
    }
}