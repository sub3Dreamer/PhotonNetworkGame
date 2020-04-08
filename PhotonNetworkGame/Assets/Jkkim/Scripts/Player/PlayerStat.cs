using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PUNGame
{
    public class PlayerStat : Photon.MonoBehaviour, IPunObservable
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

        #region Interface Override

        // PlayerData를 포톤 네트워크에 실시간으로 동기화
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.isWriting)
            {
                // Class타입은 Serialize가 안됨
                //stream.SendNext(_data);

                stream.SendNext(_data.NickName);
                stream.SendNext(_data.CurrentHp);
                stream.SendNext(_data.MaxHp);
                stream.SendNext(_data.AttackDamage);
            }
            else
            {
                //_data = (PlayerData)stream.ReceiveNext();

                _data.NickName = (string)stream.ReceiveNext();
                _data.CurrentHp = (int)stream.ReceiveNext();
                _data.MaxHp = (int)stream.ReceiveNext();
                _data.AttackDamage = (int)stream.ReceiveNext();
            }
        }

        #endregion

        public void SetData(PlayerData playerData)
        {
            _data = playerData;
        }
    }
}