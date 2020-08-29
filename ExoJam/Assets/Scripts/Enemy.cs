using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Enemy Movement
    public Vector3 enemyPosition;
    public float enemyX;
    public float enemyY;
    private float speed;
    public float startingPoint;

    public GameObject particleSystem;
    private float score;
    private bool paused;
    private int newScoreMileStone;

    // Particle System
    public ParticleSystem particles;

    private void Start()
    {
        // Setting the score to zero at the start
        score = 0;

        speed = 10;
        paused = false;
    }

    // Generating the random Enemy Position on the top of the screen
    void generateRandomEnemyPosition()
    {
        enemyPosition = new Vector3(Random.Range(-9.0f, 9.0f), 6.0f, -1.0f);
        transform.position = enemyPosition;
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

    // Update is called once per frame
    void Update()
    {
        score += 1 * Time.deltaTime;

        // Increments the scoreMileStone and speed when the score hits a milestone of a thousand
        if ((score % 1000) == 0)
        {
            // speed += 0.5f;
            newScoreMileStone += 1;
        }

        // Decides when pausing the game
        if (paused == true)
        {
            particles.Pause();
            speed = 0;
            score += 0;
        }
        else
        {
            particles.Play();

            speed = 13;

            score += 1 * Time.deltaTime;
        }

        // Decides when to start moving per enemy
        if (score >= startingPoint)
        { 
            enemyX = transform.position.x;
            enemyY = transform.position.y;

            transform.Translate(new Vector2(0, speed * Time.deltaTime));

            if (enemyY <= -6.0f)
            {
                particleSystem.SetActive(false);
            }
            else if (enemyY >= -6.0f)
            {
                particleSystem.SetActive(true);
            }

            if (enemyY <= -6.5f)
            {
                generateRandomEnemyPosition();
            }
        }
    }

    // Moving to the top of the screen when the player is hit
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "Player")
        {
            generateRandomEnemyPosition();
        }
    }
}
