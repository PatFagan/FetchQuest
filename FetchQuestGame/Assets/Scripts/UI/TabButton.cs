using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabButton : MonoBehaviour
{
    public GameObject myTabMode, tabMode2, tabMode3;

    public void OpenMyTab()
    {
        myTabMode.SetActive(true);
        tabMode2.SetActive(false);
        tabMode3.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
