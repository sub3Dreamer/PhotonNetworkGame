using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PUNGame
{
    public class NetworkManager : Photon.PunBehaviour
    {
        const string VERSION = "v1.0";

        public static void Init()
        {
            PhotonNetwork.ConnectUsingSettings(VERSION);
        }

        #region Override Method
        public override void OnConnectedToMaster()
        {
            CommonDebug.Log("NetworkManager >> OnConnectedToMaster : 랜덤 방에 접속을 시도합니다.");
            PhotonNetwork.JoinRandomRoom();
        }

        public override void OnJoinedLobby()
        {
            CommonDebug.Log("NetworkManager >> OnJoinedLobby : 랜덤 방에 접속을 시도합니다.");
            PhotonNetwork.JoinRandomRoom();
        }

        public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
        {
            CommonDebug.Log("NetworkManager >> OnPhotonRandomJoinFailed : 새로운 방을 생성합니다.");
            PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 4 }, null);
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            CommonDebug.Log("NetworkManager >> OnJoinedRoom");

            StageScene.Instance.Init();
            StageScene.Instance.CreatePlayer();

            // RPC
        }

        public override void OnFailedToConnectToPhoton(DisconnectCause cause)
        {
            base.OnFailedToConnectToPhoton(cause);
            CommonDebug.LogError("NetworkManager >> OnFailedToConnectToPhoton");
        }

        public override void OnConnectionFail(DisconnectCause cause)
        {
            base.OnConnectionFail(cause);
            CommonDebug.LogError("NetworkManager >> OnConnectionFail");
        }
        #endregion
    }
}