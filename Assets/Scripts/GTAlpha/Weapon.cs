using UnityEngine;

namespace GTAlpha
{
    public class Weapon : MonoBehaviour
    {
        /// <summary>
        /// Weapon을 구분하기 위한 고유한 키 배열
        /// </summary>
        public static readonly string[] Keys = 
        {
            "Testing Sword"
        };
        /// <summary>
        /// 장착한 Weapon에 의해 Player의 공격 모션을 선택하기 위한 공격 형태 배열
        /// </summary>
        public static readonly string[] Forms =
        {
            "One Handed Sword"
        };

        /// <summary>
        /// 전달된 키가 Keys 배열의 어떤 인덱스에 저장되어 있는지 반환하는 함수
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetWeaponKeyIndex(string key)
        {
            for (int i = 0; i < Keys.Length; i++)
            {
                if (Keys[i].Equals(key))
                {
                    return i;
                }
            }
            
            Debug.LogError($"Not Exist Weapon Key : {key}");
            return -1;
        }

        /// <summary>
        /// 전달된 공격 형태가 Forms 배열의 어떤 인덱스에 저장되어 있는지 반환하는 함수
        /// </summary>
        /// <param name="weaponForm"></param>
        /// <returns></returns>
        public static int GetWeaponFormIndex(string weaponForm)
        {
            for (int i = 0; i < Forms.Length; i++)
            {
                if (Forms[i].Equals(weaponForm))
                {
                    return i;
                }
            }
            
            Debug.LogError($"Not Exist Weapon Form : {weaponForm}");
            return -1;
        }
    }
}