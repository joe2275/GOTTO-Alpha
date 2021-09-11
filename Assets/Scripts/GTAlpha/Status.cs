using UnityEngine;

namespace GTAlpha
{
    /// <summary>
    /// Actor 클래스에서 사용하는 모든 게임적 수치를 저장하고 관리하는 Status 클래스
    /// </summary>
    public abstract class Status
    {
        #region Private Fields

        private int mHealthPoint;
        private int mStaminaPoint;

        #endregion
        
        #region Public Properties

        /// <summary>
        /// Actor의 최종 레벨
        /// </summary>
        public abstract int Level { get; }

        /// <summary>
        /// Actor의 최종 최대 생명력
        /// </summary>
        public abstract int MaxHealthPoint { get; }

        /// <summary>
        /// Actor의 현재 생명력
        /// </summary>
        public int HealthPoint
        {
            get => mHealthPoint;
            set => mHealthPoint = Mathf.Clamp(value, 0, MaxHealthPoint);
        }

        /// <summary>
        /// Actor의 최종 최대 스태미나
        /// </summary>
        public abstract int MaxStaminaPoint { get; }

        /// <summary>
        /// Actor의 현재 스태미나
        /// </summary>
        public int StaminaPoint
        {
            get => mStaminaPoint;
            set => mStaminaPoint = Mathf.Clamp(value, 0, MaxStaminaPoint);
        }

        /// <summary>
        /// Actor의 최종 공격력
        /// </summary>
        public abstract int OffensivePower { get; }
        /// <summary>
        /// Actor의 최종 방어력
        /// </summary>
        public abstract int DefensivePower { get; }

        #endregion
    }
}