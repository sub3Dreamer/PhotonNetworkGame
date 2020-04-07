using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PUNGame
{
    public class PlayerStat : Photon.MonoBehaviour
    {
        [SerializeField] PlayerData _data;

        public string _testNickName;

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

            photonView.RPC("SetDataRPC", PhotonTargets.Others, photonView.viewID, playerData.NickName);
        }

        #region RPC

        // 이미 접속해있던 애는 데이터 세팅이 안되는 문제가 있음 ㅠㅠ
        [PunRPC]
        void SetDataRPC(int photonViewID, string nickName)
        {
            if(photonView.viewID == photonViewID)
            {
                _data.NickName = nickName;
                _testNickName = nickName;
            }
        }

        #endregion
    }
}