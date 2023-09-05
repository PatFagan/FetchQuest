using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickAndDrag : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool dragging = false;
    bool cursorOnButton = false;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        cursorOnButton = true;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        cursorOnButton = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (cursorOnButton)
        {
            if (Input.GetMouseButtonDown(0))
            {
                print("work");
                dragging = true;
            }
        }

        if (dragging == true)
        {
            transform.position = Input.mousePosition;
        }

        // stop dragging if left click is released
        if (Input.GetMouseButtonUp(0))
        {
            dragging = false;

            if (cursorOnButton)
            {
                // then snap to grid if on grid panel
                GameObject[] gridPanels = GameObject.FindGameObjectsWithTag("GridPanel");
                for (int i = 0; i < gridPanels.Length; i++)
                {
                    if (gridPanels[i].GetComponent<GridPane>().cursorHovering)
                    {
                        print("snap to panel");
                        gameObject.transform.position = gridPanels[i].transform.position;
                    }
                }
            }
        }
    }
}
