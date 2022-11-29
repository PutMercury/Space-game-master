using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public InputField hostInput;
    public InputField joinInput;
    public static bool isHost;

    public void CreateRoom()
    {
        isHost = true;
        PhotonNetwork.CreateRoom(hostInput.text);
        
    }
    
    public void JoinRoom()
    {
        isHost = false;
        PhotonNetwork.JoinRoom(joinInput.text);
        
    }
        
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Choose team");
    }
}
