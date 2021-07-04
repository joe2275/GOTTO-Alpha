using System.Collections.Generic;
using UnityEngine;

namespace GTAlpha
{
    [CreateAssetMenu(fileName = "New Player Attack System", menuName = "Player Attack System", order = 0)]
    public class PlayerAttackSystem : GlobalScriptableObject
    {
        #region Serialized Fields

        [SerializeField] private PlayerAttackForm[] playerAttackFormArray = new PlayerAttackForm[Weapon.Forms.Length];

        #endregion

        #region Fields
        
        private static PlayerAttackSystem _main;
        private static Dictionary<int, List<PlayerAttackMotion>>[] _attackMotionConnectionInDictArray;

        #endregion

        #region Properties

        
        #endregion

        #region Public Functions
        
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