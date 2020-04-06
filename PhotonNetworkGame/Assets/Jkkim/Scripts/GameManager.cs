using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PUNGame
{
    public class GameManager : MonoBehaviour
    {
        const string PLAYER_RESOURCE_NAME = "TestPlayer";
        
        [SerializeField] CinemachineVirtualCamera _playerCamera;
        [SerializeField] Joystick _joystick;

        [Header("[UserInfo(Temp)]")]
        public string _userNickName;
        public int _userHp;
        public int _userAttackDamage;

        // 나중에 구조화
        public Vector3 SpawnPosition = new Vector3(0f, 3f, 0f);

        static GameManager _instance;
        Player _player;

        #region Property
        public static GameManager Instance
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

        void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                DestroyImmediate(this.gameObject);
                return;
            }

            Init();
        }

        void Init()
        {
            NetworkManager.Init();
        }

        public void CreatePlayer()
        {
            /// <summary>
            /// Resources 폴더에 있는 프리팹 이름으로 찾는 방식이라 중복될 경우 에러 발생함
            /// </summary>
            GameObject player = PhotonNetwork.Instantiate(PLAYER_RESOURCE_NAME, SpawnPosition, Quaternion.identity, 0);
            _playerCamera.Follow = player.transform;
            _playerCamera.LookAt = player.transform;

            UserInfo.NickName = _userNickName;
            UserInfo.Hp = _userHp;
            UserInfo.AttackDamage = _userAttackDamage;

            player.SetActive(true);
            
            _player = player.GetComponent<Player>();

            // Player.Init()과 싱크 맞춰야함.. 실행순서 똑바로
            // _player.Init();
            PlayerUIController.Instance.Init();
        }
    }
}
