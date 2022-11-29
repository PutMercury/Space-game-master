using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    int teamNo = TeamSelect.teamNo;

    public GameObject team1Prefab;
    public GameObject team2Prefab;

    public float minX1;
    public float maxX1;

    public float minZ1;
    public float maxZ1;
    //=====================
    public float minX2;
    public float maxX2;

    public float minZ2;
    public float maxZ2;
    //=====================
    private void Start()
    {
        Vector3 randomPositionTeam1 = new Vector3(Random.Range(minX1, maxX1), 5 , Random.Range(minZ1, maxZ1));

        Vector3 randomPositionTeam2 = new Vector3(Random.Range(minX2, maxX2), 5, Random.Range(minZ2, maxZ2));

        Quaternion team1Rotation = new Quaternion(0, 0, 0, 0);

        Quaternion team2Rotation = new Quaternion(0, 90, 0, 0);

        if (teamNo == 1)
        {
            PhotonNetwork.Instantiate(team1Prefab.name, randomPositionTeam1, team1Rotation);
        }
                                                                                                           
        if (teamNo == 2)
        {
            PhotonNetwork.Instantiate(team2Prefab.name, randomPositionTeam2, team2Rotation);
        }


    }



}
