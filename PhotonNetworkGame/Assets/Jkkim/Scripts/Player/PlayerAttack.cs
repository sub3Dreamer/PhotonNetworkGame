using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PUNGame
{
    /// <summary>
    /// SendMessage 방식이라 무조건 최상단 오브젝트에 꽃혀있어야함
    /// </summary>
    public class PlayerAttack : Photon.MonoBehaviour, IPunObservable
    {
        [SerializeField] PhotonView _photonView;
        [SerializeField] Transform _attackPoint;

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

                if (Input.GetKeyDown(KeyCode.K))
                {
                    Debug.Log("유저 아이디 : " + PhotonNetwork.player.UserId);
                    for (int i = 0; i < PhotonNetwork.otherPlayers.Length; i++)
                    {
                        var otherPlayer = PhotonNetwork.otherPlayers[i];
                        Debug.Log("다른 유저 아이디 : " +otherPlayer.UserId);

                    }
                }
            }

            
        }

        void Attack()
        {
            CommonDebug.Log("Attack!!");
            //string callRpcMethodName = "OnAttacked";
            //_photonView.RPC(callRpcMethodName, PhotonTargets.Others, UserInfo.NickName, UserInfo.AttackDamage);

            // 피격당한 오브젝트의 PhotonPlayer데이터를 받아와서 RPC쏘는 방식으로 변경 필요.
            //PhotonPlayer targetPlayer = null;
            //_photonView.RPC(callRpcMethodName, targetPlayer, UserInfo.NickName, UserInfo.AttackDamage);

            CheckHitPlayer();
        }

        void CheckHitPlayer()
        {
            Collider[] hitColliders = Physics.OverlapSphere(_attackPoint.position, 2f);

            for(int i = 0; i < hitColliders.Length; i++)
            {
                PhotonView otherPlayerView = hitColliders[i].gameObject.GetComponent<PhotonView>();
                if(otherPlayerView != null)
                {
                    string callRpcMethodName = "OnAttacked";
                    _photonView.RPC(callRpcMethodName, PhotonTargets.Others, otherPlayerView.viewID, UserInfo.NickName, UserInfo.AttackDamage);
                }
            }
        }

        #region RPC Event
        [PunRPC]
        void OnAttacked(int viewID, string attackUser, int attackDamage)
        {
            if (_photonView.viewID == viewID)
            {
                CommonDebug.Log($"OnAttacked!!! {attackUser}가 나에게 {attackDamage}의 피해를 입혔습니다.");
            }
        }
        #endregion
    }
}
