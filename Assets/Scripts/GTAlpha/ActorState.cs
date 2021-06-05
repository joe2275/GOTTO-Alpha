using System;
using StateBase;

namespace GTAlpha
{
    public class ActorState : State<int>
    {
        #region State Constants

        public const int Idle = 0;
        public const int Move = 1;
        public const int Hit = 2;
        public const int Die = 3;
        public const int Attack = 4;

        #endregion


        #region Actions

        public Action OnStart { get; set; }
        public Action OnEnd { get; set; }
        public Action OnUpdate { get; set; }
        public Action OnFixedUpdate { get; set; }

        #endregion


        public ActorState(int state) : base(state)
        {
        }

        public override void Start()
        {
            OnStart?.Invoke();
        }

        public override void End()
        {
            OnEnd?.Invoke();
        }

        public override void Update()
        {
            OnUpdate?.Invoke();
        }

        public override void FixedUpdate()
        {
            OnFixedUpdate?.Invoke();
        }
    }
}