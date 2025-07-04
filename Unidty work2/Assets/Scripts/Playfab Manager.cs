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
        // Name Server���� �ڵ����� Master Sever�� ����
        PhotonNetwork.ConnectUsingSettings();

        // ���� ������ �Ϸ�ǰų� �ð� �ʰ��� �� ���� ���
        while (PhotonNetwork.IsConnectedAndReady == false) 
        {
            yield return null;   
        }
        // Ư�� �κ� �����Ͽ� �����ϴ� �Լ�
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
