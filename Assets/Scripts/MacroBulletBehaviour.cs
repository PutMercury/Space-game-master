using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MacroBulletBehaviour : MonoBehaviour
{
    private PhotonView photonBullet;
    private float startTime;
    private float checkTime;
    [SerializeField] private float maxRange = 10;
    
    public int dmgAmount = 100;
    
    public static GameObject ship;
    
    
    
    


    // Start is called before the first frame update
    void Start()
    {
        photonBullet = GetComponent<PhotonView>();
        startTime = Time.time;
        checkTime = startTime;
        dmgAmount = 100;
        

        
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        photonBullet = GetComponent<PhotonView>();
        if (photonBullet.IsMine)
        {
            if (Time.time - checkTime >= maxRange)
            {
                Debug.Log(gameObject.name);
                PhotonNetwork.Destroy(gameObject);
                
            }
        }
      
    }
    public void OnTriggerEnter(Collider other)
    {
        photonBullet = GetComponent<PhotonView>();
        PhotonNetwork.Destroy(gameObject);
        
        
    }
}
