
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PUNGame
{
    public class GameManager : MonoBehaviour
    {
        static GameManager _instance;
        LoginData _loginData;

        #region Property
        public static GameManager Instance
        {
            get
            {
                return _instance;
            }
        }

        public LoginData LoginData
        {
            get
            {
                return _loginData;
            }
        }
        #endregion

        void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                DestroyImmediate(this.gameObject);
                return;
            }

            // 로그인 씬에서 접속하지 않았으면 강제로 로그인씬으로 이동함
            if (SceneManager.GetActiveScene() != SceneManager.Scene.Login)
                SceneManager.LoadScene(SceneManager.Scene.Login);
        }

        // Stage Scene
        public void Login(LoginData loginData)
        {
            _loginData = loginData;
            NetworkManager.Init();
        }
    }
}
