using System.Collections.Generic;
using UnityEngine;

namespace GTAlpha
{
    [CreateAssetMenu(fileName = "New Player Attack System", menuName = "Player Attack System", order = 0)]
    public class PlayerAttackSystem : GlobalScriptableObject
    {
        #region Serialized Fields

        [SerializeField] private PlayerAttackForm[] playerAttackFormArray = new PlayerAttackForm[(int)WeaponForm.Count];

        #endregion

        #region Fields
        
        private static PlayerAttackSystem _main;
        private static Dictionary<int, List<PlayerAttackMotion>>[] _singleTargetConnectionInDictArray;
        private static Dictionary<int, List<PlayerAttackMotion>>[] _multipleTargetConnectionInDictArray;

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

        public static void GetPlayerAttackMotionArray(WeaponForm form, AttackWay attackWay, int motionCount, PlayerAttackMotion[] result, out int resultCount)
        {
            PlayerAttackForm attackForm = GetPlayerAttackForm((int) form);

            if (attackWay == AttackWay.Single)
            {
                var singleTargetConnectionInDict = _singleTargetConnectionInDictArray[(int) form];
                int max = attackForm.CountOfSingleTargetMotions;
                PlayerAttackMotion motion = attackForm.GetSingleTargetMotion(Random.Range(0, max));
                result[0] = motion;

                for (int i = 1; i < motionCount; i++)
                {
                    int index = Random.Range(0, motion.CountOfConnectionKeysOut);

                    bool isExist = false;

                    for (int j = 0; j < motion.CountOfConnectionKeysOut; j++)
                    {
                        int connection = motion.GetConnectionTypeOut((index + j) % motion.CountOfConnectionKeysOut);

                        if (singleTargetConnectionInDict.ContainsKey(connection))
                        {
                            var connectionInList = singleTargetConnectionInDict[connection];
                            result[i] = singleTargetConnectionInDict[connection][
                                Random.Range(0, connectionInList.Count)];
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
            }
            else
            {
                var multipleTargetConnectionInDict = _multipleTargetConnectionInDictArray[(int) form];
                int max = attackForm.CountOfMultipleTargetMotions;
                PlayerAttackMotion motion = attackForm.GetMultipleTargetMotion(Random.Range(0, max));
                result[0] = motion;

                for (int i = 1; i < motionCount; i++)
                {
                    int index = Random.Range(0, motion.CountOfConnectionKeysOut);

                    bool isExist = false;

                    for (int j = 0; j < motion.CountOfConnectionKeysOut; j++)
                    {
                        int connection = motion.GetConnectionTypeOut((index + j) % motion.CountOfConnectionKeysOut);

                        if (multipleTargetConnectionInDict.ContainsKey(connection))
                        {
                            var connectionInList = multipleTargetConnectionInDict[connection];
                            result[i] = multipleTargetConnectionInDict[connection][
                                Random.Range(0, connectionInList.Count)];
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
            }
            
            resultCount = motionCount;
        }

        public override void Load()
        {
            _main = this;

            _singleTargetConnectionInDictArray = new Dictionary<int, List<PlayerAttackMotion>>[_main.playerAttackFormArray.Length];
            _multipleTargetConnectionInDictArray =
                new Dictionary<int, List<PlayerAttackMotion>>[_main.playerAttackFormArray.Length];

            for (int i = 0; i < _main.playerAttackFormArray.Length; i++)
            {
                PlayerAttackForm form = _main.playerAttackFormArray[i];

                for (int j = 0; j < form.CountOfSingleTargetMotions; j++)
                {
                    PlayerAttackMotion motion = form.GetSingleTargetMotion(j);
                    for (int k = 0; k < motion.CountOfConnectionKeysIn; k++)
                    {
                        if (!_singleTargetConnectionInDictArray[i].ContainsKey(motion.GetConnectionTypeIn(k)))
                        {
                            _singleTargetConnectionInDictArray[i].Add(k, new List<PlayerAttackMotion>());
                        }
                        _singleTargetConnectionInDictArray[i][k].Add(motion);
                    }
                }

                for (int j = 0; j < form.CountOfMultipleTargetMotions; j++)
                {
                    PlayerAttackMotion motion = form.GetMultipleTargetMotion(j);
                    for (int k = 0; k < motion.CountOfConnectionKeysIn; k++)
                    {
                        if (!_multipleTargetConnectionInDictArray[i].ContainsKey(motion.GetConnectionTypeIn(k)))
                        {
                            _multipleTargetConnectionInDictArray[i].Add(k, new List<PlayerAttackMotion>());
                        }
                        _multipleTargetConnectionInDictArray[i][k].Add(motion);
                    }
                }
            }
        }

        #endregion
    }
}