using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace PUNGame
{
    public class StageScene : Photon.MonoBehaviour
    {
        const string PLAYER_RESOURCE_NAME = "TestPlayer";

        CinemachineVirtualCamera _playerCamera;
        Joystick _joystick;

        // 나중에 구조화(맵별로 포지션 다르게 가져오는 처리도 해야함)
        public Vector3 SpawnPosition = new Vector3(0f, 3f, 0f);
        
        static StageScene _instance;
        Player _player;

        Dictionary<int, Player> _otherPlayerDic = new Dictionary<int, Player>();

        #region Property
        public static StageScene Instance
        {
            get
            {
                return _instance;
            }
        }

        public Player Player
        {
            get
            {
                return _player;
            }
        }

        public CinemachineVirtualCamera PlayerCamera
        {
            get
            {
                return _playerCamera;
            }
        }

        public Vector3 PlayerCameraOffset
        {
            get
            {
                return _playerCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
            }
            set
            {
                _playerCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = value;
            }
        }

        // 씬 전환시 Null이 될수있으므로 추가적으로 처리가 필요함
        public Joystick JoyStick
        {
            get
            {
                return _joystick;
            }
        }
        #endregion

        private void Awake()
        {
            _instance = this;
        }

        private void OnDestroy()
        {
            _otherPlayerDic.Clear();
        }

        #region Public Method
        public void Init()
        {
            _playerCamera = StageCameraController.Instance.PlayerCamera;
            _joystick = StageSceneUI.Instance.Joystick;
        }

        public void CreatePlayer()
        {
            /// <summary>
            /// Resources 폴더에 있는 프리팹 이름으로 찾는 방식이라 중복될 경우 에러 발생함
            /// </summary>
            GameObject player = PhotonNetwork.Instantiate(GameManager.Instance.LoginData.PlayerResourceName, SpawnPosition, Quaternion.identity, 0);
            _playerCamera.Follow = player.transform;
            _playerCamera.LookAt = player.transform;

            player.SetActive(true);

            _player = player.GetComponent<Player>();
            _player.Init(GameManager.Instance.LoginData.PlayerData);

            StageSceneUI.Instance.SetNickName();
        }
        
        public void AddOtherPlayer(int photonViewID, Player player)
        {
            if(_otherPlayerDic.ContainsKey(photonViewID))
            {
                _otherPlayerDic[photonViewID] = player;
            }
            else
            {
                _otherPlayerDic.Add(photonViewID, player);
            }
        }

        public void RemoveOtherPlayer(int photonViewID)
        {
            if (_otherPlayerDic.ContainsKey(photonViewID))
                _otherPlayerDic.Remove(photonViewID);
        }

        public Player GetOtherPlayer(int photonViewID)
        {
            if (_otherPlayerDic.ContainsKey(photonViewID))
                return _otherPlayerDic[photonViewID];

            return null;
        }
        #endregion
    }
}