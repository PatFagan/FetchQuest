using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanBeShot : MonoBehaviour
{
    public float health;
    public bool canBeDestroyed;

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
        if (collision.gameObject.tag == "Bullet")
        {
            // subtract health
            health--;

            // flash
            StartCoroutine(Flash());

            // if armor piercing rounds is false
            Destroy(collision.gameObject);

            // check if dead
            if (health <= 0 && canBeDestroyed)
            {
                Destroy(gameObject);
            }
        }
    }

    IEnumerator Flash()
    {
        Material mat = gameObject.GetComponent<MeshRenderer>().material;
        Color baseColor = mat.GetColor("_EmissionColor");
        Color white = Color.white;

        // flash when hit
        mat.SetColor("_EmissionColor", white);

        yield return new WaitForSeconds(.1f);

        // return to original color
        mat.SetColor("_EmissionColor", baseColor);
    }
}
