using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : Photon.PunBehaviour
{
    public static void Init()
    {
        PhotonNetworkInit.Init();
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        CommonDebug.Log("NetworkManager >> OnJoinedRoom");

        GameManager.Instance.CreatePlayer();
    }
}