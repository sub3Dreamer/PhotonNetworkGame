using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PUNGame
{
    /// <summary>
    /// PhotonNetworkInit이랑 꼭 분리시켜야 하는 구조인지 확인이 필요함.
    /// </summary>
    public class NetworkManager : Photon.PunBehaviour
    {
        #region Call RPC Method Name
        readonly string CREATE_PLAYER_RPC = "OnCreatePlayerRPC";
        #endregion

        public static void Init()
        {
            PhotonNetworkInit.Init();
        }

        #region Override Method
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
            PhotonNetworkInit.Init();
        }
        #endregion

        #region RPC Event
        [PunRPC]
        void OnCreatePlayerRPC(string photonViewID, PlayerData playerData)
        {

        }
        #endregion
    }
}