using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class GameStatisticsManager : MonoBehaviour
{
    public static GameStatisticsManager Instance { get; private set; }
    public int count;

    public int shots;
    public int shotsOnTarget;
    public float totalDistanceTravelled;
    public double playDuration;
    public int enemiesKilled;
    public float totalDamageReceived;
    public int orbsPickedUp;

    const string STATISTICS_FILENAME = "statistics.json";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        LoadStatistics();
    }

    void SaveStatistics()
    {
        string path = Path.Combine(Application.persistentDataPath, STATISTICS_FILENAME);
        string json = JsonUtility.ToJson(this);
        File.WriteAllText(path, json);

        // TODO: remove debugging line
        Debug.Log("Saving to path : " + path);
        Debug.Log("json : " + json);        
    }

    void LoadStatistics()
    {
        string path = Path.Combine(Application.persistentDataPath, STATISTICS_FILENAME);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(json, this);

            // TODO: remove debugging line
            Debug.Log("Read from : " + path);
            Debug.Log("Read json : " + json);
        }
    }

    public void AddStatistics(GameState gameState)
    {
        shots += gameState.shots;
        shotsOnTarget += gameState.shotsOnTarget;
        totalDistanceTravelled += gameState.totalDistanceTravelled;
        playDuration += gameState.playDuration;
        enemiesKilled += gameState.enemiesKilled;
        totalDamageReceived += gameState.totalDamageReceived;
        orbsPickedUp += gameState.orbsPickedUp;

        count++;
        SaveStatistics();
    }

    public float GetShotAccuracy()
    {
        return shotsOnTarget / shots;
    }

    public float GetDistanceTravelled()
    {
        return totalDistanceTravelled;
    }

    public float GetAverageDistanceTravelled()
    {
        return GetDistanceTravelled() / count;
    }

    public double GetPlayDuration()
    {
        return playDuration;
    }

    public double GetAveragePlayDuration()
    {
        return GetPlayDuration() / count;
    }

    public int GetEnemiesKilled()
    {
        return enemiesKilled;
    }

    public int GetAverageEnemiesKilled()
    {
        return GetEnemiesKilled() / count;
    }

    public float GetDamageReceived()
    {
        return totalDamageReceived;
    }

    public float GetAverageDamageReceived()
    {
        return GetDamageReceived() / count;
    }

    public int GetOrbsPickedUp()
    {
        return orbsPickedUp;
    }
}
