using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public GameObject startButton;
    public GameObject[] enemies;
    public WantedSystem wantedSystem;

    void Start()
    {
        wantedSystem = GameObject.FindGameObjectWithTag("WantedSystem").GetComponent<WantedSystem>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    public void StartGame()
    {
        // spawn enemies, assign their id's
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].SetActive(true);
        }

        // set target
        wantedSystem.SetTarget();

        // hide start button
        gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(currentScene.name);
    }
}