using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MoveAgainstCursor : MonoBehaviour
{
    Vector2 defaultPos;

    public float moveScalar;

    void Start()
    {
        defaultPos = new Vector2(transform.position.x, transform.position.z);
    }

    void Update()
    {
        // get mouse position
        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        // Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        // shift to account for weird askewness
        screenPosition.y -= 4000f * (moveScalar / 20f);

        // get distance between mouse and object
        Vector2 thisPos = new Vector2(transform.position.x, transform.position.z);
        Vector2 dist = thisPos - screenPosition;

        // move object slighly away from mouse based on distance
        gameObject.transform.position = defaultPos + (dist/moveScalar);
    }
}