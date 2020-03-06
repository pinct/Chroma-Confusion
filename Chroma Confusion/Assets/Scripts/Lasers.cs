using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasers : MonoBehaviour
{
    public LineRenderer line;
    public string laserDirection;
    public string laserColor;
    public GameManager gameManager;
    public List<Wall> walls;
    public List<Wall> newWalls;
    int layerMask;
    Vector2 lineStart;
    Vector2 direction;
    string lastHitName = "";
    string newLastHitName = "";

    GameObject laser2;
    LineRenderer line2;

    bool createdNextLine = false;
    // Start is called before the first frame update
    void Start()
    {
        layerMask = ~(LayerMask.GetMask("Confiner", "Conveyor"));
        lineStart = line.GetPosition(0);
        if (laserDirection == "up")
        {
            direction = transform.TransformDirection(Vector2.up);
        }
        else if (laserDirection == "down")
        {
            direction = transform.TransformDirection(Vector2.down);
        }
        else if (laserDirection == "left")
        {
            direction = transform.TransformDirection(Vector2.left);
        }
        else if (laserDirection == "right")
        {
            direction = transform.TransformDirection(Vector2.right);
        }

        RaycastHit2D hit = Physics2D.Raycast(lineStart, direction, Mathf.Infinity, layerMask);
        lastHitName = hit.collider.gameObject.name;

        walls = new List<Wall>();

        for (int i = 0; i < gameManager.allWalls.Length; i++)
        {
            if (gameManager.allWalls[i].GetComponent<Wall>().wallColor == laserColor)
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

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(lineStart, direction, Mathf.Infinity, layerMask);
        if (laserDirection == "up")
        {
            Vector3 end = new Vector3(lineStart.x, lineStart.y + hit.distance, 1);
            line.SetPosition(1, end);
        }
        else if (laserDirection == "down")
        {
            Vector3 end = new Vector3(lineStart.x, lineStart.y - hit.distance, 1);
            line.SetPosition(1, end);
        }
        else if (laserDirection == "left")
        {
            Vector3 end = new Vector3(lineStart.x - hit.distance, lineStart.y, 1);
            line.SetPosition(1, end);
        }
        else if (laserDirection == "right")
        {
            Vector3 end = new Vector3(lineStart.x + hit.distance, lineStart.y, 1);
            line.SetPosition(1, end);
        }
        if (hit.collider.gameObject.name.Contains("Receiver") && !(lastHitName.Contains("Receiver")))
        {
            ToggleWalls();
        }
        else if (!(hit.collider.gameObject.name.Contains("Receiver")) && lastHitName.Contains("Receiver"))
        {
            ToggleWalls();
        }
        lastHitName = hit.collider.gameObject.name;

        if (!createdNextLine)
        {
            laser2 = new GameObject();
            line2 = laser2.AddComponent<LineRenderer>();
            createdNextLine = true;
        }

        if (lastHitName.Contains("Prism") && createdNextLine)
        {
            if (laserColor == "Red")
            {
                RaycastHit2D nextRay = Physics2D.Raycast(new Vector2(hit.collider.transform.position.x, hit.point.y), direction, Mathf.Infinity, hit.collider.gameObject.layer);

                line2.SetPositions(new Vector3[] { hit.point, new Vector3(nextRay.point.x, nextRay.point.y) });
                line2.material = new Material(Shader.Find("Sprites/Default"));

                line2.startColor = new Color(1f, 0.647f, 0f);
                line2.endColor = new Color(1f, 0.647f, 0f);

                line2.startWidth = 0.1f;
                line2.endWidth = 0.1f;
                newWalls = new List<Wall>();

                for (int i = 0; i < gameManager.allWalls.Length; i++)
                {
                    if (gameManager.allWalls[i].GetComponent<Wall>().wallColor == "Orange")
                    {
                        newWalls.Add(gameManager.allWalls[i].GetComponent<Wall>());
                    }
                }
                if (nextRay.collider.gameObject.name.Contains("Receiver") && !(newLastHitName.Contains("Receiver")))
                {
                    foreach (Wall wall in newWalls)
                    {
                        wall.gameObject.SetActive(!wall.gameObject.activeSelf);
                    }
                }
                else if (!(nextRay.collider.gameObject.name.Contains("Receiver")) && newLastHitName.Contains("Receiver"))
                {
                    foreach (Wall wall in newWalls)
                    {
                        wall.gameObject.SetActive(!wall.gameObject.activeSelf);
                    }
                }
                newLastHitName = nextRay.collider.gameObject.name;
            }
            else if (laserColor == "Blue")
            {
                RaycastHit2D nextRay = Physics2D.Raycast(new Vector2(hit.collider.transform.position.x, hit.point.y), direction, Mathf.Infinity, hit.collider.gameObject.layer);

                line2.SetPositions(new Vector3[] { hit.point, new Vector3(nextRay.point.x, nextRay.point.y) });
                line2.material = new Material(Shader.Find("Sprites/Default"));

                line2.startColor = new Color(25.0f/255.0f, 1.0f, 0f);
                line2.endColor = new Color(25.0f / 255.0f, 1.0f, 0f);

                line2.startWidth = 0.1f;
                line2.endWidth = 0.1f;
                newWalls = new List<Wall>();

                for (int i = 0; i < gameManager.allWalls.Length; i++)
                {
                    if (gameManager.allWalls[i].GetComponent<Wall>().wallColor == "Green")
                    {
                        newWalls.Add(gameManager.allWalls[i].GetComponent<Wall>());
                    }
                }
                if (nextRay.collider.gameObject.name.Contains("Receiver") && !(newLastHitName.Contains("Receiver")))
                {
                    foreach (Wall wall in newWalls)
                    {
                        wall.gameObject.SetActive(!wall.gameObject.activeSelf);
                    }
                }
                else if (!(nextRay.collider.gameObject.name.Contains("Receiver")) && newLastHitName.Contains("Receiver"))
                {
                    foreach (Wall wall in newWalls)
                    {
                        wall.gameObject.SetActive(!wall.gameObject.activeSelf);
                    }
                }
                newLastHitName = nextRay.collider.gameObject.name;
            }
        }
        else
        {
            foreach (Wall wall in newWalls)
            {
                wall.gameObject.SetActive(!wall.gameObject.activeSelf);
            }
            newWalls = new List<Wall>();
            newLastHitName = "";
            createdNextLine = false;
            Destroy(laser2);
            Destroy(line2);
        }
    }
}
