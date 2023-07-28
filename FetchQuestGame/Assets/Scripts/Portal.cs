using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject partnerPortal;
    public GameObject partnerFlipsidePortal;
    public GameObject flipsidePortal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Bullet")
        {
            // pause partner portal
            StartCoroutine(PausePartnerPortal());
            // teleport to partner portal
            // update to get difference bw player and center of this portal
            Vector3 distFromPortal = gameObject.transform.position - collision.gameObject.transform.position;
            // then tp to new portal and take into account the same difference from the center
            collision.gameObject.transform.position = partnerPortal.transform.position;// - distFromPortal;
            // rotate in direction of new portal
            collision.gameObject.transform.rotation = Quaternion.Euler(0,0,0);
            collision.gameObject.transform.rotation = partnerPortal.transform.rotation;
            
            if (collision.gameObject.tag == "Player")
            {
                GameObject.FindGameObjectWithTag("MainCamera").transform.localRotation = partnerPortal.transform.rotation;
            }
        }
    }

    IEnumerator PausePartnerPortal()
    {
        // disable partner portal
        partnerPortal.GetComponent<BoxCollider>().enabled = false;
        partnerFlipsidePortal.GetComponent<BoxCollider>().enabled = false;
        //flipsidePortal.GetComponent<BoxCollider>().enabled = false;

        // pause
        yield return new WaitForSeconds(.25f);

        // enable partner portal
        partnerPortal.GetComponent<BoxCollider>().enabled = true;
        partnerFlipsidePortal.GetComponent<BoxCollider>().enabled = true;        
        flipsidePortal.GetComponent<BoxCollider>().enabled = true;
    }
}
