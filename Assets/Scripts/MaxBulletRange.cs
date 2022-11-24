using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MaxBulletRange : MonoBehaviour
{
    private PhotonView photonBullet;
    private float startTime;
    private float checkTime;
    [SerializeField] private float maxRange = 3;



    // Start is called before the first frame update
    void Start()
    {
        photonBullet = GetComponent<PhotonView>();
        startTime = Time.time;
        checkTime = startTime;
       
        

        
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (photonBullet.IsMine)
        {
            if (Time.time - checkTime >= maxRange)
            {
                Debug.Log(gameObject.name);
                PhotonNetwork.Destroy(this.gameObject);
                
            }
        }
      
    }
}
