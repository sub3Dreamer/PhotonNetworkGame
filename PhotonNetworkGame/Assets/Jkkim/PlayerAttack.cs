using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : Photon.MonoBehaviour, IPunObservable
{
    [SerializeField] PhotonView _photonView;

    #region Interface
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }
    #endregion
    
    void Update()
    {
        if (_photonView.isMine)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Attack();
        }
    }

    void Attack()
    {
        CommonDebug.Log("Attack!!");
        string callRpcMethodName = "OnAttack";
        _photonView.RPC(callRpcMethodName, PhotonTargets.Others);
    }

    #region RPC Event
    [PunRPC]
    void OnAttacked()
    {
        CommonDebug.Log("OnAttacked!!!");
    }
    #endregion
}
