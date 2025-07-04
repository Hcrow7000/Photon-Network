using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] double time;
    [SerializeField] double initializeTime;

    [SerializeField] int minute;
    [SerializeField] int second;
    [SerializeField] int millisecond;

    [SerializeField] Text timeText;

    [SerializeField] GameObject pasuePanel;

    void Start()
    {
        initializeTime = PhotonNetwork.Time;

        SetCursor(false);
    }
    
    void Update()
    {
       time = PhotonNetwork.Time - initializeTime;

        minute = (int)time / 60;
        second = (int)time % 60;
        millisecond = (int)(time*100) % 60;

        timeText.text = 
            $"{minute:D2} : {second:D2} : {millisecond:D2}";
        if (photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SetCursor(true);

                pasuePanel.SetActive(true);
            }
        }
    }

    public void Continue()
    {

        if (photonView.IsMine)
        {
            SetCursor(false);

            pasuePanel.SetActive(false);
        }
    }

    public void Exit()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel("Lobby");
    }

    private void SetCursor(bool state)
    {
        if (photonView.IsMine)
        {
            Cursor.lockState = (CursorLockMode)
                Convert.ToInt32(!state);

            Cursor.visible = state;
        }

    }

    private void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}
