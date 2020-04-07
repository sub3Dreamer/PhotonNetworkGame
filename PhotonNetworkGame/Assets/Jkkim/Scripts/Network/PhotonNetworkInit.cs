using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PUNGame
{
    /// <summary>
    /// NetworkManager랑 꼭 분리시켜야 하는 구조인지 확인이 필요함.
    /// </summary>
    public class PhotonNetworkInit : Photon.MonoBehaviour
    {
        public static void Init()
        {
            string gameVersion = "v1.0";
            PhotonNetwork.ConnectUsingSettings(gameVersion);
        }

        #region PUN's SendMessage
        public virtual void OnConnectedToMaster()
        {
            CommonDebug.Log("PhotonNetworkInit >> OnConnectedToMaster : 랜덤 방에 접속을 시도합니다.");
            PhotonNetwork.JoinRandomRoom();
        }

        public virtual void OnJoinedLobby()
        {
            CommonDebug.Log("PhotonNetworkInit >> OnJoinedLobby : 랜덤 방에 접속을 시도합니다.");
            PhotonNetwork.JoinRandomRoom();
        }

        public virtual void OnPhotonRandomJoinFailed()
        {
            CommonDebug.Log("PhotonNetworkInit >> OnPhotonRandomJoinFailed : 새로운 방을 생성합니다.");
            PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 4 }, null);
        }

        // the following methods are implemented to give you some context. re-implement them as needed.

        public virtual void OnFailedToConnectToPhoton(DisconnectCause cause)
        {
            CommonDebug.LogError("PhotonNetworkInit >> OnFailedToConnectToPhoton : Cause: " + cause);
        }

        public void OnJoinedRoom()
        {
            CommonDebug.Log("PhotonNetworkInit >> OnJoinedRoom");
        }
        #endregion
    }
}
