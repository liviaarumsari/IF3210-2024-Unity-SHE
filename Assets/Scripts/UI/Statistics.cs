using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Statistics : MonoBehaviour
{
    GameStatisticsManager gameStatisticsManager;

    public TMP_Text matchesPlayed;
    public TMP_Text shotsAccuracy;
    public TMP_Text distanceTravelled;
    public TMP_Text playDuration;
    public TMP_Text enemiesKilled;
    public TMP_Text damageReceived;
    public TMP_Text orbsPickedUp;

    void Start()
    {
        if (gameStatisticsManager == null)
        {
            gameStatisticsManager = GameStatisticsManager.Instance;
        }

        matchesPlayed.text = "Hallo";
    }
}
