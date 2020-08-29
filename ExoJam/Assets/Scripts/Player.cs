using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Player Movement
    public Rigidbody2D rb;
    private float x;
    public float speed;
    private float z;
    private float moveInput;

    // Player Health
    public Text healthText;
    private int health;

    // Player Score
    public Text highScore;
    public Text amountOfPoints;
    public int amountOfPointsInt;
    public float startingPoints;
    static public float score;
    public int nonStaticScore;
    public Text scoreText;

    // Player pause menu
    public bool paused;
    public GameObject pauseScreen;

    // Player Sprites
    private bool noSpriteYet;
    public SpriteRenderer renderer;
    public Sprite playerSprite1;
    public Sprite playerSprite2;
    public Sprite playerSprite3;
    public Sprite playerSprite4;
    private Sprite activeSprite;
    private string equippedSprite;

    // Particle System
    public ParticleSystem fire;
    public ParticleSystem explosion;

    // Start is called before the first frame update
    void Start()
    {
        // Makes sure that the explosion particles don't emit at the start
        explosion.Pause();

        // Sets the default playerSprite as default at the start of a scene
        activeSprite = playerSprite1;

        // Gets the highscore from the memory
        highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        startingPoints = PlayerPrefs.GetInt("HighScore", 0);
        equippedSprite = PlayerPrefs.GetString("EquippedSprite", activeSprite.name);

        // Sets the PauseScreen to false
        pauseScreen.SetActive(false);

        // Sets the score equal to zero when in the SampleScene
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            score = 0;
        } 
   
        // Sets the health to 10 when starting a new scene
        health = 10;

        rb = GetComponent<Rigidbody2D>();
        z = transform.position.z;
        x = transform.position.x;
    }

    // Function for deciding when to pause
    public void PauseGame()
    {
        if (paused == false)
        {
            paused = true;
        } else
        {
            paused = false;
        }
    }

    // Functions for setting the playerSprite
    public void FirstEquipButton()
    {
        activeSprite = playerSprite1;
        PlayerPrefs.SetString("EquippedSprite", playerSprite1.name);
    }

    public void SecondEquipButton()
    {
        activeSprite = playerSprite2;
        PlayerPrefs.SetString("EquippedSprite", playerSprite2.name);
    }

    public void ThirdEquipButton()
    {
        activeSprite = playerSprite3;
        PlayerPrefs.SetString("EquippedSprite", playerSprite3.name);
    }

    public void FourthEquipButton()
    {
        activeSprite = playerSprite4;
        PlayerPrefs.SetString("EquippedSprite", playerSprite4.name);
    }

    private void Update()
    {
        // Refreshing the HighScore
        highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();

        // Checking which Player Sprite is active
        switch (PlayerPrefs.GetString("EquippedSprite", activeSprite.name).ToString())
        {
            case "StandardPlayerSprite":
                activeSprite = playerSprite1;
                break;
            case "BlueCirclePlayer":
                activeSprite = playerSprite2;
                break;
            case "YellowPlayer":
                activeSprite = playerSprite3;
                break;
            case "LightBluePlayer":
                activeSprite = playerSprite4;
                break;
        }

        // Sets the playerSprite to the decided activeSprite
        renderer.sprite = activeSprite;

        nonStaticScore = (int) score;

        // Sets the pauseScreen to true when paused
        if (paused == true)
        {
            fire.Pause();
            speed = 0;
            score += 0;
            pauseScreen.SetActive(true);
        } else
        {
            fire.Play();

            speed = 15;

            if (health > 0)
            {
                score += 1 * Time.deltaTime;
            }
            
            pauseScreen.SetActive(false);
        }

        // Sets the health amount to zero when in the GameOver scene
        if (SceneManager.GetActiveScene().name == "GameOverScreen")
        {
            health = 0;
        }

        // Makes the score amount go up when not paused and the health is bigger then zero
        if (health > 0 && paused == false)
        {
            score += 1 * Time.deltaTime;
        }

        // Displays the text for the score and health on the screen
        scoreText.text = Mathf.Round(score).ToString();
        healthText.text = health.ToString();

        // Makes the GameOver scene active when the health is equal to zero
        if (health == 0 && SceneManager.GetActiveScene().name != "GameOverScreen")
        {
            SceneManager.LoadScene("GameOverScreen");
        }

        // Sets the highscore
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", (int) score);
        }

        // Sets the amount of points for purchasing sprites
        PlayerPrefs.SetInt("AmountOfPoints", (int) score);
        amountOfPoints.text = Mathf.Round(score).ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        x = transform.position.x;
        z = -1;

        // Player Movement
        moveInput = Input.GetAxis("Horizontal");

        transform.position = new Vector3(x + moveInput * speed * Time.deltaTime, -3.5f, 0);

        if (transform.position.x >= 9)
        {
            transform.position = new Vector3(-9f, -3.5f, -1f);    
        } else if (transform.position.x <= -9)
        {
            transform.position = new Vector3(9f, -3.5f, -1f);
        }

        rb.velocity = new Vector2(speed * moveInput, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Decrements the player health when the player hits an enemy
        if (collision.collider.tag == "Enemy")
        {
            health -= 1;
            explosion.Play();
        }
    }
}
