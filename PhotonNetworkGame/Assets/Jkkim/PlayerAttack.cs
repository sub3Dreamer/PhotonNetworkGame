using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : Photon.MonoBehaviour, IPunObservable
{

    #region Interface
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }
    #endregion

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            OnAttack();
    }

    [PunRPC]
    void OnAttack()
    {
        CommonDebug.Log("Attack!!!");
    }
}
