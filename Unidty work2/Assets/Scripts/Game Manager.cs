using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] double time;
    [SerializeField] double initializeTime;

    [SerializeField] int minute;
    [SerializeField] int second;
    [SerializeField] int millisecond;

    [SerializeField] Text timeText;

    void Start()
    {
        initializeTime = PhotonNetwork.Time;
    }
    
    void Update()
    {
       time = PhotonNetwork.Time - initializeTime;

        minute = (int)time / 60;
        second = (int)time % 60;
        millisecond = (int)(time*100) % 60;

        timeText.text = 
            $"{minute:D2} : {second:D2} : {millisecond:D2}";

    }

}
