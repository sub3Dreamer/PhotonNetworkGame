using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    const string PLAYER_RESOURCE_NAME = "TestPlayer";

    //[SerializeField] SmoothFollow _playerCamera;
    [SerializeField] CinemachineVirtualCamera _playerCamera;

    // 나중에 구조화
    public Vector3 SpawnPosition = new Vector3(0f, 3f, 0f);

    static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

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
        //_playerCamera.target = player.transform;
    }
}
