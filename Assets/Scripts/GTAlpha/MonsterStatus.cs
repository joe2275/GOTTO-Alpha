namespace GTAlpha
{
    public class MonsterStatus : CharacterStatus
    {
        #region Public Properties

        public override int Level { get; }

        public override int MaxHealthPoint { get; }
        public override int MaxStaminaPoint { get; }
        public override int OffensivePower { get; }
        public override int DefensivePower { get; }
        public override float MoveSpeed { get; }

        public int WaterResistance { get; }
        public int FireResistance { get; }
        public int EarthResistance { get; }

        public int SlashResistance { get; }
        public int PenetrationResistance { get; }
        public int BlowResistance { get; }

        #endregion
    }
}