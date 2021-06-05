using System;
using UnityEngine;

namespace StateBase
{
    public class State<T> 
    {
        public T Value { get; }

        public State(T value)
        {
            Value = value;
        }

        public Action OnStart;
        public Action OnEnd;
        public Action OnUpdate;
        public Action OnFixedUpdate;
    }
}