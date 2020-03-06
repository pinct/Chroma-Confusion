using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    public GameObject zipper;

    public string axisDirection;
    public float directionValue;

    public float beltSpeed;

    private float factor = 4;

    // Start is called before the first frame update
    void Start()
    {
        zipper.GetComponent<Animator>().speed = beltSpeed;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (axisDirection == "Y" && directionValue == 1)
        {
            other.gameObject.transform.position += Vector3.up * Time.deltaTime * beltSpeed * factor;
        }

        else if(axisDirection == "Y" && directionValue == -1)
        {
            other.gameObject.transform.position += Vector3.down * Time.deltaTime * beltSpeed * factor;
        }

        else if(axisDirection == "X" && directionValue == 1)
        {
            other.gameObject.transform.position += Vector3.right * Time.deltaTime * beltSpeed * factor;
        }

        else if (axisDirection == "X" && directionValue == -1)
        {
            other.gameObject.transform.position += Vector3.left * Time.deltaTime * beltSpeed * factor;
        }
    }
}
