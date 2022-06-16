using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : MonoBehaviour
{
    public Transform camera, gunTip;
    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, camera.transform.rotation, 5f * Time.deltaTime);

        if (Input.GetButtonDown("Shoot"))
        {
            Instantiate(bullet, gunTip.position, Quaternion.identity);
        }
    }
}