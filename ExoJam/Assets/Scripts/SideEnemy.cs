using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideEnemy : MonoBehaviour
{
    // Enemy Movement
    Vector3 startingPosition;
    private float speed;
    private bool startMoving;
    private Vector2 targetPosition;
    private int horizontalSpeed;

    // Deciding the side the certain enemy is on
    public string side;
    
    public GameObject playerObject;
    private bool paused;

    // Start is called before the first frame update
    void Start()
    {
        // Deciding the targetPosition for the enemy to move towards
        targetPosition = DecideRandomPosition();

        // Deciding the speed and the horizontalSpeed
        horizontalSpeed = 1;
        speed = 7;

        // Sets the default state of the game to not paused
        paused = false;

        // Sets the startingPosition per sideEnemy
        if (side == "right")
        {
            startingPosition = new Vector3(9.75f, Random.Range(4.0f, 5.0f), -1.0f);
        } else
        {
            startingPosition = new Vector3(-9.75f, Random.Range(4.0f, 5.0f), -1.0f);
        }
        
        // Start the timer
        StartCoroutine(waitForAction());
    }

    // Function for pausing the game
    public void PauseGame()
    {
        if (paused == false)
        {
            paused = true;
        }
        else
        {
            paused = false;
        }
    }

    // Function for deciding the random position to move towards
    Vector2 DecideRandomPosition()
    {
        Vector2 randomGeneratedPosition = new Vector2(Random.Range(-8.8f, 8.8f), -6.5f);
        return randomGeneratedPosition;
    }

    // Update is called once per frame
    void Update()
    {
        // Sets the speed based on the state of the enemy
        if (paused == true)
        {
            speed = 0;
            horizontalSpeed = 0;
        }
        else
        {
            speed = 7;
            horizontalSpeed = 1;
        }

        // Decides whether the enemy should start moving
        if (startMoving == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (transform.position.y <= -6.5f)
            {
                transform.position = startingPosition;
                startMoving = false;
                StartCoroutine(waitForAction());
            }
        }
    }

    // The timer for controlling the Enemy Movement
    IEnumerator waitForAction()
    {
        yield return new WaitForSeconds(Random.Range(8, 15));

        targetPosition = DecideRandomPosition();
        startMoving = true; 
    }

    // Sets the position to startingPosition when hitting the player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "Player")
        {
            transform.position = startingPosition;
            startMoving = false;
        }
    }
}
