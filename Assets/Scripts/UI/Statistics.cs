using System;
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

        matchesPlayed.text = gameStatisticsManager.count.ToString();
        shotsAccuracy.text = gameStatisticsManager.GetShotAccuracy().ToString("F2") + "%";
        distanceTravelled.text = gameStatisticsManager.GetDistanceTravelled().ToString("F2");
        
        TimeSpan timeSpan = TimeSpan.FromSeconds(gameStatisticsManager.GetPlayDuration());
        playDuration.text = string.Format("{0:D2}:{1:D2}:{2:D2}", 
            timeSpan.Hours, 
            timeSpan.Minutes, 
            timeSpan.Seconds);
            
        enemiesKilled.text = gameStatisticsManager.GetEnemiesKilled().ToString();
        damageReceived.text = gameStatisticsManager.GetDamageReceived().ToString("F2");
        orbsPickedUp.text = gameStatisticsManager.GetOrbsPickedUp().ToString();
    }
}
