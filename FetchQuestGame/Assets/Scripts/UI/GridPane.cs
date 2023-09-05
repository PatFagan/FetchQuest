using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GridPane : MonoBehaviour
{
    public bool cursorHovering = false;

    float minX, maxX, minY, maxY;

    void Start()
    {
        minX = transform.position.x - 25;
        maxX = transform.position.x + 25;
        minY = transform.position.y - 25;
        maxY = transform.position.y + 25;
    }

    void Update()
    {
        if(Input.mousePosition.x < maxX && Input.mousePosition.x > minX)
        {
            if(Input.mousePosition.y < maxY && Input.mousePosition.y > minY)
            {
                cursorHovering = true;
            }
        }
        else
        {
            cursorHovering = false;
        }
    }
}