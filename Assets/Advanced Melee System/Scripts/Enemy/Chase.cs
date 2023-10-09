using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Chase : MonoBehaviour
{
    int MoveSpeed = 4;
    int MaxDist = 10;
    int MinDist = 5;

    private Transform player;
    private Animator _animator;

    void Start()
    {
        
    }

    void Update()
    {
        chasePlayer();


    }

    internal void chasePlayer()
    {
        
        
        transform.LookAt(player.transform.position);
        if (Vector3.Distance(transform.position, player.transform.position) >= MinDist)
        {

            transform.position += transform.forward * MoveSpeed * Time.deltaTime;



            if (Vector3.Distance(transform.position, player.transform.position) <= MaxDist)
            {
                //Here Call any function U want Like Shoot at here or something
            }

        }
    }
}