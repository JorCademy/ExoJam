using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    static bool audioBegin = false;
    new public AudioSource audio;
    string currentScene;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
        Debug.Log(audioBegin);
    }

    void Awake()
    {
        if (!audioBegin)
        {
            audio.Play();
            DontDestroyOnLoad(audio);
            audioBegin = true;
        }
    }
}
