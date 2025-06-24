using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] double time;
    [SerializeField] double initializeTime;

    [SerializeField] int minute;
    [SerializeField] int second;
    [SerializeField] int millisecond;

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

        Debug.Log
            ($"{minute:D2} : {second:D2} : {millisecond:D2}");

    }

}
