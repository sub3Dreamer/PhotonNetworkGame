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
            if (stream.isWriting)
            {
                // Class타입은 Serialize가 안됨
                //stream.SendNext(_data);

                stream.SendNext(_stat.Data.NickName);
                stream.SendNext(_stat.Data.CurrentHp);
                stream.SendNext(_stat.Data.MaxHp);
                stream.SendNext(_stat.Data.AttackDamage);
            }
            else
            {
                //_data = (PlayerData)stream.ReceiveNext();

                var playerStat = _stat.Data;

                playerStat.NickName = (string)stream.ReceiveNext();
                playerStat.CurrentHp = (int)stream.ReceiveNext();
                playerStat.MaxHp = (int)stream.ReceiveNext();
                playerStat.AttackDamage = (int)stream.ReceiveNext();

                _hpGauge.SetGauge(playerStat.CurrentHp, playerStat.MaxHp);
            }
        }

        #endregion

        #region Public Method
        public void Init(PlayerData playerData)
        {
            _stat.SetData(playerData);
            _hpGauge.gameObject.SetActive(!photonView.isMine);
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