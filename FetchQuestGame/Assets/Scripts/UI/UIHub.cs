using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIHub : MonoBehaviour
{
    public GameObject mainUI;

    public void Retry()
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>().Revive();
        //gameObject.SetActive(false);
    }
}