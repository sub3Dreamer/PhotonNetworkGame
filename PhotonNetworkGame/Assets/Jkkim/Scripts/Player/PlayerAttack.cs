using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PUNGame
{
    public class PlayerAttack : Photon.MonoBehaviour
    {
        [SerializeField] Player _player;

        [SerializeField] Transform _attackPoint;
        [SerializeField] float _attackRange = 2f;

        void Update()
        {
            if (photonView.isMine)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                    Attack();

                if(Input.GetKeyDown(KeyCode.L))
                {
                    CommonDebug.Log(_player.Stat.NickName);
                }

            }
        }

        #region Public Method
        public void OnAttack()
        {
            Attack();
        }
        #endregion

        void Attack()
        {
            CommonDebug.Log("Attack!!");

            Collider[] hitColliders = Physics.OverlapSphere(_attackPoint.position, _attackRange);

            for (int i = 0; i < hitColliders.Length; i++)
            {
                Player otherPlayer = hitColliders[i].gameObject.GetComponent<Player>();
                if (otherPlayer != null)
                {
                    // 공격 당한 유저의 네트워크 아이디
                    int photonViewID = otherPlayer.photonView.viewID;

                    // 공격한 유저(나)
                    var attackUser = StageScene.Instance.Player.Stat.NickName;

                    // 공격 당한 유저
                    var attackedUser = otherPlayer.Stat.NickName;

                    // 공격 데미지
                    var attackDamage = StageScene.Instance.Player.Stat.AttackDamage;

                    photonView.RPC("AttackRPC", PhotonTargets.Others, photonViewID, attackUser, attackedUser, attackDamage);
                }
            }
        }

        #region RPC
        [PunRPC]
        void AttackRPC(int photonViewID, string attackUser, string attackedUser, int attackDamage)
        {
            if (photonView.viewID == photonViewID)
            {
                CommonDebug.Log($"{attackUser}가 {attackedUser}에게 {attackDamage}의 피해를 입혔습니다.");
            }
        }
        #endregion
    }
}
