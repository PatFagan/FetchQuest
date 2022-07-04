using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPress : MonoBehaviour
{
    Button button;

    void Start()
    {
        button = this.gameObject.GetComponent<Button>();
    }

    void Update()
    {
        if (Input.GetButton("Button"))
        {
            button.onClick.Invoke();
        }
    }
}