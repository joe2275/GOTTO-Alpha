using Manager;
using UnityEngine;

namespace GTAlpha
{
    /// <summary>
    /// Weapon 관련 정보 중, 변하지 않는 무기 정보를 저장한다. 
    /// </summary>
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

        [SerializeField] private string weaponForm = "";
        

        #endregion

        #region Properties

        /// <summary>
        /// 무기 아이콘 이미지
        /// </summary>
        public Sprite Image => image;
        /// <summary>
        /// Script 에서 이름 문자열을 찾기 위한 키 값
        /// </summary>
        public string NameKey => nameKey;
        /// <summary>
        /// NameKey 를 이용하여 Script에서 찾은 이름 문자열
        /// </summary>
        public string Name => ScriptManager.HasKey(nameKey) ? ScriptManager.GetScript(nameKey) : "None";

        /// <summary>
        /// 무기의 기본 공격력
        /// </summary>
        public int OffensivePower => offensivePower;
        /// <summary>
        /// 무기의 기본 방어력
        /// </summary>
        public int DefensivePower => defensivePower;
        /// <summary>
        /// 무기의 기본 이동속도 향상율
        /// </summary>
        public float MoveSpeedIncrease => moveSpeedIncrease;
        /// <summary>
        /// 무기의 기본 속성
        /// </summary>
        public Element Element => element;
        /// <summary>
        /// 무기의 기본 속성력
        /// </summary>
        public int ElementalPower => elementalPower;
        /// <summary>
        /// 무기의 기본 참격력
        /// </summary>
        public int SlashPower => slashPower;
        /// <summary>
        /// 무기의 기본 관통력
        /// </summary>
        public int PenetrationPower => penetrationPower;
        /// <summary>
        /// 무기의 기본 타격력
        /// </summary>
        public int BlowPower => blowPower;

        /// <summary>
        /// 무기 형태
        /// </summary>
        public string WeaponForm => weaponForm;


        #endregion
    }
}