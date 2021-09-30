using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PAD.Player
{
    //ref:https://forum.unity.com/threads/third-person-camera.897767/
    public class ThirdPersonCam : MonoBehaviour
    {
        [SerializeField] bool _useInitialOffset = true;
        [SerializeField] bool _cursorVisible = true;
        [SerializeField] bool _collisionAvoidance = true;
        [SerializeField] float _collisionAvoidanceSpeed = 5;
        [SerializeField] float _maxSmoothMoveOffset = 2.5f;
        [SerializeField] Vector3 _originOffset;
        public float camOffsetDistance;
        float _currentCamOffsetDistance;
        [SerializeField] Vector2 _rotationSpeed = Vector2.one;
        [SerializeField] Vector2 _rotationLimitVertical = new Vector2(-70, 85);
        public Transform target;
        Transform _origin;
        [SerializeField] LayerMask noPlayerMask;


        void Start()
        {
            _origin = transform.parent;

            if (!target)
            {
                target = _origin.parent;
            }


            if (_useInitialOffset)
            {
                _originOffset = target.position - _origin.position;
            }
            if (camOffsetDistance == 0)
            {
                camOffsetDistance = (transform.position - _origin.position).magnitude;
            }
            _currentCamOffsetDistance = camOffsetDistance;

            SetCursorVisibility();
        }

        private void Update()
        {
            ChangeCursorVisibility();

            // do not rotate when the cursor is invisible
            if (!_cursorVisible)
            {
                OrbitCameraRotation();
            }
        }

        public void FixedUpdate()
        {
            // keep position
            ManageOriginPosition();

            if (_collisionAvoidance)
            {
                CollisionAvoidance();
            }
            else
            {
                _currentCamOffsetDistance = camOffsetDistance;
            }

            ManageDistance();
        }

        void ManageOriginPosition()
        {
            // non-smoothed version:
            //_origin.position = target.position + _originOffset;

            Vector3 wantedOriginPosition = target.position + _originOffset;
            Vector3 originToTargetVector = _origin.position - wantedOriginPosition;
            float originToTargetDistance = originToTargetVector.magnitude;

            float maxMoveDistanceLimit = originToTargetDistance - _maxSmoothMoveOffset;

            float maxMoveDistance = 0.01f + originToTargetDistance * 0.2f;

            maxMoveDistance = Mathf.Max(maxMoveDistance, maxMoveDistanceLimit);
            _origin.position =
                Vector3.MoveTowards(_origin.position, wantedOriginPosition, maxMoveDistance);
        }

        void ChangeCursorVisibility()
        {
            if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetMouseButtonDown(2))
            {
                _cursorVisible = !_cursorVisible;
                SetCursorVisibility();
            }
        }

        private float rotationVertical = 0f;

        void OrbitCameraRotation()
        {
            float hAxis = Input.GetAxis("Mouse X");
            float vAxis = -Input.GetAxis("Mouse Y");

            _origin.Rotate(Vector3.up, hAxis * _rotationSpeed.x, Space.World);

            rotationVertical += vAxis * _rotationSpeed.y;
            rotationVertical = ClampAngle(rotationVertical,
                                            _rotationLimitVertical.x,
                                            _rotationLimitVertical.y);

            _origin.localEulerAngles = new Vector3(rotationVertical,
                                                    _origin.localEulerAngles.y,
                                                    _origin.localEulerAngles.z);

            //_origin.Rotate(_origin.right, vAxis * _rotationSpeed.y * Time.deltaTime, Space.World);

            //_origin.rotation = ClampRotationAroundXAxis(_origin.rotation);
        }

        public static float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360F)
                angle += 360F;
            if (angle > 360F)
                angle -= 360F;
            return Mathf.Clamp(angle, min, max);
        }

        Quaternion ClampRotationAroundXAxis(Quaternion q)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

            angleX = Mathf.Clamp(angleX, _rotationLimitVertical.x, _rotationLimitVertical.y);

            q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

            return q;
        }

        void CollisionAvoidance()
        {
            Vector3 camToOriginVector = (transform.position - _origin.position).normalized * camOffsetDistance;
            RaycastHit hit;
            bool anythingBetweenCameraAndPlayer =
                Physics.Raycast(_origin.position, camToOriginVector, out hit,
                    camToOriginVector.magnitude, noPlayerMask);

            if (anythingBetweenCameraAndPlayer && hit.transform != target)
            {
                _currentCamOffsetDistance = (hit.point - _origin.position).magnitude;

                // non-smoothed version:
                //transform.position = hit.point - camToOriginVector.normalized * 0.1f;
            }
            else
            {
                _currentCamOffsetDistance = camOffsetDistance;
            }
        }

        void ManageDistance()
        {
            Vector3 camToOriginVector = transform.position - _origin.position;
            if (Vector3.Dot(camToOriginVector, transform.forward) > 0)
            {
                camToOriginVector *= -1;
            }
            float currentDistance = camToOriginVector.magnitude;
            currentDistance = Mathf.MoveTowards(currentDistance, _currentCamOffsetDistance,
                Time.deltaTime * _collisionAvoidanceSpeed);
            transform.position = _origin.position + camToOriginVector.normalized * currentDistance;
        }

        public void OnDeath()
        {
            _originOffset = Vector3.zero;
            transform.LookAt(_origin, Vector3.up);
            _collisionAvoidance = false;
        }

        void SetCursorVisibility()
        {
            Cursor.visible = _cursorVisible;
            Cursor.lockState = _cursorVisible ? CursorLockMode.None : CursorLockMode.Locked;
        }

    }
}