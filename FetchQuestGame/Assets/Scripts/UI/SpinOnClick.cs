using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinOnClick : MonoBehaviour
{
    float initialClickPos;
    float spinDist;
    public float maxSpinDist;

    // Update is called once per frame
    void Update()
    {
        // ref: https://forum.unity.com/threads/solved-spin-a-3d-object-using-mouse.384167/
        if (Input.GetMouseButtonDown(0))
        {
            initialClickPos = Input.mousePosition.x;
        }

        if(Input.GetMouseButton(0))
        {   
            if (Mathf.Abs(spinDist) < maxSpinDist)
                spinDist += (Input.mousePosition.x - initialClickPos);
            initialClickPos = Input.mousePosition.x;
        }

        // friction
        if(spinDist >= 0)
        {
            spinDist--;
        }
        else if(spinDist <= 0)
        {
            spinDist++;
        }

        gameObject.transform.Rotate(new Vector3(0f, -spinDist/200f, 0f));

    }
}
