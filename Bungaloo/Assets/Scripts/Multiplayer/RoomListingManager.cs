using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomListingManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    Transform _content;

    [SerializeField]
    GameObject _roomListing;

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log('J');
        foreach(RoomInfo info in roomList){
            Debug.Log("SDFIJPOS");
            RoomListing listing = Instantiate(_roomListing, _content).GetComponent<RoomListing>();
            if(listing != null){
                listing.SetRoomInfo(info);
            }
        }
    }
}
