using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GTAlpha
{
    /// <summary>
    /// 플레이어 공격 모션에 필요한 정보를 관리하는 클래스
    /// </summary>
    [Serializable]
    public class PlayerAttackMotion
    {
        #region Serialized Fields

        [SerializeField] private int key;

        /// <summary>
        /// 반드시 순서대로 존재해야 할 것
        /// </summary>
        [SerializeField] private int[] connectionKeyArrayIn;
        /// <summary>
        /// 반드시 순서대로 존재해야 할 것
        /// </summary>
        [SerializeField] private int[] connectionKeyArrayOut;

        [SerializeField] private bool isNextAttackTimeFixed = true;
        
        [SerializeField] private float nextAttackTime;

        [SerializeField] private float minNextAttackTime;
        [SerializeField] private float maxNextAttackTime;

        [SerializeField] private bool canBeStartMotion = true;

        #endregion

        #region Properties

        /// <summary>
        /// 공격 모션이 가지는 공격 형식 내에서 고유한 Key 값
        /// </summary>
        public int Key => key;

        /// <summary>
        /// 이 공격 모션이 다음 공격 모션으로 연결될 수 있는 키 값 배열의 개수
        /// </summary>
        public int CountOfConnectionKeysIn => connectionKeyArrayIn.Length;
        /// <summary>
        /// 이 공격 모션의 다음 공격 모션으로 연결될 수 있는 키 값 배열의 개수
        /// </summary>
        public int CountOfConnectionKeysOut => connectionKeyArrayOut.Length;

        /// <summary>
        /// 다음 공격 모션이 나오는 시간으로 공격 입력 정확도를 판단할 때 사용
        /// </summary>
        public float NextAttackTime =>
            isNextAttackTimeFixed ? nextAttackTime : Random.Range(minNextAttackTime, maxNextAttackTime);

        /// <summary>
        /// 공격 모션 시작 시 첫 공격 모션으로 선택될 수 있는가
        /// </summary>
        public bool CanBeStartMotion => canBeStartMotion;

        #endregion

        #region Public Functions

        /// <summary>
        /// 전달된 인덱스에 존재하는 이 공격 모션이 다음 공격 모션으로 연결될 수 있는 키 값을 반환하는 함수
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int GetConnectionTypeIn(int index)
        {
            if (index < 0 || index >= connectionKeyArrayIn.Length)
            {
                Debug.LogErrorFormat("Out of Connection Key In Index - Length : {0}, Index : {1}", connectionKeyArrayIn.Length, index);
                return -1;
            }

            return connectionKeyArrayIn[index];
        }
        
        /// <summary>
        /// 전달된 인덱스에 존재하는 이 공격 모션의 다음 공격 모션으로 연결될 수 있는 키 값을 반환하는 함수
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int GetConnectionTypeOut(int index)
        {
            if (index < 0 || index >= connectionKeyArrayOut.Length)
            {
                Debug.LogErrorFormat("Out of Connection Key Out Index - Length : {0}, Index : {1}", connectionKeyArrayOut.Length, index);
                return -1;
            }

            return connectionKeyArrayOut[index];
        }

        #endregion
    }
}