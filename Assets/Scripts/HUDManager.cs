using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

    // Score text label
    public Text scoreLabel;
        
    // Use this for initialization
    void Start ()
    {
        // Start of game
        ResetHUD();		
	}

    // Show up to date stats of the player
    public void ResetHUD()
    {
        scoreLabel.text = "Score: " + GameManager.instance.score;
    }

}
