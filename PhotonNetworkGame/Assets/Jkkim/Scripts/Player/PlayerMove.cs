using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PUNGame
{
    /// <summary>
    /// SendMessage 방식이라 무조건 최상단 오브젝트에 꽃혀있어야함
    /// </summary>
    public class PlayerMove : Photon.MonoBehaviour
    {
        const float DEFAULT_GRAVITY_VALUE = -9.81f;

        [SerializeField] CharacterController _characterController;
        [SerializeField] PhotonTransformView _photonTransformView;

        [Header("[이동/회전 속도]")]
        [SerializeField] float _forwardMoveSpeed = 5f;      // 전진 이동 속도
        [SerializeField] float _backwardMoveSpeed = 2f;     // 후진 이동 속도
        [SerializeField] float _strafeMoveSpeed = 3f;       // 좌우 이동 속도
        [SerializeField] float _rotateSpeed = 50f;         // 회전 속도

        [Header("[중력 배율]")]
        [SerializeField] float _gravityRatio = 1f;

        [Header("[플레이어 카메라 Offset]")]
        [SerializeField] float _panSpeed = 0.5f;              // 카메라 회전 속도
        [SerializeField] float _panOffsetMinY = 0f;
        [SerializeField] float _panOffsetMaxY = 15f;
        [SerializeField] float _panOffsetZ = -15f;

        Vector3 _currentMovement;
        float _currentTurnSpeed;
        bool _isMove;

        void Update()
        {
            if (photonView.isMine)
            {
                ResetSpeedValues();

                // Mouse Input
                UpdateJoystickMovement();
                UpdateScreenDrag();

                // KeyBoard Input
                UpdateRotateMovement();
                UpdateForwardMovement();
                UpdateBackwardMovement();
                UpdateStrafeMovement();

                // Move Apply
                MoveCharacterController();
                ApplyGravityToCharacterController();
                ApplySynchronizedValues();
            }
        }

        #region Movement Method
        void ResetSpeedValues()
        {
            _currentMovement = Vector3.zero;
            _currentTurnSpeed = 0;
        }

        void UpdateJoystickMovement()
        {
            var vertical = StageScene.Instance.JoyStick.Vertical;
            var horizontal = StageScene.Instance.JoyStick.Horizontal;

            var moveVertical = transform.forward * vertical * (vertical >= 0 ? _forwardMoveSpeed : _backwardMoveSpeed);
            var moveHorizontal = transform.right * horizontal * _strafeMoveSpeed;

            _currentMovement = moveVertical + moveHorizontal;
            _isMove = vertical != 0 || horizontal != 0;
        }

        Vector3 _panOriginPos;
        void UpdateScreenDrag()
        {
            if (_isMove)
                return;

#if UNITY_EDITOR
            if (EventSystem.current.IsPointerOverGameObject())
#else
            if (EventSystem.current.IsPointerOverGameObject(0))
#endif
                return;

            if (Input.GetMouseButtonDown(0))
            {
                _panOriginPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 panPos = Camera.main.ScreenToViewportPoint(Input.mousePosition) - _panOriginPos;

                if (Mathf.Abs(panPos.y) >= Mathf.Abs(panPos.x))
                {
                    // Camera Rotate
                    Vector3 cameraOffsetPos = new Vector3(0f, Mathf.Clamp(StageScene.Instance.PlayerCameraOffset.y - panPos.y * _panSpeed, _panOffsetMinY, _panOffsetMaxY), _panOffsetZ);
                    StageScene.Instance.PlayerCameraOffset = cameraOffsetPos;
                }
                else
                {
                    // Player Rotate
                    _currentTurnSpeed = panPos.x < 0 ? -_rotateSpeed : _rotateSpeed;
                    transform.Rotate(0.0f, _currentTurnSpeed * Time.deltaTime, 0.0f);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                
            }
        }

        void UpdateRotateMovement()
        {
            if (Input.GetKey(KeyCode.A) || Input.GetAxisRaw("Horizontal") < -0.1f)
            {
                _currentTurnSpeed = -_rotateSpeed;
                transform.Rotate(0.0f, -_rotateSpeed * Time.deltaTime, 0.0f);
            }

            if (Input.GetKey(KeyCode.D) || Input.GetAxisRaw("Horizontal") > 0.1f)
            {
                _currentTurnSpeed = _rotateSpeed;
                transform.Rotate(0.0f, _rotateSpeed * Time.deltaTime, 0.0f);
            }
        }

        void UpdateForwardMovement()
        {
            if (Input.GetKey(KeyCode.W) || Input.GetAxisRaw("Vertical") > 0.1f)
            {
                _currentMovement = transform.forward * _forwardMoveSpeed;
            }
        }

        void UpdateBackwardMovement()
        {
            if (Input.GetKey(KeyCode.S) || Input.GetAxisRaw("Vertical") < -0.1f)
            {
                _currentMovement = -transform.forward * _backwardMoveSpeed;
            }
        }

        void UpdateStrafeMovement()
        {
            if (Input.GetKey(KeyCode.Q) == true)
            {
                _currentMovement = -transform.right * _strafeMoveSpeed;
            }

            if (Input.GetKey(KeyCode.E) == true)
            {
                _currentMovement = transform.right * _strafeMoveSpeed;
            }
        }

        void MoveCharacterController()
        {
            _characterController.Move(_currentMovement * Time.deltaTime);
        }

        void ApplyGravityToCharacterController()
        {
            var gravity = DEFAULT_GRAVITY_VALUE * _gravityRatio;
            _characterController.Move(transform.up * Time.deltaTime * gravity);
        }

        void ApplySynchronizedValues()
        {
            _photonTransformView.SetSynchronizedValues(_currentMovement, _currentTurnSpeed);
        }
        #endregion
    }
}