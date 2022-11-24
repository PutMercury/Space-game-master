using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cruiser : ShipMovement
{
    public FloatVariable desMaxHP;
    public FloatVariable desCurrentHP;

    // Start is called before the first frame update
    void Start()
    {
        desMaxHP.value = 200;
        desCurrentHP.value = desMaxHP.value;

        base.Start();

        accelCap = 10f;
        decelCap = 8f;
        turnCap = 1f;
        rollCap = 1f;
        pitchCap = 1f;

        rb.drag = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(rb.velocity);
    }
}
