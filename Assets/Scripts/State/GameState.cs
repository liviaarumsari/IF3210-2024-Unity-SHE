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
    
    GameSettingsManager gameSettingsManager;
    public DifficultyLevel difficultyLevel { get; private set; }

    int currentStageIndex = 0;
    Stage currentStage;
    Stage[] stages = (Stage[])System.Enum.GetValues(typeof(Stage));

    public int score { get; private set; }
    public float health { get; private set; }

    int shots = 0;
    int shotsOnTarget = 0;
    float totalDistanceTravelled = 0f;
    double playDuration = 0;
    int enemiesKilled = 0;
    float totalDamageReceived = 0f;
    int orbsPickedUp = 0;

    Pet[] pets;

    DateTime startTime;
    DateTime endTime;
    DateTime lastSavedTime;
    DateTime lastStartTime;

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
}
