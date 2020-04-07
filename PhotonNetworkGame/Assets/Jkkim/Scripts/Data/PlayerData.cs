using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PUNGame
{
    [System.Serializable]
    public class PlayerData
    {
        public string NickName;
        public int CurrentHp;
        public int MaxHp;
        public int AttackDamage;

        public PlayerData()
        {

        }
    }
}