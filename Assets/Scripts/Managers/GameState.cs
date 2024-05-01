using System;
using System.Collections;
using System.Collections.Generic;
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
        TimeSpan timeSpan = lastSavedTime - lastStartTime;
        playTimeSeconds += (float)timeSpan.TotalSeconds;
        lastStartTime = lastSavedTime;
    }

    public void ResumeGame()
    {
        lastStartTime = DateTime.Now;
    }

    public void EndGame()
    {
        endTime = DateTime.Now;
        TimeSpan timeSpan = endTime - lastStartTime;
        playTimeSeconds += (float)timeSpan.TotalSeconds;
    }


}
