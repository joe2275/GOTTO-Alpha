using System.Collections.Generic;
using UnityEngine;

namespace GTAlpha
{
    /// <summary>
    /// 플레이어 공격 시스템을 책임지는 클래스
    /// </summary>
    [CreateAssetMenu(fileName = "New Player Attack System", menuName = "Player Attack System", order = 0)]
    public class PlayerAttackSystem : GlobalScriptableObject
    {
        #region Serialized Fields

        [SerializeField] private PlayerAttackForm[] playerAttackFormArray = new PlayerAttackForm[Weapon.Forms.Length];

        #endregion

        #region Fields
        
        private static PlayerAttackSystem _main;
        private static Dictionary<int, List<PlayerAttackMotion>>[] _attackMotionConnectionInDictArray;

        private static readonly List<int> _temporaryIndexList = new List<int>();

        #endregion

        #region Properties

        
        #endregion

        #region Public Functions
        
        /// <summary>
        /// 전달된 인덱스에 존재하는 플레이어 무기 형식인 PlayerAttackForm 객체를 반환하는 정적 함수
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static PlayerAttackForm GetPlayerAttackForm(int index)
        {
            PlayerAttackForm[] playerAttackFormArray = _main.playerAttackFormArray;
            if (index < 0 || index >= playerAttackFormArray.Length)
            {
                Debug.LogErrorFormat("Out of Player Attack Form Index - Length : {0}, Index : {1}", playerAttackFormArray.Length, index);
                return null;
            }

            return playerAttackFormArray[index];
        }

        /// <summary>
        /// 전달된 weaponFormIndex에 존재하는 플레이어 공격 형식에서 motionCount 만큼의 공격 모션 배열 result를 반환하는 정적 함수
        /// motionCount 보다 더 적게 연결될 수 있기 때문에 resultCount를 out 정수 형태의 매개변수가 필요하다. 
        /// </summary>
        /// <param name="weaponFormIndex"></param>
        /// <param name="motionCount"></param>
        /// <param name="result"></param>
        /// <param name="resultCount"></param>
        public static void GetPlayerAttackMotionArray(int weaponFormIndex, int motionCount, PlayerAttackMotion[] result, out int resultCount)
        {
            PlayerAttackForm attackForm = GetPlayerAttackForm(weaponFormIndex);

            var attackMotionConnectionInDict = _attackMotionConnectionInDictArray[weaponFormIndex];
            int max = attackForm.CountOfAttackMotions;
            PlayerAttackMotion motion = attackForm.GetAttackMotion(Random.Range(0, max));
            result[0] = motion;

            for (int i = 1; i < motionCount; i++)
            {
                int index = Random.Range(0, motion.CountOfConnectionKeysOut);

                bool isExist = false;

                for (int j = 0; j < motion.CountOfConnectionKeysOut; j++)
                {
                    int connection = motion.GetConnectionTypeOut((index + j) % motion.CountOfConnectionKeysOut);

                    if (attackMotionConnectionInDict.ContainsKey(connection))
                    {
                        var connectionInList = attackMotionConnectionInDict[connection];
                        result[i] = attackMotionConnectionInDict[connection][Random.Range(0, connectionInList.Count)];
                        motion = result[i];
                        isExist = true;
                        break;
                    }
                }

                if (!isExist)
                {
                    resultCount = i;
                    return;
                }
            }

            resultCount = motionCount;
        }
        
        /// <summary>
        /// 전달된 무기 형식에 해당하는 공격 모션 중 공격의 첫 모션으로 사용할 수 있는 PlayerAttackMotion 객체를 반환하는 정적 함수
        /// </summary>
        /// <param name="weaponForm"></param>
        /// <returns></returns>
        public static PlayerAttackMotion GetPlayerAttackMotionStart(string weaponForm)
        {
            return GetPlayerAttackMotionStart(Weapon.GetWeaponFormIndex(weaponForm));
        }

        /// <summary>
        /// 전달된 인덱스의 무기 형식에 해당하는 공격 모션 중 공격의 첫 모션으로 사용할 수 있는 PlayerAttackMotion 객체를 반환하는 정적 함수
        /// </summary>
        /// <param name="weaponFormIndex"></param>
        /// <returns></returns>
        public static PlayerAttackMotion GetPlayerAttackMotionStart(int weaponFormIndex)
        {
            if (weaponFormIndex < 0 || weaponFormIndex > Weapon.Forms.Length)
            {
                Debug.LogError($"Out of Weapon Form Array Index - Index : {weaponFormIndex}, Length : {Weapon.Forms.Length}");
                return null;
            }

            PlayerAttackForm playerAttackForm = _main.playerAttackFormArray[weaponFormIndex];

            _temporaryIndexList.Clear();
            for (int i = 0; i < playerAttackForm.CountOfAttackMotions; i++)
            {
                _temporaryIndexList.Add(i);
            }

            if (_temporaryIndexList.Count < 1)
            {
                Debug.LogError($"Not Exist Start Motion - Weapon Form : {Weapon.Forms[weaponFormIndex]}");
                return null;
            }
            int index = _temporaryIndexList[Random.Range(0, _temporaryIndexList.Count)];
            PlayerAttackMotion playerAttackMotion = playerAttackForm.GetAttackMotion(_temporaryIndexList[index]); 
            while (!playerAttackMotion.CanBeStartMotion)
            {
                _temporaryIndexList.RemoveAt(index);
                if (_temporaryIndexList.Count < 1)
                {
                    Debug.LogError($"Not Exist Start Motion - Weapon Form : {Weapon.Forms[weaponFormIndex]}");
                    return null;
                }
                index = _temporaryIndexList[Random.Range(0, _temporaryIndexList.Count)];
                playerAttackMotion = playerAttackForm.GetAttackMotion(_temporaryIndexList[index]);
            }

            return playerAttackMotion;
        }

        /// <summary>
        /// 전달된 무기 형식에 해당하는 공격 모션 중 전달된 공격 모션 다음에 연결될 수 있는 공격 모션을 랜덤하게 반환하는 정적 함수
        /// </summary>
        /// <param name="weaponForm"></param>
        /// <param name="motion"></param>
        /// <returns></returns>
        public static PlayerAttackMotion GetPlayerAttackMotionNext(string weaponForm, PlayerAttackMotion motion)
        {
            return GetPlayerAttackMotionNext(Weapon.GetWeaponFormIndex(weaponForm), motion);
        }
        
        /// <summary>
        /// 전달된 무기 형식에 해당하는 공격 모션 중 전달된 공격 모션 다음에 연결될 수 있는 공격 모션을 랜덤하게 반환하는 정적 함수
        /// </summary>
        /// <param name="weaponFormIndex"></param>
        /// <param name="motion"></param>
        /// <returns></returns>
        public static PlayerAttackMotion GetPlayerAttackMotionNext(int weaponFormIndex, PlayerAttackMotion motion)
        {
            if (weaponFormIndex < 0 || weaponFormIndex > Weapon.Forms.Length)
            {
                Debug.LogError($"Out of Weapon Form Array Index - Index : {weaponFormIndex}, Length : {Weapon.Forms.Length}");
                return null;
            }
            
            var attackMotionConnectionInDict = _attackMotionConnectionInDictArray[weaponFormIndex];

            List<PlayerAttackMotion> attackMotionList = attackMotionConnectionInDict[Random.Range(0, motion.CountOfConnectionKeysOut)];
            return attackMotionList[Random.Range(0, attackMotionList.Count)];
        }

        /// <summary>
        /// 플레이어 공격 형식 배열에 저장된 정보를 빠른 시간에 정보를 계산하기 위해 정보를 알맞게 분류하는 Load 함수
        /// </summary>
        public override void Load()
        {
            _main = this;

            _attackMotionConnectionInDictArray =
                new Dictionary<int, List<PlayerAttackMotion>>[_main.playerAttackFormArray.Length];

            for (int i = 0; i < _attackMotionConnectionInDictArray.Length; i++)
            {
                _attackMotionConnectionInDictArray[i] = new Dictionary<int, List<PlayerAttackMotion>>();
            }

            for (int i = 0; i < _main.playerAttackFormArray.Length; i++)
            {
                PlayerAttackForm form = _main.playerAttackFormArray[i];

                for (int j = 0; j < form.CountOfAttackMotions; j++)
                {
                    PlayerAttackMotion motion = form.GetAttackMotion(j);
                    for (int k = 0; k < motion.CountOfConnectionKeysIn; k++)
                    {
                        if (!_attackMotionConnectionInDictArray[i].ContainsKey(motion.GetConnectionTypeIn(k)))
                        {
                            _attackMotionConnectionInDictArray[i].Add(motion.GetConnectionTypeIn(k), new List<PlayerAttackMotion>());
                        }
                        _attackMotionConnectionInDictArray[i][motion.GetConnectionTypeIn(k)].Add(motion);
                    }
                }
            }
        }

        #endregion
    }
}