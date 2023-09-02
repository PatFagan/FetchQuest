using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonTest : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject caption;
    bool captionActive = false;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        gameObject.GetComponent<Image>().color = Color.blue;

        captionActive = true;
        caption.SetActive(captionActive);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        gameObject.GetComponent<Image>().color = Color.white;

        captionActive = false;
        caption.SetActive(captionActive);
    }
}
