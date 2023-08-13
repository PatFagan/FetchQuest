using UnityEngine;

public class StickToPlayer : MonoBehaviour
{
    public Transform parentPlayer;

    void Update()
    {
        transform.position = parentPlayer.transform.position;
    }
}