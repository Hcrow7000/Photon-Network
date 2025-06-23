using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using Photon.Pun;
using PlayFab.GroupsModels;
using System;

public class Cursors : MonoBehaviourPun
{
    void Start()
    {   
        SetCursor(false);
       
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
