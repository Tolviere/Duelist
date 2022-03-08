using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    void Update(){
        DontDestroyOnLoad(gameObject);
    }
    public override void OnConnectedToMaster(){
        PhotonNetwork.JoinLobby();
        Debug.Log("Connecting to lobby.");
    }

    public override void OnJoinedLobby(){
        SceneManager.LoadScene("Main Menu");
        Debug.Log("Connected to lobby.");
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }
}
