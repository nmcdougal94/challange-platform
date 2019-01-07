using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    
    //score of player
    public int score = 0;

    // High Score
    public int highScore = 0;

    // Level start Score
    public int startScore = 0;

    // Current level
    public int currentLevel = 1;

    // How many levels there are
    public int highestLevel = 2;

    // Default number of lives
    public int numLives = 3;

    // Number of lives
    public int lives;

    // Mana
    public int mana;

    // Variable to access HUD Manager
    HUDManager HUDManager;

    // static instance of GM that can be accessed anywhere
    public static GameManager instance;

    void Awake()
    {
        lives = numLives;

        // Check that the GM has been assigned
        if (instance == null)
        {
            // Assign it to the current object
            instance = this;
        }
        // Make sure that instance is the current game object
        else if (instance != this)
        {
            // Destroy the current game object
            Destroy(gameObject);
        }

        // Don't destroy the object when changing scene
        DontDestroyOnLoad(gameObject);

        // Find object of type HUDManager
        HUDManager = FindObjectOfType<HUDManager>();
    }

    // Increase the player score
    public void increaseScore (int amount)
    {
        // Increase score by amount
        score += amount;

        print("Score is now: " + score);

        // Have we surpassed our high score?
        if (score > highScore)
        {
            highScore = score;
            print("New high score of " + score + "!");
        }

        // Update the HUD
        if (HUDManager != null)
        {
            HUDManager.ResetHUD();
        }
        else
        {
            print("Hud Manager is null");
        }
    }

    public void ResetLevel()
    {
        score = startScore;

        print("Score is now: " + score);

        // Update the HUD
        if (HUDManager != null)
        {
            HUDManager.ResetHUD();
        }
        else
        {
            print("Hud Manager is null");
        }

        if (lives > 0)
        {
            lives--;
            print("You lost a life. You now have " + lives + " lives left.");
            SceneManager.LoadScene("Level" + currentLevel);
        }
        else
        {
            // No more lives, reset game
            print("Out of lives, game over :(");

            ResetGame();
        }
            
    }

    // Reset the game
    public void ResetGame()
    {
        // Reset the score
        score = 0;

        // Reset the HUD
        if (HUDManager != null)
        {
            HUDManager.ResetHUD();
        }
        else
        {
            print("Hud Manager is null");
        }

        // Reset lives
        lives = numLives;

        // Reset the level
        currentLevel = 1;

        // Load the first level
        SceneManager.LoadScene("Level1");
    }

    // Send the player to the next level
    public void AdvanceLevel()
    {
        startScore = score;
        // Check if there are more levels
        if (currentLevel < highestLevel)
        {
            // Increase currentl level by 1
            currentLevel++;
        }
        else
        {
            // Send the player back to level 1
            currentLevel = 1;
        }
       
        SceneManager.LoadScene("Level" + currentLevel);
    }
}
