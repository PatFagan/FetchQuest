using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : MonoBehaviour
{
    Transform camera;
    public Transform gunTip;
    public GameObject bullet;
    public int bulletCount = 6;
    public bool drawn = true;
    bool reloading = false;
    public Animator animator;

    public AudioSource gunshot;

    // Start is called before the first frame update
    void Start()
    {
        drawn = true;
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // gun faces camera
        transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, camera.transform.rotation, 5f * Time.deltaTime);

        // shoot gun
        if (Input.GetButtonDown("Shoot") && bulletCount > 0 && drawn && !reloading)
        {
            animator.Play("Recoil", 0, 0f);
            //anim.Play("Base Layer.Bounce", 0, 0.25f)
            gunshot.Play();
            Instantiate(bullet, gunTip.position, Quaternion.identity);
            bulletCount--;
        }

        if (Input.GetButtonDown("Reload") && drawn && !reloading)
        {
            animator.Play("Reload", 0, 0f);
            StartCoroutine(Reload());
        }

        // draw gun
        if (Input.GetButtonDown("Draw"))
        {
            if (!drawn)
                animator.Play("DrawGun");
            else if (drawn)
                animator.Play("LowerGun");
            
            drawn = !drawn;
        }
    }

    IEnumerator Reload()
    {
        reloading = true;
        yield return new WaitForSeconds(.6f);
        bulletCount = 6;
        reloading = false;
    }
}