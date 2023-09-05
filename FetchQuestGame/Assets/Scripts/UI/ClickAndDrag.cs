using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickAndDrag : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool dragging = false;
    bool cursorOnButton = false;

    public Transform parentPanel;

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
        // initiate dragging
        if (cursorOnButton)
        {
            if (Input.GetMouseButtonDown(0))
            {
                dragging = true;
                gameObject.transform.SetParent(parentPanel);
            }
        }

        // move ui element w mouse
        if (dragging == true)
        {
            transform.position = Input.mousePosition;
        }

        // stop dragging if left click is released
        if (Input.GetMouseButtonUp(0))
        {
            dragging = false;

            // snap to grid if released on a grid pane
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
                        
                        //gameObject.transform.parent = gridPanels[i].transform.position;
                        gameObject.transform.SetParent(gridPanels[i].transform);
                    }
                }
            }
        }
    }
}
