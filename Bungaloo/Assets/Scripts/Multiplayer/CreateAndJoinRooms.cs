using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    bool inMainMenu = true;

    Button createButton;
    void Start()
    {
        
    }
/*
    void Update(){
        if(inMainMenu){
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

        if(SceneManager.GetActiveScene().name == "Host Menu"){
            createButton = GameObject.FindGameObjectWithTag("Create Button").GetComponent<Button>();
        }

        if(SceneManager.GetActiveScene().name == "Server Browser"){
            foreach (RoomInfo game in PhotonNetwork.GetRoomList()) {
                debug.log (game.name);
                debug.log (game.PlayerCount);
                debug.log (game.MaxPlayers);
            }
        }*/
    

    public void JoinRoom(){
        PhotonNetwork.JoinRandomOrCreateRoom();
    }

/*    public void CreateRoom(){
        inMainMenu = false;
    }*/
}
