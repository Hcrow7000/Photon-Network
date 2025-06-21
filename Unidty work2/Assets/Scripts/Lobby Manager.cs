using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
using UnityEngine.UIElements;


public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform parentTransforms;

    [SerializeField] Dictionary<string, GameObject>
        dictioanry = new Dictionary<string, GameObject>();

    public void CreateRoomPanel()
    {
        
    }

    public void OnCreateRoom()
    {
        RoomOptions roomOptions 
            = new RoomOptions();

        roomOptions.MaxPlayers = 4;

        roomOptions.IsOpen = true;

        roomOptions.IsVisible = true;
        
        PhotonNetwork.CreateRoom("Battle",roomOptions);

    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        GameObject prefab = null;

        foreach(RoomInfo room in roomList)
        {
            // ���� ������ ���
            if (room.RemovedFromList == true)
            {
                dictioanry.TryGetValue(room.Name, out prefab);

                Destroy(prefab);

                dictioanry.Remove(room.Name);
            }
            else // ���� ������ ����Ǵ� ���
            {
                // ���� ó�� �����Ǵ� ���
                if(dictioanry.ContainsKey(room.Name) == false)
                {
                    GameObject clone = Instantiate
                        (Resources.Load<GameObject>("Room")
                        , parentTransforms);

                    clone.GetComponent<Information>().
                        Details(room.Name, room.PlayerCount,
                        room.MaxPlayers);

                    dictioanry.Add(room.Name, clone);

                }
                else // ���� ���ŵǾ��� ��
                {
                    dictioanry.TryGetValue (room.Name, out prefab);

                    prefab.GetComponent<Information>().
                        Details(room.Name, room.PlayerCount,
                        room.MaxPlayers);

                }
            }
            
            

        }

    }
}
