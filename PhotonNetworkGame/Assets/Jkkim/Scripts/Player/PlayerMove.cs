using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PUNGame
{
    /// <summary>
    /// SendMessage 방식이라 무조건 최상단 오브젝트에 꽃혀있어야함
    /// </summary>
    public class PlayerMove : MonoBehaviour
    {
        const float DEFAULT_GRAVITY_VALUE = -9.81f;

        [SerializeField] CharacterController _characterController;
        [SerializeField] PhotonView _photonView;
        [SerializeField] PhotonTransformView _photonTransformView;

        // 필요 없게될경우 프로퍼티 제거
        public PhotonView PhotonView
        {
            get
            {
                return _photonView;
            }
        }

        [Header("[이동/회전 속도]")]
        [SerializeField] float _forwardMoveSpeed = 5f;      // 전진 이동 속도
        [SerializeField] float _backwardMoveSpeed = 2f;     // 후진 이동 속도
        [SerializeField] float _strafeMoveSpeed = 3f;       // 좌우 이동 속도
        [SerializeField] float _rotateSpeed = 100f;         // 회전 속도

        [Header("[중력 배율]")]
        [SerializeField] float _gravityRatio = 1f;

        Vector3 _currentMovement;
        float _currentTurnSpeed;

        void Update()
        {
            if (_photonView.isMine)
            {
                ResetSpeedValues();

                UpdateRotateMovement();

                UpdateForwardMovement();
                UpdateBackwardMovement();
                UpdateStrafeMovement();

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