using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PUNGame
{
    [RequireComponent(typeof(PhotonView), typeof(CharacterController))]
    public class Player : MonoBehaviour
    {
        [SerializeField] PlayerMove _moveControl;
        [SerializeField] PlayerAttack _attackControl;

        PhotonPlayer _photonPlayer;

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            _photonPlayer = PhotonNetwork.player;
            Debug.Log("Init");
        }
    }
}