using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinOnClick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(new Vector3(0f, .3f, 0f));
    }
}
