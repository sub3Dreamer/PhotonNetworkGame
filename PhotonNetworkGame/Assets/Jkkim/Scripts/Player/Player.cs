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