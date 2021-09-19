using System;
using UnityEngine;

namespace GTAlpha
{
    public class PlayerAttackTimer : MonoBehaviour
    {
        #region Fields

        private static PlayerAttackTimer _instance;

        private static float _timer;
        private static int _recordedDiffMs;

        #endregion

        #region Properties
        
        public static bool IsRecorded { get; private set; }
        public static int AttackTimeMs { get; private set; }
        public static int TimerMs => (int)(_timer * 1000.0f);
        

        #endregion

        #region Public Functions

        public static void TimerOn(float attackTime)
        {
            AttackTimeMs = (int)(attackTime * 1000.0f);
            IsRecorded = false;
            _timer = 0.0f;
            _instance.gameObject.SetActive(true);
        }

        public static void TimerOff()
        {
            _instance.gameObject.SetActive(false);
        }

        public static AttackAccuracy GetAccuracy(int playerLv, int enemyLv)
        {
            int paramX = GetParameterX(playerLv, enemyLv);
            int timeDiffMs = _recordedDiffMs;

            int perfectRange = GetAccuracyRange(paramX, PlayerAttackTimerConstant.AttackPerfectGradient,
                PlayerAttackTimerConstant.AttackPerfectMinTimeMs, PlayerAttackTimerConstant.AttackPerfectMaxTimeMs);

            if (timeDiffMs <= perfectRange)
            {
                return AttackAccuracy.Perfect;
            }

            int goodRange = GetAccuracyRange(paramX, PlayerAttackTimerConstant.AttackGoodGradient,
                PlayerAttackTimerConstant.AttackGoodMinTimeMs, PlayerAttackTimerConstant.AttackGoodMaxTimeMs);

            if (timeDiffMs <= goodRange)
            {
                return AttackAccuracy.Good;
            }

            int badRange = GetAccuracyRange(paramX, PlayerAttackTimerConstant.AttackBadGradient,
                PlayerAttackTimerConstant.AttackBadMinTimeMs, PlayerAttackTimerConstant.AttackBadMaxTimeMs);
            if (timeDiffMs <= badRange)
            {
                return AttackAccuracy.Bad;
            }

            return AttackAccuracy.Miss;
        }
        
        public static int GetParameterX(int playerLv, int enemyLevel)
        {
            return 10 * (playerLv - enemyLevel);
        }

        public static int GetAccuracyRange(int paramX, float gradient, int minRange, int maxRange)
        {
            // a^x
            float apx = Mathf.Pow(gradient, paramX);
            // a^-x
            float amx = Mathf.Pow(gradient, -paramX);

            return (int)((0.5f * (minRange + maxRange) - minRange) * (apx - amx) / (apx + amx) +
                         0.5f * (minRange + maxRange));
        }

        #endregion

        #region Private Functions

        private void Awake()
        {
            if (_instance is null)
            {
                _instance = this;
                gameObject.SetActive(false);
                return;
            }

            Destroy(gameObject);
        }

        private void Update()
        {
            if (IsRecorded) return;
            
            _timer += Time.deltaTime;
            int timerMs = TimerMs;
            int curTimeDiffMs = Mathf.Abs(timerMs - AttackTimeMs);

            // 현재 기록된 시간 차가 기록 가능한 범위인 경우
            if (curTimeDiffMs < PlayerAttackTimerConstant.AttackRecordTimeMs)
            {
                // 이전 기록된 시간 차가 존재하지 않고 유저가 공격 버튼을 입력한 경우
                if (!PlayerInput.AttackStarted) return;
                IsRecorded = true;
                PlayerInput.AttackStarted = false;

                _recordedDiffMs = curTimeDiffMs;
            }
            else
            {
                PlayerInput.AttackStarted = false;
            }
        }

        #endregion
    }
}