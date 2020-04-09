using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PUNGame
{
    public class PlayerAttack : Photon.MonoBehaviour
    {
        [SerializeField] Player _player;

        [SerializeField] Transform[] _attackPoints;
        [SerializeField] float _attackRange = 2f;

        void Update()
        {
            if (photonView.isMine)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                    Attack();
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

            List<Player> hitPlayerList = new List<Player>();

            for(int i = 0; i < _attackPoints.Length; i++)
            {
                var attackPoint = _attackPoints[i];

                Collider[] hitColliders = Physics.OverlapSphere(attackPoint.position, _attackRange);

                for (int j = 0; j < hitColliders.Length; j++)
                {
                    Player hitPlayer = hitColliders[j].gameObject.GetComponent<Player>();
                    if (hitPlayer != null)
                    {
                        // 충돌체의 viewID가 나일경우 패스  
                        if (hitPlayer.photonView.viewID == photonView.viewID)
                            continue;

                        if (hitPlayerList.Contains(hitPlayer))
                            continue;

                        hitPlayerList.Add(hitPlayer);
                    }
                }
            }

            for(int i = 0; i < hitPlayerList.Count; i++)
            {
                var hitPlayer = hitPlayerList[i];

                var player = StageScene.Instance.Player;

                // 공격한 유저(나)
                var attackUser = player.Stat.NickName;

                // 공격 데미지
                var attackDamage = player.Stat.AttackDamage;

                // 피격 유저에게 RPC를 전송함
                hitPlayer.photonView.RPC("AttackRPC", PhotonTargets.Others, attackUser, hitPlayer.Stat.NickName, attackDamage);
            }
        }

        #region RPC
        [PunRPC]
        void AttackRPC(string attackUser, string attackedUser, int attackDamage)
        {
            CommonDebug.Log($"AttackRPC!! {attackUser}가 {attackedUser}에게 {attackDamage}의 피해를 입혔습니다.");

            var playerStat = _player.Stat;
            playerStat.CurrentHp = Mathf.Clamp(playerStat.CurrentHp - attackDamage, 0, playerStat.MaxHp);
        }
        #endregion
    }
}
