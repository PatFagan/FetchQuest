using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonCaption : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Material defaultMat, animatedMat;

    public GameObject caption;
    bool captionActive = false;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        //gameObject.GetComponent<Image>().color = Color.blue;
        gameObject.GetComponent<Image>().material = animatedMat;

        captionActive = true;
        caption.SetActive(captionActive);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        //gameObject.GetComponent<Image>().color = Color.white;
        gameObject.GetComponent<Image>().material = defaultMat;

        captionActive = false;
        caption.SetActive(captionActive);
    }
}