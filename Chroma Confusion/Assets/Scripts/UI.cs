using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    public TextMeshProUGUI resetText;

    public TextMeshProUGUI levelTime;

    [HideInInspector] public int seconds = 0;
    [HideInInspector] public int minutes = 0;
    [HideInInspector] public int hours = 0;

    public float timeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        timerText.text = "Timer: Not Active";

        if(resetText != null)
        {
            StartCoroutine(ShowResetText());
        }

        UpdateLevelTime();
        InvokeRepeating("IncrementTime", 0f, 1f / timeSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IncrementTime()
    {
        if(seconds < 59)
        {
            seconds++;
        }

        else
        {
            seconds = 0;

            if(minutes < 59)
            {
                minutes++;
            }

            else
            {
                minutes = 0;
                hours++;
            }
        }

        UpdateLevelTime();
    }

    void UpdateLevelTime()
    {
        levelTime.text = $"Time: {hours:00}h : {minutes:00}m : {seconds:00}s";
    }

    IEnumerator ShowResetText()
    {
        yield return new WaitForSeconds(5f);
        resetText.gameObject.SetActive(false);
    }
}
