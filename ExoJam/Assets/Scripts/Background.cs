using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Background : MonoBehaviour
{
    private float backgroundPositionX;
    private float backgroundPositionY;
    public float speed;
    private Vector2 startingPosition;
    public bool paused;

    // Start is called before the first frame update
    void Start()
    {
        paused = false;

        backgroundPositionX = transform.position.x;

        // Decides the startingPosition per background
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            startingPosition = new Vector2(backgroundPositionX, 7.5f);
        } else
        {
            startingPosition = new Vector2(backgroundPositionX, 280.8f);
        }
    }

    // Function for the behaviour of the backgrounds in the MainMenu scene
    void OnMenuScreen()
    {
        backgroundPositionY = transform.position.y;

        transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));

        if (backgroundPositionY <= 264.0f)
        {
            transform.position = startingPosition;
        }
    }

    // Functions for the behaviour of the backgrounds in the Game scene
    void OnGameScreen()
    {
        backgroundPositionY = transform.position.y;

        transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));

        if (backgroundPositionY <= -7.5f)
        {
            transform.position = startingPosition;
        }
    }

    // Function for loading the MainMenu
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Scenes/MainMenu");
    }

    // Function for pausing the game
    public void ActionWhenPaused()
    {
        if (paused == false)
        {
            paused = true;
        } else
        {
            paused = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Makes the background stop when paused in-game
        if (paused == true)
        {
            speed = 0;
        } else
        {
            if (SceneManager.GetActiveScene().name == "SampleScene")
            {
                speed = 5;
            } else
            {
                speed = 1;
            }
        }

        // Decides whether to run the behaviour for the Game scene of MainMenu scene
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            OnGameScreen();
        } else
        {
            OnMenuScreen();
        }
    }
}
