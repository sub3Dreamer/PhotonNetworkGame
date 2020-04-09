using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PUNGame
{
    public class LoginData
    {
        public string PlayerResourceName;
        public PlayerData PlayerData;

        public LoginData(string playerResourceName, PlayerData playerData)
        {
            PlayerResourceName = playerResourceName;
            PlayerData = playerData;
        }
    }
}