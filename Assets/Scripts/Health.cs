using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Health : MonoBehaviour
{
    public float maxHealth;
    float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log(currentHealth);
    }
    [PunRPC]
    public void TakeDamage (float dmgAmount)
    {
        Debug.Log(currentHealth);
        Debug.Log("Take damage triggered");
        currentHealth = currentHealth - dmgAmount;
        if (currentHealth <= 0)
        {
            ShipDeath();
        }
    }

    void ShipDeath()
    {
        Destroy(gameObject.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
