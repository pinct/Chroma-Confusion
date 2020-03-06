using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{
    public float movementSpeed;

    public bool userCanControl = true;

    public Animator animator;

    public GameObject[] keyboardKeys;

    private bool[] keyboardKeysPressed;

    public GameObject controlsCanvas;

    private void Start()
    {
        keyboardKeysPressed = new bool[keyboardKeys.Length];
    }

    void Update()
    {

        if (userCanControl)
        {
            float _xMovement = Input.GetAxis("Horizontal");

            float _yMovement = Input.GetAxis("Vertical");

            Vector2 _movement = new Vector2(_xMovement, _yMovement);

            Move(_movement);
        }
        if (Input.GetButtonDown("Cancel"))
        {
            SceneManager.LoadScene("StageSelect");
        }
        controlsCanvas.transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    void Move(Vector2 movement)
    {
        if (movement != Vector2.zero)
        {
            if(movement.x == 0 && (movement.y > 0 && movement.y <= 1))
            {
                if (keyboardKeysPressed.Length > 0)
                {
                    if (!keyboardKeysPressed[0])
                    {
                        keyboardKeys[0].gameObject.SetActive(false);
                        keyboardKeysPressed[0] = false;
                    }
                }

                ChangeRotation(0f);
            }

            else if ((movement.x > 0 && movement.x <= 1) && (movement.y > 0 && movement.y <= 1))
            {
                ChangeRotation(-45f);
            }

            else if((movement.x > 0 && movement.x <= 1) && movement.y == 0)
            {
                if (keyboardKeysPressed.Length > 0)
                {
                    if (!keyboardKeysPressed[1])
                    {
                        keyboardKeys[1].gameObject.SetActive(false);
                        keyboardKeysPressed[1] = false;
                    }
                }

                ChangeRotation(-90f);
            }

            else if((movement.x > 0 && movement.x <= 1) && (movement.y < 0 && movement.y >= -1))
            {
                ChangeRotation(-135f);
            }

            else if(movement.x == 0 && (movement.y < 0 && movement.y >= -1))
            {
                if (keyboardKeysPressed.Length > 0)
                {

                    if (!keyboardKeysPressed[2])
                    {
                        keyboardKeys[2].gameObject.SetActive(false);
                        keyboardKeysPressed[2] = false;
                    }
                }

                ChangeRotation(-180f);
            }

            else if((movement.x < 0 && movement.x >= -1) && (movement.y < 0 && movement.y >= -1))
            {
                ChangeRotation(135f);
            }

            else if ((movement.x < 0 && movement.x >= -1) && movement.y == 0)
            {
                if (keyboardKeysPressed.Length > 0)
                {
                    if (!keyboardKeysPressed[3])
                    {
                        keyboardKeys[3].gameObject.SetActive(false);
                        keyboardKeysPressed[3] = false;
                    }
                }

                ChangeRotation(90f);
            }

            else if((movement.x < 0 && movement.x >= -1) && (movement.y > 0 && movement.y <= 1))
            {
                ChangeRotation(45f);
            }

            transform.position += new Vector3(movement.x, movement.y, 0) * Time.deltaTime * movementSpeed;

            animator.SetBool("isWalking", true);
        }

        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    void ChangeRotation(float rotationZ)
    {
        transform.rotation = Quaternion.Euler(Vector3.forward * rotationZ);
    }
}