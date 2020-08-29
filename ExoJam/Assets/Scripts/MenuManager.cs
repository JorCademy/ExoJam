using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Controls")
        {
            StartCoroutine(ToNextScene());
        }
    }

    // Function for reloading the GameScene
    public void ReloadScene()
    {
        SceneManager.LoadScene("Scenes/SampleScene");
    }

    public void ToControlsScene()
    {
        SceneManager.LoadScene("Scenes/Controls");
    }

    // Function for loading the ShopScene
    public void ToStoreScene()
    {
        SceneManager.LoadScene("Scenes/Shop");
    }

    // Function for loading the MainMenu scene
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Scenes/MainMenu");
    }

    public void Update()
    {
        if (SceneManager.GetActiveScene().name == "Controls")
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ReloadScene();
            }
        }
    }

    IEnumerator ToNextScene()
    {
        yield return new WaitForSeconds(5);
        ReloadScene();
    }
}
