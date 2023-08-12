using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class GoapAction : MonoBehaviour//, IPunObservable
{
    public string name;
    public int cost;
    public Dictionary<string, bool> preconditions = new Dictionary<string, bool>();
    public string aiName = "Dummy";

    public float speed;
    GameObject target;
    Transform followerTransform;
    public Vector3 targetPos;
    public Vector3 currentPos;
    GameObject follower;

    public int currentTarget = 1;
    public int targetSwap = 0;
    public float runTimeInSeconds = 1.0f;
    public bool running = false;

    public abstract bool CheckPreconditions();
    public abstract float RunAction();

    public GoapAgent goapAgentScript;

    //[PunRPC]
    public void SetTarget(int targetIndex)
    {
        //if (PhotonNetwork.isMasterClient)
        {
            string targetTag = "Player" + targetIndex;
            target = GameObject.Find(targetTag);
            follower = GameObject.FindGameObjectWithTag("Enemy");

            StartCoroutine(MoveToTarget());

            if (GameObject.Find("Player2"))
                targetSwap++;

            if (targetSwap > 5)
            {
                if (currentTarget == 1)
                    currentTarget = 2;
                else
                    currentTarget = 1;
            }

            //targetSwap = 0;
            goapAgentScript.SetTargetIndex(currentTarget);
            print("target swap to " + currentTarget);
        }
    }

    /*
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(targetSwap);
        }
        else
        {
            targetSwap = (int)stream.ReceiveNext();
        }
    }
    */

    IEnumerator MoveToTarget()
    {
        if (name == "Leap")
        {
            //if (PhotonNetwork.isMasterClient)
            {
                if (target && follower.GetComponent<Goap>().pausedGoap == false)
                {
                    targetPos = target.GetComponent<Transform>().position;
                    followerTransform = follower.GetComponent<Transform>();
                    currentPos = new Vector3(followerTransform.position.x, 0f, followerTransform.position.z);

                    float t = 0f;
                    int duration = 5;
                    targetPos = new Vector3(targetPos.x, 0f, targetPos.z);
                    yield return new WaitForSeconds(1.25f);//wait for charge Animation
                    while (targetPos != currentPos && target && duration > 0) // while not arrived and while target exists
                    {
                        currentPos = new Vector3(followerTransform.position.x, -1f, followerTransform.position.z);
                        t += Time.fixedDeltaTime;
                        //follower.GetComponent<Transform>().position = Vector3.Lerp(currentPos, targetPos, t);

                        // spawn in teleport particle effect HERE

                        // position = Vector2.MoveTowards(from, to, speed);
                        //follower.GetComponent<Transform>().position = targetPos;// Vector3.MoveTowards(currentPos, targetPos, speed * Time.fixedDeltaTime);

                        follower.GetComponent<Transform>().position = Vector3.MoveTowards(currentPos, targetPos, 300f * Time.fixedDeltaTime);
                        yield return new WaitForSeconds(.05f);
                        duration--;
                    }
                }
           
            }
        }
    }

    protected IEnumerator endRunning()
    {
        yield return new WaitForSeconds(runTimeInSeconds);
        running = false;
    }
}