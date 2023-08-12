using UnityEngine;

public class StickToPlayer : MonoBehaviour
{
    public Transform parentPlayer;

    void Start()
    {
        //player = GameObject.FindGameObjectsWithTag("Player").GetComponent<Transform>();

        /*
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)
        {
            GameObject tempPlayer;
            tempPlayer = GameObject.FindGameObjectsWithTag("Player")[i];
            if (tempPlayer.GetComponent<NetworkIdentity>().isLocalPlayer)
            {
                player = tempPlayer.GetComponent<Transform>();
                print("set player stick");
            }
        }
        */
    }

    void Update()
    {
        transform.position = parentPlayer.transform.position;
    }
}