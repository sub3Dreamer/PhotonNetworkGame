using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PUNGame
{
    public class PlayerStat : Photon.MonoBehaviour
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

            photonView.RPC("SetDataRPC", PhotonTargets.Others, photonView.viewID, playerData.NickName);
        }

        #region RPC
        [PunRPC]
        void SetDataRPC(int photonViewID, string nickName)
        {
            if(photonView.viewID == photonViewID)
            {
                _data.NickName = nickName;
            }
        }

        #endregion
    }
}