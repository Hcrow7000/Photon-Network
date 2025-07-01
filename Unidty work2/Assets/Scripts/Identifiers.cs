using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab.ClientModels;
using Photon.Pun;
using UnityEngine.UI;
using PlayFab;
public class Identifiers 
    : MonoBehaviourPunCallbacks
{

    [SerializeField] Text nameText;
    private void Awake()
    {
        Load();       
    }
    void Load()
    {
        PlayFabClientAPI.GetAccountInfo
            (
                new GetAccountInfoRequest(),
                Success,
                Failure

            );
    }

    void Success
        (GetAccountInfoResult getAccountInfoResult)
    {
        if (photonView.IsMine)
        {
            nameText.text = PhotonNetwork
                .LocalPlayer.NickName;
        }
        else
        {
            nameText.text = photonView.Owner.NickName;
        }
  
    }

    void Failure(PlayFabError playFabError)
    {
        Debug.Log
            (playFabError.GenerateErrorReport());

    }
}
