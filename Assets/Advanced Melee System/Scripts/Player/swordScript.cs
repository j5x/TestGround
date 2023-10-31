using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class swordScript : MonoBehaviour{
    
    [SerializeField] private float postureDMG;
    [SerializeField] UnityEvent OnSuccesFullBlock;
    private void OnCollisionEnter(Collision collision)

    {
        if (collision.collider.CompareTag("Sword"))
        {
            var playerCombat = GameObject.FindGameObjectWithTag("Player").GetComponent<PCombat>();
            if (playerCombat.blocking)
            {
                Debug.Log("Sword is hit");
                playerCombat.ApplyPostureDamage(postureDMG); // Adjust the value as needed
                OnSuccesFullBlock?.Invoke();
            }
        }
    }
}


