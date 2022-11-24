using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battleship : ShipMovement
{
    public FloatVariable desMaxHP;
    public FloatVariable desCurrentHP;
    
    // Start is called before the first frame update
    void Start()
    {
        desMaxHP.value = 300;
        desCurrentHP.value = desMaxHP.value;

        base.Start();

        accelCap = 10f;
        decelCap = 6f;
        turnCap = 0.5f;
        rollCap = 0.5f;
        pitchCap = 0.5f;

        rb.drag = 1.2f;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(rb.velocity);
    }
}
