using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    //Speed
    public float speed = 3;

    //Range
    public float rangeY = 2;
        
    //Initial position
    Vector3 initPos;

    //Direction
    int direction = 1;

    //Down speed
    public float dwnSpd = 1.2f;

	// Use this for initialization
	void Start ()
    {
        //Sets initial position
        initPos = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        /*
        //Factor
        float factor = 1;

        //Check direction
        if (direction == -1)
            factor *= 1.2f;
        */

        //Ternary operator
        float factor = direction == -1 ? dwnSpd : 1;
        //Movement
        float movementY = factor * speed * Time.deltaTime * direction;

        //New position
        float newY = transform.position.y + movementY;

        /*
        if (direction == -1)
            movementY *= 1.2f;
        */

        //Check if still in range
        if (Mathf.Abs(newY - initPos.y) > rangeY)
        {
            direction *= -1;
        }
        
        //Else continue moving
        else
        {
            transform.position += new Vector3(0, movementY, 0);
        }



	}
}
