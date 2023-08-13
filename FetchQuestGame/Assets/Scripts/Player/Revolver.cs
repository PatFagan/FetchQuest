using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Revolver : NetworkBehaviour
{
    public Transform gunTip;
    public GameObject bullet;
    public int bulletCount = 6;
    public bool drawn = true;
    bool reloading = false;
    public Animator animator;
    public GameObject parentPlayer;

    public AudioSource gunshot;

    // Start is called before the first frame update
    void Start()
    {
        drawn = true;
    }

    // Update is called once per frame
    void Update()
    {
        // stick to parent player
        transform.position = parentPlayer.transform.position;
        
        // get aim direction from player
        Quaternion aimDirection = parentPlayer.GetComponent<PlayerMovement>().cameraRotation;
        // gun faces camera
        transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, 
            aimDirection, 5f * Time.deltaTime);

        if (parentPlayer.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            GunInputs();
        }
    }

    void GunInputs()
    {
        // shoot gun
        if (Input.GetButtonDown("Shoot") && bulletCount > 0 && drawn && !reloading)
        {
            animator.Play("Recoil", 0, 0f);
            //anim.Play("Base Layer.Bounce", 0, 0.25f)
            gunshot.Play();
            Instantiate(bullet, gunTip.position, Quaternion.identity);
            bulletCount--;
        }

        // reload gun
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