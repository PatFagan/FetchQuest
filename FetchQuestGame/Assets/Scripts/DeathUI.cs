using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DeathUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Material defaultMat, animatedMat;
    public GameObject deathUI;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        //Output to console the GameObject's name and the following message
        Debug.Log("Cursor Entering " + name + " GameObject");
        gameObject.GetComponent<Image>().material = animatedMat;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        //Output the following message with the GameObject's name
        Debug.Log("Cursor Exiting " + name + " GameObject");
        gameObject.GetComponent<Image>().material = defaultMat;
    }

    public void Retry()
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>().Revive();
        deathUI.SetActive(false);
    }
}