using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PUNGame
{
    public class PlayerStat : MonoBehaviour
    {
        [SerializeField] PlayerData _data;

        #region Property
        public PlayerData Data
        {
            get
            {
                return _data;
            }
        }

        public string NickName
        {
            get
            {
                return _data.NickName;
            }
        }

        public int MaxHp
        {
            get
            {
                return _data.MaxHp;
            }
            set
            {
                _data.MaxHp = value;
            }
        }

        public int CurrentHp
        {
            get
            {
                return _data.CurrentHp;
            }
            set
            {
                _data.CurrentHp = value;
            }
        }

        public int AttackDamage
        {
            get
            {
                return _data.AttackDamage;
            }
        }
        #endregion

        public void SetData(PlayerData playerData)
        {
            _data = playerData;
        }
    }
}