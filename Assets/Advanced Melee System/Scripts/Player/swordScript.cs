using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordScript : MonoBehaviour{
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Sword"))
        {
            var playerCombat = GameObject.FindGameObjectWithTag("Player").GetComponent<PCombat>();
            if (playerCombat.blocking)
            {
                Debug.Log("Sword is hit");
                playerCombat.ApplyPostureDamage(25.0f); // Adjust the value as needed
            }
        }
    }
}


