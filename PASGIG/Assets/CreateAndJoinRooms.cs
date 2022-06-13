using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    
    public InputField createInput;
    public InputField joinInput;

    public void CreateRoom()
    {
        Debug.Log("The room name is " + createInput.text);
        PhotonNetwork.CreateRoom(createInput.text);
        Debug.Log("Room Created");
    }

    public void JoinRoom()
    {
        Debug.Log("The room you are trying to join is named " + joinInput.text);
        PhotonNetwork.JoinRoom(joinInput.text);
        Debug.Log("Room Joined");
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
        Debug.Log("Level Loaded");
    }
    
}
