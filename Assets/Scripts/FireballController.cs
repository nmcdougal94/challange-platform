using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour {
    // Start location
    private Vector3 location;

    // Distance to destroy
    public int distance = 50;

    //Collider component
    Collider col;

    // Use this for initialization
    void Start ()
    {
        // Sets start location
        location = transform.position;

        // Gets collider of fireball
        col = GetComponent<Collider>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(transform.position,location) > distance)
        {
            Destroy(gameObject);
        }	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Destroys the enemy
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Terrain"))
        {
            // Advance the level
            Destroy(gameObject);
        }

    }
}
