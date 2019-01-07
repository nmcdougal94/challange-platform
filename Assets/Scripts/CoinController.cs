using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour {

    //rotation speed
    public float rotationSpeed = 100f;
	
	// Update is called once per frame
	void Update () {
        float angRot = rotationSpeed * Time.deltaTime;

        transform.Rotate(Vector3.up * angRot, Space.World);
	}
}
