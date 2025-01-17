﻿using System.Collections.Generic;
using UnityEngine;

namespace StateBase
{
    public class StateBase<T> : MonoBehaviour
    {
        /// <summary>
        /// 현재 State를 설정하거나 반환하는 프로퍼티
        /// </summary>
        public T State
        {
            get => mState.Value;
            set
            {
                mState?.OnEnd?.Invoke();
                mState = mStateDict[value];
                mState.OnStart?.Invoke();
            }
        }

        private State<T> mState;

        private Dictionary<T, State<T>> mStateDict;

        public void SetState(State<T> state)
        {
            if(!mStateDict.ContainsKey(state.Value))
            {
                mStateDict.Add(state.Value, state);
            }
            else
            {
                mStateDict[state.Value] = state;
            }
        }

        protected virtual void Awake()
        {
            mStateDict = new Dictionary<T, State<T>>();
        }

        protected virtual void Update()
        {
            mState?.OnUpdate?.Invoke();
        }

        protected virtual void FixedUpdate()
        {
            mState?.OnFixedUpdate?.Invoke();
        }
    }
}