using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;
public class RoomListing : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _text;
    
    public void SetRoomInfo(RoomInfo roomInfo){
        _text.text = roomInfo.Name + roomInfo.PlayerCount + "/" + roomInfo.MaxPlayers;
    }
}
