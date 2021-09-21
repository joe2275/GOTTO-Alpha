using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Camera3D
{
    public class ThirdPersonCamera : MonoBehaviour
    {
        public static ThirdPersonCamera Main { get; private set; }
        
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

        [SerializeField] private Transform followTransform;

        #endregion

        #region Properties

        public Transform FollowTransform
        {
            get => followTransform;
            set => followTransform = value;
        }
        
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
            myTransform.localRotation = localRotation * rotator;

            Vector3 localEulerAngles = myTransform.localEulerAngles;

            if (localEulerAngles.x > 60.0f && localEulerAngles.x < 310.0f)
            {
                if (localEulerAngles.x < 180.0f)
                {
                    localEulerAngles.x = 60.0f;
                    localEulerAngles.z = 0.0f;
                    myTransform.localEulerAngles = localEulerAngles; 
                }
                else
                {
                    localEulerAngles.x = -50.0f;
                    localEulerAngles.z = 0.0f;
                    myTransform.localEulerAngles = localEulerAngles;
                }
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

            if (gameObject.CompareTag("MainCamera"))
            {
                Main = this;
            }
        }

        private void FixedUpdate()
        {
            if (!(TargetTransform is null))
            {
                Quaternion targetQuaternion = Quaternion.LookRotation(TargetTransform.position - transform.position);

                Transform myTransform = transform;
                myTransform.rotation = Quaternion.RotateTowards(myTransform.rotation, targetQuaternion,
                    maxRotationDelta * Time.deltaTime);
                
                Vector3 localEulerAngles = myTransform.localEulerAngles;
                
                if (localEulerAngles.x > 60.0f && localEulerAngles.x < 310.0f)
                {
                    if (localEulerAngles.x < 180.0f)
                    {
                        localEulerAngles.x = 60.0f;
                        localEulerAngles.z = 0.0f;
                        myTransform.localEulerAngles = localEulerAngles; 
                    }
                    else
                    {
                        localEulerAngles.x = -50.0f;
                        localEulerAngles.z = 0.0f;
                        myTransform.localEulerAngles = localEulerAngles;
                    }
                }
            }

            if (followTransform)
            {
                transform.position = followTransform.position;
            }
            
            cameraTransform.position = CameraPosition;
            cameraTransform.rotation = Quaternion.LookRotation(Focus);
        }

        #endregion
    }
}