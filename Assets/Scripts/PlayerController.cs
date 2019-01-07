using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    //Walking speed
    public float walkSpd;

    //Jump speed
    public float jumpSpd;

    //Coin collection sound
    public AudioSource coinSound;

    //Rigidbody component
    Rigidbody rb;

    //Collider component
    Collider col;

    //flag to keep track of jumping
    bool pressedJump = false;

    //Size of player
    Vector3 size;

    // Fireball
    public GameObject fireballPrefab;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        //get player size
        size = col.bounds.size;

	}

    void FixedUpdate()
    {
        WalkHandler();
        jumpHandler();
        shootFireball();
    }

    void WalkHandler()
    {
        //Input on x axis
        float hAxis = Input.GetAxis("Horizontal");

        //Input on z axis
        float vAxis = Input.GetAxis("Vertical");

        //Input on j axis
        //float jAxis = Input.GetAxis("Jump");
        
        //Calculate movement values
        //float jMove = jAxis * jumpSpd * Time.deltaTime;
        float hMove = hAxis * walkSpd * Time.deltaTime;
        float vMove = vAxis * walkSpd * Time.deltaTime;

        //New position
        //Vector3 newPos = transform.position + new Vector3(hMove, jMove, vMove);
        Vector3 newPos = transform.position + new Vector3(hMove, 0, vMove);

        //Move
        rb.MovePosition(newPos);
    }

    
    void jumpHandler()
    {
        //Input for jumping
        float jAxis = Input.GetAxis("Jump");

        bool isGrounded = CheckGrounded();
        
        if(jAxis > 0)
        {
            if(!pressedJump && isGrounded)
            {
                pressedJump = true;
                //Jumping vector
                Vector3 jumpVector = new Vector3(0, jAxis * jumpSpd, 0);

                //Add force
                rb.AddForce(jumpVector, ForceMode.VelocityChange);
            }
        }
        else
        {
            //set flag to false
            pressedJump = false;
        }
    }

    bool CheckGrounded()
    {
        //location of all 4 corners
        Vector3 corner1 = transform.position + new Vector3(size.x / 2, -size.y / 2 + 0.01f, size.z / 2);
        Vector3 corner2 = transform.position + new Vector3(-size.x / 2, -size.y / 2 + 0.01f, size.z / 2);
        Vector3 corner3 = transform.position + new Vector3(size.x / 2, -size.y / 2 + 0.01f, -size.z / 2);
        Vector3 corner4 = transform.position + new Vector3(-size.x / 2, -size.y / 2 + 0.01f, -size.z / 2);

        //check if the player is grounded
        bool grounded1 = Physics.Raycast(corner1, -Vector3.up, 0.02f);
        bool grounded2 = Physics.Raycast(corner2, Vector3.down, 0.02f);
        bool grounded3 = Physics.Raycast(corner3, Vector3.down, 0.02f);
        bool grounded4 = Physics.Raycast(corner4, Vector3.down, 0.02f);

        return (grounded1 || grounded2 || grounded3 || grounded4);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Coin"))
        {
            // Increase player score
            GameManager.instance.increaseScore(1);

            //Play Sound
            coinSound.Play();
                  
            //Destroy the coin
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Enemy"))
        {
            // Reset the game
            GameManager.instance.ResetLevel();
        }
        else if (other.CompareTag("Goal"))
        {
            // Advance the level
            GameManager.instance.AdvanceLevel();
        }

    }

    void shootFireball()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            Vector3 location = transform.position + new Vector3(0.2f, 0, 1f);

            var fireball = Instantiate(fireballPrefab, location, transform.rotation);

            fireball.GetComponent<Rigidbody>().velocity = fireball.transform.forward * 6;
            fireball.transform.eulerAngles = new Vector3(0, -90f, 0);
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            Vector3 location = transform.position + new Vector3(-0.2f, 0, -1f);

            var fireball = Instantiate(fireballPrefab, location, transform.rotation);

            fireball.GetComponent<Rigidbody>().velocity = fireball.transform.forward * -6;
            fireball.transform.eulerAngles = new Vector3(0, 90f, 0);
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            Vector3 location = transform.position + new Vector3(-2f, 0, 0f);

            var fireball = Instantiate(fireballPrefab, location, transform.rotation);

            fireball.GetComponent<Rigidbody>().velocity = fireball.transform.right * -6;
            fireball.transform.eulerAngles = new Vector3(0, 180f, 0);
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            Vector3 location = transform.position + new Vector3(1f, 0, -0.2f);

            var fireball = Instantiate(fireballPrefab, location, transform.rotation);

            fireball.GetComponent<Rigidbody>().velocity = fireball.transform.right * 6;
        }


    }
}
