using System.Collections;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using PlayFab.ClientModels;
using PlayFab;

public class Playfab : MonoBehaviourPunCallbacks
{
    [SerializeField] string version;

    [SerializeField] InputField emailInputField;
    [SerializeField] InputField passwordInputFiled;

    public void Success(LoginResult loginResult)
    {
        PhotonNetwork.AutomaticallySyncScene = false;

        PhotonNetwork.GameVersion = version;

        StartCoroutine(Connect());

    }

    IEnumerator Connect()
    {
        // Name Server에서 자동으로 Master Sever로 연결
        PhotonNetwork.ConnectUsingSettings();

        // 서버 연결이 완료되거나 시간 초과될 때 까지 대기
        while (PhotonNetwork.IsConnectedAndReady == false) 
        {
            yield return null;   
        }
        // 특정 로비를 생성하여 진입하는 함수
        PhotonNetwork.JoinLobby();

    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.LoadLevel("Lobby");
    }

    public void Access()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = emailInputField.text,
            Password = passwordInputFiled.text,
        };

        PlayFabClientAPI.LoginWithEmailAddress
        (
            request,
            Success,
            Failure
        );
    } 

    void Failure(PlayFabError playFabError)
    {
        Debug.Log(playFabError.GenerateErrorReport());
    }
}
