using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PUNGame
{
    [RequireComponent(typeof(PhotonView), typeof(CharacterController))]
    public class Player : Photon.MonoBehaviour
    {
        [SerializeField] PlayerStat _stat;
        [SerializeField] PlayerMove _moveControl;
        [SerializeField] PlayerAttack _attackControl;

        [Header("[Canvas]")]
        [SerializeField] HpGauge _hpGauge;

        PhotonPlayer _photonPlayer;

        #region Property
        public PlayerStat Stat
        {
            get
            {
                return _stat;
            }
        }
        #endregion

        #region Public Method
        public void Init(PlayerData playerData)
        {
            _photonPlayer = PhotonNetwork.player;
            _stat.SetData(playerData);
            Debug.Log("Player >> Init");

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