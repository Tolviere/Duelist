using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
using Photon.Realtime;
public class RoomCreator : MonoBehaviourPunCallbacks
{
    public TMP_InputField nameInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateRoom(){

        PhotonNetwork.JoinOrCreateRoom(nameInput.text, new RoomOptions{ MaxPlayers = 4, IsVisible = true}, TypedLobby.Default);
    }
}
