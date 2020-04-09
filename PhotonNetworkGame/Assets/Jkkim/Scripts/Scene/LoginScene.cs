using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PUNGame
{
    public class LoginScene : MonoBehaviour
    {
        [SerializeField] LoginSceneUI _loginUI;

        static LoginScene _instance;

        public static LoginScene Instance
        {
            get
            {
                return _instance;
            }   
        }
        
        private void Awake()
        {
            _instance = this;
        }

        public void Login(LoginData loginData)
        {
            GameManager.Instance.LoadStageScene(loginData);
        }
    }
}