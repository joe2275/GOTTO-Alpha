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
        [SerializeField] private Transform rightTransform;
        [SerializeField] private Transform cameraTransform;

        [SerializeField] private float focusDistance;
        [SerializeField] private float backwardDistance;
        [SerializeField] private float rightDistance;
        [SerializeField] private float upDistance;

        [SerializeField] private float obstacleDistance = 0.0f;

        [SerializeField] private LayerMask targetLayer;
        [SerializeField] private float maxTargetDistance = 100.0f;
        [SerializeField] private float maxRotationDelta = 360.0f;

        #endregion

        #region Properties
        
        public Camera Camera { get; private set; }

        public Transform TargetTransform { get; set; }

        public Vector3 Forward => forwardTransform.position - transform.position;
        public Vector3 Right => rightTransform.position - transform.position;

        public Vector3 Focus
        {
            get
            {
                return transform.position - cameraTransform.position + Forward * focusDistance;
                // if (TargetTransform is null)
                // {
                //     return transform.position - cameraTransform.position + Forward * focusDistance;
                // }
                //
                // Vector3 position = transform.position;
                // return position - cameraTransform.position +
                //        Forward * (TargetTransform.position - position).magnitude;
            }
        }

        public Vector3 CameraPosition
        {
            get
            {
                Vector3 position = transform.position;
                Vector3 forward = forwardTransform.position - position;
                Vector3 backward = -forward;
                Vector3 cameraPosition = position;
                Vector3 rayPosition = position;

                cameraPosition += backward * backwardDistance;

                Vector3 right = Right;
                cameraPosition += right * rightDistance;
                rayPosition += right * rightDistance;

                Vector3 up = new Vector3(forward.y * right.z - forward.z * right.y,
                    forward.z * right.x - forward.x * right.z, forward.x * right.y - forward.y * right.x).normalized;
                cameraPosition += up * upDistance;
                rayPosition += up * upDistance;

                Ray ray = new Ray(rayPosition, backward);
                if (Physics.Raycast(ray, out RaycastHit hit, backwardDistance + obstacleDistance, obstacleLayer))
                {
                    cameraPosition = rayPosition + backward * (hit.distance - obstacleDistance);
                }

                return cameraPosition;
            }
        }

        #endregion

        #region Public Functions

        public void Rotate(Vector2 angles)
        {
            if (!(TargetTransform is null))
            {
                return;
            }
            
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

        public Transform SearchTarget()
        {
            Ray ray = Camera.ScreenPointToRay(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0.0f));
            if (Physics.Raycast(ray, out RaycastHit hit, maxTargetDistance, targetLayer))
            {
                TargetTransform = hit.transform;
            }
            else
            {
                TargetTransform = null;
            }

            return TargetTransform;
        }

        #endregion

        #region Private Functions

        private void Awake()
        {
            Camera = GetComponentInChildren<Camera>(true);
        }

        private void LateUpdate()
        {
            if (!(TargetTransform is null))
            {
                Quaternion targetQuaternion = Quaternion.LookRotation(TargetTransform.position - transform.position);

                Transform myTransform = transform;
                myTransform.rotation = Quaternion.RotateTowards(myTransform.rotation, targetQuaternion,
                    maxRotationDelta * Time.deltaTime);
                
                Vector3 localEulerAngles = myTransform.localEulerAngles;
                
                if (localEulerAngles.x > 60.0f && localEulerAngles.x < 180.0f)
                {
                    localEulerAngles.x = 60.0f;
                    myTransform.localEulerAngles = localEulerAngles; 
                }
                else if (localEulerAngles.x < 310.0f && localEulerAngles.x > 180.0f)
                {
                    localEulerAngles.x = -50.0f;
                    myTransform.localEulerAngles = localEulerAngles;
                }
            }
            
            cameraTransform.position = CameraPosition;
            cameraTransform.rotation = Quaternion.LookRotation(Focus);
        }

        #endregion
    }
}