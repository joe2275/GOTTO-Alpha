using System;
using UnityEngine;

namespace GTAlpha
{
    /// <summary>
    /// 플레이어의 공격 형식에 필요한 정보들을 관리하는 클래스
    /// </summary>
    [Serializable]
    public class PlayerAttackForm
    {
        #region Serialized Fields

        [SerializeField] private PlayerAttackMotion[] attackMotionArray;

        #endregion

        #region Properties
        
        /// <summary>
        /// 공격 형식에 존재하는 공격 모션들의 개수
        /// </summary>
        public int CountOfAttackMotions => attackMotionArray.Length;

        #endregion

        #region Public Functions

        /// <summary>
        /// 전달된 인덱스에 존재하는 공격 모션 객체를 반환하는 함수
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public PlayerAttackMotion GetAttackMotion(int index)
        {
            if (index < 0 || index >= attackMotionArray.Length)
            {
                Debug.LogErrorFormat("Out of Attack Motion Index - Length : {0}, Index : {1}", attackMotionArray.Length, index);
                return null;
            }

            return attackMotionArray[index];
        }

        #endregion
    }
}