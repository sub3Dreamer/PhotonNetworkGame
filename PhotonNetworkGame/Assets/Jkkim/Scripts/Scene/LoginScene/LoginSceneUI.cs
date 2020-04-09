using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PUNGame
{
    public class LoginSceneUI : MonoBehaviour
    {
        const string PLAYER_DATA_FORMAT = "PlayerData_{0}"; 

        [SerializeField] InputField _nickNameField;
        [SerializeField] Dropdown _selectCharacterDropDown;

        [SerializeField] Transform _modelView;

        [SerializeField] Text _txtModelName;
        [SerializeField] Text _txtHpValue;
        [SerializeField] Text _txtAttackValue;
        [SerializeField] Text _txtDesc;

        public List<string> PlayerResourceList = new List<string>();

        Dictionary<string, Player> _playerDic = new Dictionary<string, Player>();
        Dictionary<string, PlayerDataScriptableObject> _playerDataDic = new Dictionary<string, PlayerDataScriptableObject>();

        string _currentPlayerResourceName;
        PlayerDataScriptableObject _currentPlayerData;

        private void Awake()
        {
            LoadPlayerResource();
        }

        private void Start()
        {
            SetPlayerUI();
        }

        void LoadPlayerResource()
        {
            for (int i = 0; i < PlayerResourceList.Count; i++)
            {
                var playerRes = PlayerResourceList[i];

                // 플레이어 오브젝트 로드(리소스 이름 : Player1)
                string playerObjectFileName = playerRes;
                var playerLoad = ResourceLoad.Load<Player>(playerObjectFileName);
                
                if (playerLoad != null)
                {
                    Player playerObject = Instantiate(playerLoad);

                    playerObject.transform.SetParent(_modelView);
                    playerObject.transform.localPosition = Vector3.zero;
                    playerObject.transform.localScale = Vector3.one;
                    playerObject.transform.localRotation = Quaternion.identity;

                    playerObject.gameObject.SetActive(false);

                    if (_playerDic.ContainsKey(playerRes))
                        _playerDic[playerRes] = playerObject;
                    else
                        _playerDic.Add(playerRes, playerObject);
                }

                // 플레이어 데이터 Scriptable Object 로드(리소스 이름 : PlayerData_Player1)
                string playerDataObjectFileName = string.Format(PLAYER_DATA_FORMAT, playerRes);
                PlayerDataScriptableObject playerDataObject = ResourceLoad.Load<PlayerDataScriptableObject>(playerDataObjectFileName);

                if (playerDataObject != null)
                {
                    if (_playerDataDic.ContainsKey(playerRes))
                        _playerDataDic[playerRes] = playerDataObject;
                    else
                        _playerDataDic.Add(playerRes, playerDataObject);
                }
            }
        }

        void SetPlayerUI()
        {
            _selectCharacterDropDown.ClearOptions();

            List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
            foreach(KeyValuePair<string, PlayerDataScriptableObject> playerData in _playerDataDic)
            {
                Dropdown.OptionData option = new Dropdown.OptionData(playerData.Value.ModelName);
                options.Add(option);
            }

            _selectCharacterDropDown.AddOptions(options);

            OnChangeCharacter(0);
        }

        public string GetNickName()
        {
            return _nickNameField.text.Trim();
        }
        

        #region Button Event
        public void OnClickStart()
        {
            if(_currentPlayerData == null)
            {
                CommonDebug.LogError("에러 발생!! 선택된 플레이어 데이터가 없습니다.");
                return;
            }

            var playerData = new PlayerData();
            playerData.NickName = GetNickName();
            playerData.CurrentHp = _currentPlayerData.Hp;
            playerData.MaxHp = _currentPlayerData.Hp;
            playerData.AttackDamage = _currentPlayerData.AttackDamage;

            var loginData = new LoginData(_currentPlayerResourceName, playerData);
            LoginScene.Instance.Login(loginData);
        }


        #endregion

        #region DropDown Event
        public void OnChangeCharacter(int charIndex)
        {
            
            for(int i = 0; i < PlayerResourceList.Count; i++)
            {
                if (_playerDic.ContainsKey(PlayerResourceList[i]))
                    _playerDic[PlayerResourceList[i]].gameObject.SetActive(false);
            }

            var playerResourceName = PlayerResourceList[charIndex];

            if (_playerDic.ContainsKey(playerResourceName) == false)
                return;

            if (_playerDataDic.ContainsKey(playerResourceName) == false)
                return;

            // Set Model
            _playerDic[playerResourceName].gameObject.SetActive(true);

            // Set Ability Info
            var playerData = _playerDataDic[playerResourceName];
            _txtModelName.text = playerData.ModelName;
            _txtHpValue.text = playerData.Hp.ToString();
            _txtAttackValue.text = playerData.AttackDamage.ToString();
            _txtDesc.text = playerData.Desc;

            _currentPlayerResourceName = playerResourceName;
            _currentPlayerData = playerData;
        }

        #endregion
    }
}