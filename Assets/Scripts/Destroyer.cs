using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Destroyer : ShipMovement
{
    public FloatVariable desMaxHP;
    public FloatVariable desCurrentHP;
    void Start()
    {
        desMaxHP.value = 100;
        desCurrentHP.value = desMaxHP.value;

        base.Start();

        accelCap = 15f;
        decelCap = 10f;
        turnCap = 2f;
        rollCap = 2f;
        pitchCap = 2f;

        rb.drag = 1f;
    }

    // Start is called before the first frame update
  

    // Update is called once per frame
    void Update()
    {
        
    }
}
