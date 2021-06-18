using Manager;
using UnityEngine;

namespace GTAlpha
{
    /*
     * WeaponInfo
     * Weapon 관련 정보 중, 변하지 않는 정보를 저장한다. 
     */
    [CreateAssetMenu(fileName = "New Weapon Information", menuName = "Information/Weapon", order = 0)]
    public class WeaponInfo : ScriptableObject
    {
        #region Serialized Fields
        
        [SerializeField] private Sprite image;
        [SerializeField] private string nameKey;

        [SerializeField] private int offensivePower = 100;
        [SerializeField] private int defensivePower = 10;
        [SerializeField] private float moveSpeedIncrease = 0.0f;
        [SerializeField] private Element element = Element.Fire;
        [SerializeField] private int elementalPower = 50;
        [SerializeField] private int slashPower = 50;
        [SerializeField] private int penetrationPower = 10;
        [SerializeField] private int blowPower = 40;

        [SerializeField] private WeaponForm weaponForm = WeaponForm.TestingForm;
        

        #endregion

        #region Properties

        public Sprite Image => image;

        public string NameKey => nameKey;
        public string Name => ScriptManager.HasKey(nameKey) ? ScriptManager.GetScript(nameKey) : "None";

        public int OffensivePower => offensivePower;
        public int DefensivePower => defensivePower;
        public float MoveSpeedIncrease => moveSpeedIncrease;
        public Element Element => element;
        public int ElementalPower => elementalPower;
        public int SlashPower => slashPower;
        public int PenetrationPower => penetrationPower;
        public int BlowPower => blowPower;

        public WeaponForm WeaponForm => weaponForm;


        #endregion
    }
}