using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum Stage
{
    Stage1,
    Stage2, 
    Stage3,
    Story1, 
    Story2,
    Story3,
    Victory
}

[System.Serializable]
public class GameState
{
    GameSettingsManager gameSettingsManager;
    DifficultyLevel difficultyLevel;
    Stage stage;
    int score;
    float money;
    int shots;
    int shotsOnTarget;
    float distance;
    float playTimeSeconds;
    float health;
    DateTime startTime;
    DateTime endTime;
    DateTime lastSavedTime;
    DateTime lastStartTime;

    public GameState(GameSettingsManager currentGameSettingsManager)
    {
        gameSettingsManager = currentGameSettingsManager;
        difficultyLevel = gameSettingsManager.GetDifficultyLevel();
        stage = Stage.Story1;
        startTime = DateTime.Now;
        lastStartTime = startTime;
    }

    public void SaveGame()
    {
        lastSavedTime = DateTime.Now;
        UpdatePlayTimeSeconds(lastSavedTime, lastStartTime);
        lastStartTime = lastSavedTime;
    }

    public void PauseGame()
    {
        if (stage == Stage.Stage1 || stage == Stage.Stage2 || stage == Stage.Stage3)
        {
            lastSavedTime = DateTime.Now;
        }
    }

    public void ResumeGame()
    {
        if (stage == Stage.Stage1 || stage == Stage.Stage2 || stage == Stage.Stage3)
        {
            lastStartTime = DateTime.Now;
        }
    }

    public void EndGame()
    {
        endTime = DateTime.Now;
        stage = Stage.Victory;
        UpdatePlayTimeSeconds(endTime, lastStartTime);
    }

    void UpdatePlayTimeSeconds(DateTime start, DateTime end)
    {
        TimeSpan timeSpan = end - start;
        playTimeSeconds += (float)timeSpan.TotalSeconds;
    }

    public void AdvanceToNextStage()
    {
        switch (stage)
        {
            case Stage.Story1:
                stage = Stage.Stage1;
                ResumeGame();
                break;
            case Stage.Stage1:
                PauseGame();
                stage = Stage.Story2;
                break;
            case Stage.Story2:
                stage = Stage.Stage2;
                ResumeGame();
                break;
            case Stage.Stage2:
                PauseGame();
                stage = Stage.Story3;
                break;
            case Stage.Story3:
                stage = Stage.Stage3;
                ResumeGame();
                break;
            case Stage.Stage3:
                PauseGame();
                stage = Stage.Victory;
                break;
            default:
                Debug.Log("Game has ended");
                break;
        }
    }
}
