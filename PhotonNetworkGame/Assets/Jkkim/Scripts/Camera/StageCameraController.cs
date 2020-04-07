using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace PUNGame
{
    public class StageCameraController : MonoBehaviour
    {
        [SerializeField] CinemachineVirtualCamera _playerCamera;

        static StageCameraController _instance;

        public static StageCameraController Instance
        {
            get
            {
                return _instance;
            }
        }

        public CinemachineVirtualCamera PlayerCamera
        {
            get
            {
                return _playerCamera;
            }
        }

        private void Awake()
        {
            _instance = this;
        }
    }
}