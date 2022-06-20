using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WantedSystem : MonoBehaviour
{
    public Sprite[] wantedPosters;
    public int currentTarget;
    public Image currentTargetPoster;
    public TMP_Text wantedText, casualtiesDisplay, killsDisplay;

    public GameObject leaderboard;

    public int casualties, kills;

    // Start is called before the first frame update
    void Start()
    {
        currentTarget = Random.Range(0, wantedPosters.Length);
    }

    // Update is called once per frame
    void Update()
    {
        currentTargetPoster.sprite = wantedPosters[currentTarget];

        casualtiesDisplay.text = casualties.ToString();
        killsDisplay.text = kills.ToString();

        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            leaderboard.SetActive(true);
        }
    }
}