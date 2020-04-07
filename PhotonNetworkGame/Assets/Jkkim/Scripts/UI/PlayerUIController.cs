using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PUNGame
{
    public class PlayerUIController : MonoBehaviour
    {
        [SerializeField] Joystick _joystick;
        [SerializeField] Text _txtNickName;
        [SerializeField] HpGauge _hpGauge;

        static PlayerUIController _instance;

        #region Property
        public static PlayerUIController Instance
        {
            get
            {
                return _instance;
            }
        }

        public Joystick Joystick
        {
            get
            {
                return _joystick;
            }
        }
        #endregion

        void Awake()
        {
            _instance = this;
        }

        public void Init()
        {
            _txtNickName.text = StageScene.Instance.Player.Stat.NickName;
        }

        public void SetHp(int currentHp, int maxHp)
        {
            _hpGauge.SetGauge(currentHp, maxHp);
        }

        #region Button Event
        public void OnClickAttack()
        {
            StageScene.Instance.Player.OnAttack();
        }
        #endregion
    }
}