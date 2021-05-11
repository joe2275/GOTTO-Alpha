using System;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public static class StateManager
    {
        public static StateHandler<bool> Pause { get; private set; }

        static StateManager()
        {
            Pause = new StateHandler<bool>();

            Pause.AddEnterEvent(true, OnPauseEnter);
            Pause.AddExitEvent(true, OnPauseExit);
        }

        #region Private Methods

        private static void OnPauseEnter()
        {
            Time.timeScale = 0.0f;
        }
        private static void OnPauseExit()
        {
            Time.timeScale = 1.0f;
        }

        #endregion
    }

    public class StateHandler<T>
    {
        #region Fields

        private T mCurState;
        private readonly Dictionary<T, Event> mEventDict;
        private readonly EqualityComparer<T> mComparer;

        #endregion

        #region Properties

        public T State
        {
            get => mCurState;
            set
            {
                // 기존의 상태값과 다르다면
                if (!mComparer.Equals(value, mCurState))
                {
                    // 기존의 상태값 저장
                    PrevState = mCurState;
                    // 현재 상태값 변경
                    mCurState = value;

                    // 이전 상태값이 이벤트 객체로 등록되어 있다면
                    if (mEventDict.ContainsKey(PrevState))
                    {
                        mEventDict[PrevState].Exit();
                    }
                    // 변경된 상태값이 이벤트 객체로 등록되어 있다면
                    if (mEventDict.ContainsKey(mCurState))
                    {
                        mEventDict[mCurState].Enter();
                    }
                }
            }
        }
        public T PrevState { get; private set; }

        #endregion


        #region Public Methods

        public StateHandler()
        {
            mEventDict = new Dictionary<T, Event>();
            mComparer = EqualityComparer<T>.Default;
        }

        public void AddEnterEvent(T e, Action action)
        {
            if (!mEventDict.ContainsKey(e))
            {
                mEventDict.Add(e, new Event());
            }

            mEventDict[e].OnEnter += action;
        }
        
        public void AddExitEvent(T e, Action action)
        {
            if (!mEventDict.ContainsKey(e))
            {
                mEventDict.Add(e, new Event());
            }

            mEventDict[e].OnExit += action;
        }
        
        public void RemoveEnterEvent(T e, Action action)
        {
            mEventDict[e].OnEnter -= action;
        }
        
        public void RemoveExitEvent(T e, Action action)
        {
            mEventDict[e].OnExit -= action;
        }
        
        #endregion

        private class Event
        {
            public event Action OnEnter;
            public event Action OnExit;

            public void Enter()
            {
                OnEnter?.Invoke();
            }
            public void Exit()
            {
                OnExit?.Invoke();
            }
        }
    }

}
