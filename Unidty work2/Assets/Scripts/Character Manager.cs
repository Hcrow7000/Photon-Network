using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CharacterManager : MonoBehaviourPunCallbacks
{
    [SerializeField] List<Transform> transformList;
    
    void Start()
    {
        if (photonView.IsMine)
        {
            int index = PhotonNetwork.CurrentRoom.PlayerCount - 1;

            PhotonNetwork.Instantiate
                ("Character", transformList[index].position, Quaternion.identity);
        }
    }

}