using System;
using UnityEngine.SceneManagement;
using UnityEngine;

[System.Serializable]
public class GameState
{
    public enum Stage
    {
        Cutscene01,
        Idle01,
        Quest01,
        Cutscene02,
        Idle02,
        Quest02,
        Cutscene03,
        Idle03,
        Quest03,
        Cutscene04,
        Idle04,
        Quest04,
        Victory,
        GameOver
    }

    public GameSettingsManager gameSettingsManager;
    public DifficultyLevel difficultyLevel;

    public int currentStageIndex = 0;
    public Stage currentStage;
    public Stage[] stages = (Stage[])System.Enum.GetValues(typeof(Stage));

    public int score;
    public float health;

    public int shots = 0;
    public int shotsOnTarget = 0;
    public float totalDistanceTravelled = 0f;
    public double playDuration = 0;
    public int enemiesKilled = 0;
    public float totalDamageReceived = 0f;
    public int orbsPickedUp = 0;

    public Pet[] pets;

    public DateTime startTime;
    public DateTime endTime;
    public DateTime lastSavedTime;
    public DateTime lastStartTime;

    public GameState(GameSettingsManager currentGameSettingsManager)
    {
        gameSettingsManager = currentGameSettingsManager;
        difficultyLevel = gameSettingsManager.GetDifficultyLevel();
        currentStage = Stage.Cutscene01;
        startTime = DateTime.Now;
        lastStartTime = startTime;
        LoadCurrentStage();
    }

    public void SaveGame()
    {
        lastSavedTime = DateTime.Now;
        AddPlayDuration(lastSavedTime, lastStartTime);
    }

    public void LoadGame()
    {
        ResumeGame();
        LoadCurrentStage();
    }

    public void PauseGame()
    {
        lastSavedTime = DateTime.Now;
        AddPlayDuration(lastSavedTime, lastStartTime);
    }

    public void ResumeGame()
    {
        lastStartTime = DateTime.Now;
    }

    public void EndGame(Stage endStage)
    {
        endTime = DateTime.Now;
        currentStage = endStage;
        AddPlayDuration(endTime, lastStartTime);
        LoadCurrentStage();
    }

    void AddPlayDuration(DateTime start, DateTime end)
    {
        TimeSpan timeSpan = end - start;
        playDuration += timeSpan.TotalSeconds;
    }

    public void AdvanceToNextStage()
    {
        // Only advance until Quest04
        if (currentStageIndex < Array.IndexOf(stages, Stage.Victory))
        {
            currentStageIndex++;
            currentStage = stages[currentStageIndex];

            if (currentStage == Stage.Victory)
            {
                EndGame(Stage.Victory);
                return;
            }

            LoadCurrentStage();
        }
    }

    void LoadCurrentStage()
    {
        SceneManager.LoadScene(Scene.GetSceneName(GameConfig.StageToSceneName[currentStage]));
    }

    public void UpdateScore(int newScore)
    {
        score = newScore;
    }

    public void UpdateHealth(int newHealth)
    {
        health = newHealth;
    }

    public void OnShot()
    {
        shots++;
    }

    public void OnShotOnTarget()
    {
        shotsOnTarget++;
    }

    public void AddDistanceTravelled(float distanceTravelled)
    {
        if (GameConfig.StageToSceneName[currentStage] == Scene.SceneName.IdleScene)
        {
            return;
        }
        totalDistanceTravelled += distanceTravelled;
    }

    public void OnEnemyKilled()
    {
        enemiesKilled++;
    }

    public void AddDamageReceived(float damageReceived)
    {
        totalDamageReceived += damageReceived;
    }

    public void OnOrbsPickedUp()
    {
        orbsPickedUp++;
    }

    public float GetShotAccuracy()
    {
        return shotsOnTarget / shots;
    }

    public float GetDistanceTravelled()
    {
        return totalDistanceTravelled;
    }

    public double GetPlayDuration()
    {
        return playDuration;
    }

    public int GetEnemiesKilled()
    {
        return enemiesKilled;
    }

    public float GetDamageReceived()
    {
        return totalDamageReceived;
    }

    public int GetOrbsPickedUp()
    {
        return orbsPickedUp;
    }

    public string GetStageName()
    {
        string stageName = currentStage.ToString(); 
        return stageName.Substring(stageName.Length - 2); 
    }
}
