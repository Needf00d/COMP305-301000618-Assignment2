using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class DeathPlaneController : MonoBehaviour
{
    public Transform activeCheckpoint;
    public GameObject player;


    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.transform.position = activeCheckpoint.position;
        }
    }
}
