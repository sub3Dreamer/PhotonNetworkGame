using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PUNGame
{
    /// <summary>
    /// SendMessage 방식이라 무조건 최상단 오브젝트에 꽃혀있어야함
    /// </summary>
    public class PlayerAttack : Photon.MonoBehaviour
    {
        [SerializeField] PhotonView _photonView;
        [SerializeField] Transform _attackPoint;
        [SerializeField] float _attackRange = 2f;

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

            string callRpcMethodName = "OnAttackedRPC";
            
            Collider[] hitColliders = Physics.OverlapSphere(_attackPoint.position, _attackRange);

            for (int i = 0; i < hitColliders.Length; i++)
            {
                PhotonView otherPlayer = hitColliders[i].gameObject.GetComponent<PhotonView>();
                if (otherPlayer != null)
                {
                    // 피격이 적중한 유저의 네트워크 아이디
                    int attackedUserNetworkID = otherPlayer.viewID;

                    _photonView.RPC(callRpcMethodName, PhotonTargets.Others, attackedUserNetworkID, UserInfo.NickName, UserInfo.AttackDamage);
                }
            }
        }

        #region Public Method
        public void OnAttack()
        {
            Attack();
        }
        #endregion

        #region RPC Event
        [PunRPC]
        void OnAttackedRPC(int attackedUserNetworkID, string attackUser, int attackDamage)
        {
            if (_photonView.viewID == attackedUserNetworkID)
            {
                CommonDebug.Log($"OnAttacked!!! {attackUser}가 나에게 {attackDamage}의 피해를 입혔습니다.");

                //int currentHp = 
                //PlayerUIController.Instance.SetHp()
            }
        }
        #endregion
    }
}
