using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPerkManager : MonoBehaviour
{
    public Dictionary<string, bool> abilities = new Dictionary<string, bool>();
    //public CoinTracker coinScript;

    // Start is called before the first frame update
    void Start()
    {
        // set up list of abilities
        abilities.Add("Fireball", false);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Ability")
        {
            // fireball ability
            if (collider.gameObject.name == "FireballAbility")
            {
                /*
                // buy the ability
                if (coinScript.coinsCollected >= 4)
                {
                    print("buy fire");
                    coinScript.coinsCollected -= 4;
                    abilities["Fireball"] = true;
                    collider.gameObject.SetActive(false);
                }
                */
            }
        }
    }
}