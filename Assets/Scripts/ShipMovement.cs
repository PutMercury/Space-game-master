using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public abstract class ShipMovement : MonoBehaviour
{
    public float accelCap;
    public float decelCap;
    public float turnCap;
    public float rollCap;
    public float pitchCap;

    
    

    public Rigidbody rb;

    PhotonView view;

    public void Start()
    {
        

        view = GetComponent<PhotonView>();
        
        

        accelCap = 10f;
        
        decelCap = 10f;

        turnCap = 2f;

        rollCap = 2f;

        pitchCap = 2f;
  
    }



    void Update()
    {

    }


    private void FixedUpdate()
    {
        
        
        

        

        if (view.IsMine)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rb.AddForce(transform.forward * accelCap, ForceMode.Acceleration);  //Responsible for forward acceleration
                

            }

            if (Input.GetKey(KeyCode.S))
            {
                rb.AddForce(-transform.forward * decelCap, ForceMode.Acceleration);       //Responsible for deceleration

            }


            //========================================================================
            if (Input.GetKey(KeyCode.A))
            {
                rb.AddRelativeTorque(Vector3.down * turnCap, ForceMode.Acceleration);
            }
            if (Input.GetKey(KeyCode.D))
            {
                rb.AddRelativeTorque(Vector3.up * turnCap, ForceMode.Acceleration);
            }
            //========================================================================
            if (Input.GetKey(KeyCode.E))
            {
                rb.AddRelativeTorque(Vector3.back * rollCap, ForceMode.Acceleration);
            }
            if (Input.GetKey(KeyCode.Q))
            {
                rb.AddRelativeTorque(Vector3.forward * rollCap, ForceMode.Acceleration);
            }
            //=======================================================================
            if (Input.GetKey(KeyCode.LeftControl))
            {
                rb.AddRelativeTorque(Vector3.left * pitchCap, ForceMode.Acceleration);
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                rb.AddRelativeTorque(Vector3.right * pitchCap, ForceMode.Acceleration);
            }
            //======================================================================
        }









    }
}

