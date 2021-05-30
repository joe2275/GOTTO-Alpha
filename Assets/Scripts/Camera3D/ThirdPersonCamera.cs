using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Camera3D
{
    public class ThirdPersonCamera : MonoBehaviour
    {
        #region Serialized Fields

        [SerializeField] private LayerMask obstacleLayer;

        [SerializeField] private Transform forwardTransform;
        [SerializeField] private Transform cameraTransform;

        [SerializeField] private float focusDistance;
        [SerializeField] private float backwardDistance;
        [SerializeField] private float rightDistance;
        [SerializeField] private float upDistance;

        #endregion

        #region Properties

        public Vector3 Forward => forwardTransform.position - transform.position;

        public Vector3 Focus => transform.position + Forward * focusDistance - cameraTransform.position;

        public Vector3 CameraPosition
        {
            get
            {
                Vector3 position = transform.position;
                Vector3 forward = forwardTransform.position - position;
                Vector3 backward = -forward;
                Vector3 cameraPosition = position;
                Vector3 rayPosition = position;

                // Ray ray = new Ray(cameraPosition, backward);
                //
                // if (Physics.Raycast(ray, out RaycastHit hit, backwardDistance, obstacleLayer))
                // {
                //     cameraPosition += backward * hit.distance;
                // }
                // else
                // {
                //     cameraPosition += backward * backwardDistance;
                // }
                
                cameraPosition += backward * backwardDistance;

                Vector3 right = new Vector3(forward.z, 0.0f, -forward.x).normalized;
                cameraPosition += right * rightDistance;
                rayPosition += right * rightDistance;

                // ray = new Ray(cameraPosition, right);
                //
                // if (Physics.Raycast(ray, out hit, rightDistance, obstacleLayer))
                // {
                //     cameraPosition += right * hit.distance;
                // }
                // else
                // {
                //     cameraPosition += right * rightDistance;
                // }

                Vector3 up = new Vector3(forward.y * right.z - forward.z * right.y,
                    forward.z * right.x - forward.x * right.z, forward.x * right.y - forward.y * right.x).normalized;
                cameraPosition += up * upDistance;
                rayPosition += up * upDistance;

                // ray = new Ray(cameraPosition, up);
                //
                // if (Physics.Raycast(ray, out hit, upDistance, obstacleLayer))
                // {
                //     cameraPosition += up * hit.distance;
                // }
                // else
                // {
                //     cameraPosition += up * upDistance;
                // }

                Ray ray = new Ray(rayPosition, backward);
                if (Physics.Raycast(ray, out RaycastHit hit, backwardDistance, obstacleLayer))
                {
                    return hit.point;
                }
                else
                {
                    return cameraPosition;
                }
            }
        }

        #endregion

        #region Public Methods

        public void Rotate(Vector2 angles)
        {
            Transform myTransform = transform;
            myTransform.Rotate(Vector3.up, angles.x, Space.World);

            Quaternion localRotation = myTransform.localRotation;
            Quaternion rotator = Quaternion.AngleAxis(-angles.y, Vector3.right);
            Quaternion newRotation = localRotation * rotator;

            Vector3 localEulerAngles = newRotation.eulerAngles;

            if (localEulerAngles.x < 60.0f || localEulerAngles.x > 310.0f)
            {
                myTransform.localRotation = newRotation;
            }
        }

        #endregion

        #region Private Methods

        private void Update()
        {
            Rotate(new Vector2(Keyboard.current.rightArrowKey.ReadValue() - Keyboard.current.leftArrowKey.ReadValue(), Keyboard.current.upArrowKey.ReadValue() - Keyboard.current.downArrowKey.ReadValue()) * 0.1f);
            // Rotate(Mouse.current.delta.ReadValue() * 0.1f);
        }

        private void LateUpdate()
        {
            cameraTransform.position = CameraPosition;
            cameraTransform.rotation = Quaternion.LookRotation(Forward) * Quaternion.FromToRotation(Forward, Focus);
        }

        #endregion
    }
}