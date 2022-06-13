using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ConnectToPhotonManager : MonoBehaviourPunCallbacks
{   

    public TMP_Text buttonText;
    public string username; 

    public void OnClickConnect() {
        Debug.Log("Clicked connect button");
        username = LoadManager.GetInstance().Username;
        Debug.Log(username);
        if (username.Length >= 1) {
            PhotonNetwork.NickName = username;
            Debug.Log("Nickname assigned");
            buttonText.text = "Connecting...";
            Debug.Log("Connect Attempted");
            PhotonNetwork.ConnectUsingSettings();
            Debug.Log("Connect Successful");
        }
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        Debug.Log("Lobby Joined");
    }
        
    
    public override void OnJoinedLobby() 
    {
        SceneManager.LoadScene("Multiplayer Lobby");
        Debug.Log("Moved to Multiplayer");
    }
}
