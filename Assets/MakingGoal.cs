using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakingGoal : MonoBehaviour
{
    private AudioSource _source;
    private void OnTriggerEnter(Collider collision) {
        if (collision.CompareTag("Ball")) { 
            collision.gameObject.GetComponent<Ball>().Respawn();
           
        }
        
    }
}
