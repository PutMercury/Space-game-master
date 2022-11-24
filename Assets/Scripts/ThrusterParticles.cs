using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;

public class ThrusterParticles : MonoBehaviour
{
    [SerializeField] ParticleSystem thrustFlame;
    public float tailLength = 0.8f;
    ParticleSystem.MainModule length;

    PhotonView view;

    void Start()
    {
        length = thrustFlame.main;
        length.startLifetime = tailLength;
        thrustFlame = GetComponent<ParticleSystem>();
        view = GetComponent<PhotonView>();
    }

    void Update()
    {
        view = GetComponent<PhotonView>();
        
        

        if (view.IsMine)
        {
            length = thrustFlame.main;
            length.startLifetime = tailLength;

            if (Input.GetKey(KeyCode.W))
            {
                tailLength = 2f;
            }
            else
            {
                tailLength = 0.8f;
            }
        }
        

    }



}
