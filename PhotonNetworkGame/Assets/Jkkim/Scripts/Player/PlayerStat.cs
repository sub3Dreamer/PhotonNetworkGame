using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PUNGame
{
    public class PlayerStat : MonoBehaviour
    {
        [SerializeField] string _nickName;
        [SerializeField] int _hp;
        [SerializeField] int _attackDamage;

        #region Property
        public string NickName
        {
            get
            {
                return _nickName;
            }
        }

        public int Hp
        {
            get
            {
                return _hp;
            }
            set
            {
                _hp = value;
            }
        }

        public int AttackDamage
        {
            get
            {
                return _attackDamage;
            }
        }
        #endregion

        public void SetData(string nickName, int hp, int attackDamage)
        {
            _nickName = nickName;
            _hp = hp;
            _attackDamage = attackDamage;
        }
    }
}