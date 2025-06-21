using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CharacterManager : MonoBehaviourPunCallbacks
{
    [SerializeField] Vector3 direction;
    void Start()
    {
        PhotonNetwork.Instantiate
            ("Character", direction,Quaternion.identity);

    }

    
    
}
