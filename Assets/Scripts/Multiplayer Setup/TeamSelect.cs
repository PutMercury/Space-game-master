using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TeamSelect : MonoBehaviour
{
    public static int teamNo;
    bool isHost = CreateAndJoinRooms.isHost;

    public void JoinTeam1()
    {
        teamNo = 1;
        PhotonNetwork.LoadLevel("HostReadyUp"); 
    }

    public void JoinTeam2()
    {
        teamNo = 2;
        PhotonNetwork.LoadLevel("HostReadyUp");
    }       
     


}
