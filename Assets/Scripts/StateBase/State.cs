using UnityEngine;

namespace StateBase
{
    public abstract class State<T> 
    {
        public T Value { get; private set; }

        public State(T value)
        {
            Value = value;
        }

        public abstract void Start();
        public abstract void Update();
        public abstract void FixedUpdate();
        public abstract void End();
    }
}