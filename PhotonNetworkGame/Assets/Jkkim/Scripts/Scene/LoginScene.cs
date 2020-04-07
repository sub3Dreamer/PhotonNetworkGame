using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PUNGame
{
    public class LoginScene : MonoBehaviour
    {
        [SerializeField] InputField _nickNameField;

        LoginData _data;

        void CreateData()
        {
            _data = new LoginData();
            var playerData = new PlayerData();
            playerData.NickName = _nickNameField.text.Trim();
            playerData.CurrentHp = 50;
            playerData.MaxHp = 50;
            playerData.AttackDamage = 5;

            _data.PlayerData = playerData;
        }

        #region Button Event
        public void OnClickStart()
        {
            CreateData();
            SceneManager.LoadScene(SceneManager.Scene.Stage);
            GameManager.Instance.Login(_data);
        }
        #endregion
    }
}