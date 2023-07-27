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
    public GameObject restartButton;

    public GameObject leaderboard;
    bool leaderboardEnabled = false;

    public int casualties, kills;

    public void SetTarget()
    {
        currentTarget = Random.Range(0, wantedPosters.Length);
        leaderboardEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        currentTargetPoster.sprite = wantedPosters[currentTarget];

        casualtiesDisplay.text = casualties.ToString();
        killsDisplay.text = kills.ToString();

        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && leaderboardEnabled)
        {
            leaderboard.SetActive(true);
            restartButton.SetActive(true);
        }
    }
}