using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PUNGame
{
    [RequireComponent(typeof(PhotonView), typeof(CharacterController))]
    public class Player : Photon.MonoBehaviour, IPunObservable
    {
        [SerializeField] PlayerStat _stat;
        [SerializeField] PlayerMove _moveControl;
        [SerializeField] PlayerAttack _attackControl;

        [Header("[Canvas]")]
        [SerializeField] HpGauge _hpGauge;

        #region Property
        public PlayerStat Stat
        {
            get
            {
                return _stat;
            }
        }
        #endregion

        #region Interface Override

        // PlayerData를 포톤 네트워크에 실시간으로 동기화
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            var playerStat = _stat.Data;

            if (stream.isWriting)
            {
                stream.SendNext(playerStat.NickName);
                stream.SendNext(playerStat.CurrentHp);
                stream.SendNext(playerStat.MaxHp);
                stream.SendNext(playerStat.AttackDamage);

                // 내 Hp 게이지 갱신
                StageSceneUI.Instance.SetHp(playerStat.CurrentHp, playerStat.MaxHp);
                _hpGauge.gameObject.SetActive(false);
            }
            else
            {
                playerStat.NickName = (string)stream.ReceiveNext();
                playerStat.CurrentHp = (int)stream.ReceiveNext();
                playerStat.MaxHp = (int)stream.ReceiveNext();
                playerStat.AttackDamage = (int)stream.ReceiveNext();

                // 피격 유저 Hp 갱신
                _hpGauge.gameObject.SetActive(true);
                _hpGauge.SetGauge(playerStat.CurrentHp, playerStat.MaxHp);
            }
        }

        #endregion

        void Start()
        {
            _hpGauge.gameObject.SetActive(false);
        }

        #region Public Method
        public void Init(PlayerData playerData)
        {
            _stat.SetData(playerData);
        }

        public void OnAttack()
        {
            _attackControl.OnAttack();
        }

        public void SetHp(int currentHp, int maxHp)
        {
            _hpGauge.SetGauge(currentHp, maxHp);
        }
        #endregion

        
    }
}