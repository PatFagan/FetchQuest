using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform parentPlayer;

    bool cameraSet = true;

    void Update()
    {
        bool isPlayerDead = parentPlayer.gameObject.GetComponent<PlayerMovement>().dead;

        // when player is alive
        if (!isPlayerDead)
            transform.position = parentPlayer.transform.position;

        // when player just got revived, and camera is resetting
        if (!cameraSet && isPlayerDead)
        {
            if (gameObject.transform.position != parentPlayer.transform.position)
            {
                transform.position = Vector3.Lerp(transform.position, parentPlayer.transform.position, .01f);
            }
        }

        // when player is dead
        else if(isPlayerDead)
        {
            Vector3 deathPosition = new Vector3(transform.position.x,
                transform.position.y + .1f, transform.position.z + .1f);

            transform.position = Vector3.Lerp(transform.position, deathPosition, .01f);

            //Camera.main.fieldOfView = Mathf.MoveTowards(Camera.main.fieldOfView, 140f, Time.deltaTime * 7f);

            Vector3 faceDirection = parentPlayer.position - transform.position;
            transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, 
                Quaternion.LookRotation(new Vector3(faceDirection.x, 0f, faceDirection.z)), Time.deltaTime * .1f);
        }
    }

    public void Revive()
    {
        StartCoroutine(FinishRevive());
    }

    IEnumerator FinishRevive()
    {
        cameraSet = false;
        
        yield return new WaitForSeconds(1.5f);

        print("hi");
        cameraSet = true;
        print("ho");

        print("off to work");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerMovement>().dead = false;
        player.GetComponent<PlayerHealth>().health = player.GetComponent<PlayerHealth>().maxHealth;
        player.GetComponent<PlayerMovement>().ResetCursor();
    }

}