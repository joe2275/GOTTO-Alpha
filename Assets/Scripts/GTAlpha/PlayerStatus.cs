namespace GTAlpha
{
    public class PlayerStatus : CharacterStatus
    {
        #region Private Static Methods

        /// <summary>
        /// 전달된 2차 능력치를 1차 능력치로 계산해주는 함수
        /// </summary>
        /// <param name="second"></param>
        /// <param name="increase"></param>
        /// <param name="limitation"></param>
        /// <returns></returns>
        private static int ConvertSecondToFirst(int second, float increase, int limitation)
        {
            return -(int) (increase * limitation * limitation / (second + increase * limitation) + limitation);
        }

        /// <summary>
        /// 전달된 최대 생명력을 이용하여 계산된 추가 최대 생명력을 계산하는 함수로 반환된 값은 기존의 최대 생명력에 더해져서 사용된다.  
        /// </summary>
        /// <param name="maxHealthPoint"></param>
        /// <returns></returns>
        private static int GetMaxHealthPointAddition(int maxHealthPoint)
        {
            float limitation = PlayerInfo.MaximumHealthPointTimes * maxHealthPoint;
            return -(int) (PlayerInfo.MaxHealthPointIncrease * limitation * limitation /
                (PlayerData.Possessions + PlayerInfo.MaxHealthPointIncrease * limitation) + limitation);
        }

        #endregion

        #region Public Properties
        /// <summary>
        /// 최종 플레이어 레벨
        /// </summary>
        public override int Level => PlayerData.Level;
        /// <summary>
        /// 최종 플레이어 최대 생명력
        /// </summary>
        public override int MaxHealthPoint
        {
            get
            {
                int maxHealthPoint = ConvertSecondToFirst(PlayerData.Vitality, PlayerInfo.VitalityIncrease,
                    PlayerInfo.VitalityLimitation);
                return maxHealthPoint + GetMaxHealthPointAddition(maxHealthPoint);
            }
        }
        /// <summary>
        /// 최종 플레이어 최대 회피력
        /// </summary>
        public override int MaxStaminaPoint => ConvertSecondToFirst(PlayerData.Endurance, PlayerInfo.EnduranceIncrease,
            PlayerInfo.EnduranceLimitation);
        /// <summary>
        /// 최종 플레이어 공격력
        /// </summary>
        public override int OffensivePower => ConvertSecondToFirst(PlayerData.Strength, PlayerInfo.StrengthIncrease,
            PlayerInfo.StrengthLimitation);
        /// <summary>
        /// 최종 플레이어 방어력
        /// </summary>
        public override int DefensivePower => ConvertSecondToFirst(PlayerData.Resistance, PlayerInfo.ResistanceIncrease,
            PlayerInfo.ResistanceLimitation);
        /// <summary>
        /// 최종 플레이어 이동 속도
        /// </summary>
        public override float MoveSpeed { get; } = 3.0f;
        /// <summary>
        /// 최종 플레이어의 공격 속성
        /// </summary>
        public Element ElementalType { get; }
        /// <summary>
        /// 최종 플레이어의 공격 속성력
        /// </summary>
        public float ElementalPower { get; }
        /// <summary>
        /// 최종 플레이어의 참격 공격력
        /// </summary>
        public int SlashPower { get; }
        /// <summary>
        /// 최종 플레이어의 관통 공격력
        /// </summary>
        public int PenetrationPower { get; }
        /// <summary>
        /// 최종 플레이어의 타격 공격력
        /// </summary>
        public int BlowPower { get; }

        #endregion
    }
}