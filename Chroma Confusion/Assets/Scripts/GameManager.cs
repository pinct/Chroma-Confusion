using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public string currentStage;

    public int time;

    public GameObject[] allWalls;

    public GameObject[] allButtons;

    public string activatedTimerColor = "";

    public Button currentButtonTimer;

    public UI ui;

    public AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.StopSound("BackgroundMusic");
        AudioManager.PlaySound("LabMusic", false);

        allWalls = GameObject.FindGameObjectsWithTag("Wall");
        allButtons = GameObject.FindGameObjectsWithTag("Button");
        SetupWalls();

        InvokeRepeating("IncrementTime", 0f, 1f);
    }

    public void IncrementTime()
    {
        time += 1;
    }

    public void StopTime()
    {
        CancelInvoke("IncrementTime");
    }

    public void SetupWalls()
    {
        GameObject[] wallsToDisableAtStart = Array.FindAll(allWalls, walls => walls.GetComponent<Wall>().disableOnStart);
        
        for(int i = 0; i < wallsToDisableAtStart.Length; i++)
        {
            wallsToDisableAtStart[i].SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
    }
}
