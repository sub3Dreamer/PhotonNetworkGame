using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PUNGame
{
    public class PlayerUIController : MonoBehaviour
    {
        [SerializeField] Text _txtNickName;
        [SerializeField] HpGauge _hpGauge;

        static PlayerUIController _instance;

        public static PlayerUIController Instance
        {
            get
            {
                return _instance;
            }
        }

        void Awake()
        {
            _instance = this;
        }

        public void Init()
        {
            _txtNickName.text = GameManager.Instance.Player.Stat.NickName;
        }

        public void SetHp(int currentHp, int maxHp)
        {
            _hpGauge.SetGauge(currentHp, maxHp);
        }

        #region Button Event
        public void OnClickAttack()
        {
            GameManager.Instance.Player.OnAttack();
        }
        #endregion
    }
}