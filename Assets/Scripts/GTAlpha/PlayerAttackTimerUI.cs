using System;
using Camera3D;
using UnityEngine;

namespace GTAlpha
{
    public class PlayerAttackTimerUI : MonoBehaviour
    {
        [SerializeField] private Transform timerFromTransform;
        [SerializeField] private Transform timerToTransform;
        [SerializeField] private Vector2 fromToScale = new Vector2(5.0f, 1.0f);

        [SerializeField] private Transform targetTransform;

        // Scale : 5 -> 1
        public void SetTimer(int curMs, int targetMs)
        {
            float scale = fromToScale.x - (float) curMs / targetMs * (fromToScale.x - fromToScale.y);

            if (scale < Mathf.Epsilon)
            {
                scale = 0.0f;
            }

            timerFromTransform.localScale = new Vector3(scale, scale, 1.0f);
        }

        private void Update()
        {
            timerToTransform.position = ThirdPersonCamera.Main.Camera.WorldToScreenPoint(targetTransform.position);
            
            float scale = fromToScale.x - (float) PlayerAttackTimer.TimerMs / PlayerAttackTimer.AttackTimeMs * (fromToScale.x - fromToScale.y);

            if (scale < Mathf.Epsilon)
            {
                scale = 0.0f;
            }

            timerFromTransform.localScale = new Vector3(scale, scale, 1.0f);
        }

        private void OnEnable()
        {
            timerToTransform.position = ThirdPersonCamera.Main.Camera.WorldToScreenPoint(targetTransform.position);
            timerFromTransform.localScale = new Vector3(fromToScale.x, fromToScale.x, 1.0f);
        }
    }
}