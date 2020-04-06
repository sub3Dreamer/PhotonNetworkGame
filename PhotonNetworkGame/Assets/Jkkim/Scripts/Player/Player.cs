using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PUNGame
{
    [RequireComponent(typeof(PhotonView), typeof(CharacterController))]
    public class Player : MonoBehaviour
    {
        [SerializeField] PlayerStat _stat;
        [SerializeField] PlayerMove _moveControl;
        [SerializeField] PlayerAttack _attackControl;

        [Header("[Canvas]")]
        [SerializeField] HpGauge _hpGauge;

        PhotonPlayer _photonPlayer;

        public PlayerStat Stat
        {
            get
            {
                return _stat;
            }
        }

        // 순서 처리 제대로 잡아야함..
        private void Start()
        {
            Init();
        }

        public void Init()
        {
            _photonPlayer = PhotonNetwork.player;
            Debug.Log("Init");
        }
        
        #region Public Method
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