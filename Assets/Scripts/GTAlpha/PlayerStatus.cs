namespace GTAlpha
{
    public class PlayerStatus : Status
    {
        #region Private Static Methods

        private static int ConvertSecondToFirst(int second, float increase, int limitation)
        {
            return -(int) (increase * limitation * limitation / (second + increase * limitation) + limitation);
        }

        private static int GetMaxHealthPointAddition(int maxHealthPoint)
        {
            float limitation = Constant.MaximumHealthPointTimes * maxHealthPoint;
            return -(int) (Constant.MaxHealthPointIncrease * limitation * limitation /
                (PlayerData.Possessions + Constant.MaxHealthPointIncrease * limitation) + limitation);
        }

        #endregion

        #region Public Properties

        public override int MaxHealthPoint
        {
            get
            {
                int maxHealthPoint = ConvertSecondToFirst(PlayerData.Vitality, Constant.VitalityIncrease,
                    Constant.VitalityLimitation);
                return maxHealthPoint + GetMaxHealthPointAddition(maxHealthPoint);
            }
        }

        public override int MaxStaminaPoint => ConvertSecondToFirst(PlayerData.Endurance, Constant.EnduranceIncrease,
            Constant.EnduranceLimitation);

        public override int OffensivePower => ConvertSecondToFirst(PlayerData.Strength, Constant.StrengthIncrease,
            Constant.StrengthLimitation);

        public override int DefensivePower => ConvertSecondToFirst(PlayerData.Resistance, Constant.ResistanceIncrease,
            Constant.ResistanceLimitation);

        public override float MoveSpeed { get; }

        public Element ElementalType { get; }

        public float ElementalPower { get; }

        public int SlashPower { get; }

        public int PenetrationPower { get; }

        public int BlowPower { get; }

        #endregion
    }
}