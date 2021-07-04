namespace GTAlpha
{
    public class PlayerStatus : CharacterStatus
    {
        #region Private Static Methods

        private static int ConvertSecondToFirst(int second, float increase, int limitation)
        {
            return -(int) (increase * limitation * limitation / (second + increase * limitation) + limitation);
        }

        private static int GetMaxHealthPointAddition(int maxHealthPoint)
        {
            float limitation = PlayerInfo.MaximumHealthPointTimes * maxHealthPoint;
            return -(int) (PlayerInfo.MaxHealthPointIncrease * limitation * limitation /
                (PlayerData.Possessions + PlayerInfo.MaxHealthPointIncrease * limitation) + limitation);
        }

        #endregion

        #region Public Properties

        public override int Level => PlayerData.Level;

        public override int MaxHealthPoint
        {
            get
            {
                int maxHealthPoint = ConvertSecondToFirst(PlayerData.Vitality, PlayerInfo.VitalityIncrease,
                    PlayerInfo.VitalityLimitation);
                return maxHealthPoint + GetMaxHealthPointAddition(maxHealthPoint);
            }
        }

        public override int MaxStaminaPoint => ConvertSecondToFirst(PlayerData.Endurance, PlayerInfo.EnduranceIncrease,
            PlayerInfo.EnduranceLimitation);

        public override int OffensivePower => ConvertSecondToFirst(PlayerData.Strength, PlayerInfo.StrengthIncrease,
            PlayerInfo.StrengthLimitation);

        public override int DefensivePower => ConvertSecondToFirst(PlayerData.Resistance, PlayerInfo.ResistanceIncrease,
            PlayerInfo.ResistanceLimitation);

        public override float MoveSpeed { get; } = 3.0f;

        public Element ElementalType { get; }

        public float ElementalPower { get; }

        public int SlashPower { get; }

        public int PenetrationPower { get; }

        public int BlowPower { get; }

        #endregion
    }
}