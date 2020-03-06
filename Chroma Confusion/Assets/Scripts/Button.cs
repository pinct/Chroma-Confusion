using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameManager gameManager;

    public UI ui;

    Coroutine lastRoutine = null;

    // The color of the button //
    public string buttonColor;

    // The list of walls that this button can toggle //
    public List<Wall> walls;

    public CharacterMovement characterMovement;

    [Header("Optional")]
    public int timerTime;
    public bool timerShouldBeActive;

    public enum ButtonType
    {
        Normal,
        Timed,
        Pressure
    }

    public ButtonType buttonType;

    private void Start()
    {
        walls = new List<Wall>();

        for(int i = 0; i < gameManager.allWalls.Length; i++)
        {
            if(gameManager.allWalls[i].GetComponent<Wall>().wallColor == buttonColor)
            {
                walls.Add(gameManager.allWalls[i].GetComponent<Wall>());
            }
        }
    }

    public void ToggleWalls()
    {
        foreach (Wall wall in walls)
        {
            wall.gameObject.SetActive(!wall.gameObject.activeSelf);
        }
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (buttonType == ButtonType.Normal)
        {
            ToggleWalls();
            AudioManager.PlaySound("ButtonSound", true);
            GetComponent<Animator>().Play("ButtonBop");
        }

        else if (buttonType == ButtonType.Timed)
        {
            if (!timerShouldBeActive)
            {
                ToggleWalls();

                for (int i = 0; i < gameManager.allButtons.Length; i++)
                {
                    if(gameManager.allButtons[i].GetComponent<Button>().buttonColor != buttonColor)
                    {

                        gameManager.allButtons[i].GetComponent<Button>().timerShouldBeActive = false;
                    }
                    else
                    {
                        gameManager.allButtons[i].GetComponent<Button>().timerShouldBeActive = true;
                    }
                }

                if (gameManager.activatedTimerColor == string.Empty)
                {
                    timerShouldBeActive = true;

                    RefreshTimer();
                    gameManager.activatedTimerColor = buttonColor;
                    gameManager.currentButtonTimer = this;
                }

                else if (gameManager.activatedTimerColor != buttonColor)
                {
                    gameManager.currentButtonTimer.CancelTimer(false);
                    gameManager.activatedTimerColor = buttonColor;
                    gameManager.currentButtonTimer = this;
                    timerShouldBeActive = true;
                    RefreshTimer();
                }
            }
            if (gameManager.activatedTimerColor == buttonColor)
            {
                gameManager.currentButtonTimer.CancelTimer(true);
                CancelTimer(true);
                gameManager.activatedTimerColor = buttonColor;
                gameManager.currentButtonTimer = this;
                timerShouldBeActive = true;
                RefreshTimer();
            }

            GetComponent<Animator>().Play("ButtonBop");
            AudioManager.PlaySound("ButtonSound", true);
        }

        else if(buttonType == ButtonType.Pressure)
        {
            ToggleWalls();
            GetComponent<Animator>().Play("ButtonBop");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (buttonType == ButtonType.Normal)
        {
            transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
            return;
        }

        else if(buttonType == ButtonType.Pressure)
        {
            ToggleWalls();
        }
    }

    public IEnumerator TriggerTimer()
    {
        int timeLeft = timerTime;

        while (timerShouldBeActive)
        {
            ui.timerText.text = "Timer: " + timeLeft;
            yield return new WaitForSeconds(1);
            timeLeft--;
            if (gameManager.currentButtonTimer == null)
            {
                timerTime = 0;
                break;
            }
            if (timeLeft <= 0)
            {
                CancelTimer(false);
            }
        }
    }

    public void CancelTimer(bool startTimerAfter)
    {
        if (!startTimerAfter)
        {
            ui.timerText.text = "Timer: Not Active";
            ToggleWalls();
        }

        timerShouldBeActive = false;
        gameManager.activatedTimerColor = "";
        gameManager.currentButtonTimer = null;
        if(lastRoutine != null)
        {
            StopCoroutine(lastRoutine);
        }
    }

    public void RefreshTimer()
    {
        lastRoutine = StartCoroutine(TriggerTimer());
    }
}
