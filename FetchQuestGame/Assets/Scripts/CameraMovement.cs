using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform parentPlayer;

    void Update()
    {
        bool isPlayerDead = parentPlayer.gameObject.GetComponent<PlayerMovement>().dead;

        if (!isPlayerDead)
            transform.position = parentPlayer.transform.position;

        else if(isPlayerDead)
        {
            ///*
            Vector3 deathPosition = new Vector3(transform.position.x,
                transform.position.y + .1f, transform.position.z + .1f);

            transform.position = Vector3.Lerp(transform.position, deathPosition, .01f);
            //*/

            Camera.main.fieldOfView = Mathf.MoveTowards(Camera.main.fieldOfView, 140f, Time.deltaTime * 7f);

            //transform.rotation = Quaternion.Lerp(transform.position, 
              //  new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z - 45f), .1f);
        

            Vector3 faceDirection = parentPlayer.position - transform.position;
            transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, 
                Quaternion.LookRotation(new Vector3(faceDirection.x, 0f, faceDirection.z)), Time.deltaTime * .1f);

            print(transform.rotation);
        }
    }
}