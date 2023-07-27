using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : MonoBehaviour
{
    Transform camera;
    public Transform gunTip;
    public GameObject bullet;
    int bulletCount = 6;
    public bool drawn = false;
    public Animator anim;

    public AudioSource gunshot;

    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // gun faces camera
        transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, camera.transform.rotation, 5f * Time.deltaTime);

        // shoot gun
        if (Input.GetButtonDown("Shoot") && bulletCount > 0 && drawn == true)
        {
            anim.Play("Recoil", 0, 0f);
            //anim.Play("Base Layer.Bounce", 0, 0.25f)
            gunshot.Play();
            Instantiate(bullet, gunTip.position, Quaternion.identity);
            bulletCount--;
        }

        // draw gun
        if (Input.GetButtonDown("Draw"))
        {
            drawn = true;
            anim.Play("DrawGun");
        }
    }
}