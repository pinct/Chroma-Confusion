using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStage : MonoBehaviour
{
    public GameManager gameManager;

    public string nextStage;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Character")
        {
            int stars = 0;

            if (gameManager.time > 100)
            {
                stars = 1;
            }

            else if(gameManager.time > 60 && gameManager.time <= 100)
            {
                stars = 2;
            }

            else if(gameManager.time > 0 && gameManager.time <= 60)
            {
                stars = 3;
            }

            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "Stars", stars);

            if (SceneManager.GetActiveScene().name.Substring(5) == PlayerPrefs.GetInt("HighestStage", 1).ToString())
            {
                PlayerPrefs.SetInt("HighestStage", PlayerPrefs.GetInt("HighestStage", 1) + 1);
            }

            gameManager.ui.CancelInvoke();

            if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "TimeInSeconds"))
            {
                if (gameManager.time < PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "TimeInSeconds"))
                {
                    PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "TimeInSeconds", gameManager.time);
                    PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "H", gameManager.ui.hours);
                    PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "M", gameManager.ui.minutes);
                    PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "S", gameManager.ui.seconds);
                }
            }

            else
            {
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "TimeInSeconds", gameManager.time);
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "H", gameManager.ui.hours);
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "M", gameManager.ui.minutes);
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "S", gameManager.ui.seconds);
            }

            SceneManager.LoadScene("Stage" + nextStage);
        }
    }
}
