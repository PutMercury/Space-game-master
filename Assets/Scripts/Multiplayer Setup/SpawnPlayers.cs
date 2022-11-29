using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    int teamNo = TeamSelect.teamNo;

    public GameObject team1Prefab;
    public GameObject team2Prefab;

    public float minX;
    public float maxX;

    public float minZ;
    public float maxZ;

    private void Start()
    {
        Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), 5 , Random.Range(minZ, maxZ));
        if (teamNo == 1)
        {
            PhotonNetwork.Instantiate(team1Prefab.name, randomPosition, Quaternion.identity);
        }
                                                                                                            //NOTES... THE SHIP THAT SPAWNS FIRST SPAWNS ON THE CHOOSE TEAM SCENE
        if (teamNo == 2)
        {
            PhotonNetwork.Instantiate(team2Prefab.name, randomPosition, Quaternion.identity);
        }


    }



}
