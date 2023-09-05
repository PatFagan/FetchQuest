using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSwitch : MonoBehaviour
{
    public GameObject switchOn, switchOff;

    bool toggled = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Toggle()
    {
        toggled = !toggled;

        if (toggled)
        {
            switchOn.GetComponent<Image>().color = Color.blue;
            switchOff.GetComponent<Image>().color = Color.white;
        }
        else if (!toggled)
        {
            switchOn.GetComponent<Image>().color = Color.white;
            switchOff.GetComponent<Image>().color = Color.blue;
        }

    }
}
