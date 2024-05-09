using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public enum DifficultyLevel
{
    Low,
    Medium,
    High
}

public class GameSettingsManager : MonoBehaviour
{
    // Singleton
    public static GameSettingsManager Instance { get; private set; }

    // UI References
    public AudioSource backgroundMusic;

    // Constants
    const string DIFFICULTY_KEY = "Difficulty";
    const string PLAYER_NAME_KEY = "PlayerName";
    const string VOLUME_KEY = "Volume";

    const DifficultyLevel DEFAULT_DIFFICULTY_LEVEL = DifficultyLevel.Low;
    const string DEFAULT_PLAYER_NAME = "Karina";
    const float DEFAULT_VOLUME = 1f;

    // Private Variables
    DifficultyLevel difficultyLevel;
    string playerName;
    float volume;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            LoadSettings();
            SetBackgroundMusicVolume();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadSettings();    
    }

    private void OnApplicationQuit()
    {
        SaveSettings();
    }

    void SetBackgroundMusicVolume()
    {
        backgroundMusic.volume = volume;
    }

    // Load
    public void LoadSettings()
    {
        LoadVolume();
        LoadDifficultyLevel();
        LoadPlayerName();
    }

    void LoadDifficultyLevel()
    {
        string savedDifficulty = PlayerPrefs.GetString(DIFFICULTY_KEY, DEFAULT_DIFFICULTY_LEVEL.ToString());
        DifficultyLevel loadedDifficultyLevel = (DifficultyLevel)System.Enum.Parse(typeof(DifficultyLevel), savedDifficulty);
        SetDifficultyLevel(loadedDifficultyLevel);
    }

    void LoadPlayerName()
    {
        string loadedPlayerName = PlayerPrefs.GetString(PLAYER_NAME_KEY, DEFAULT_PLAYER_NAME);
        SetPlayerName(loadedPlayerName);
    }

    void LoadVolume()
    {
        float loadedVolume = PlayerPrefs.GetFloat(VOLUME_KEY, DEFAULT_VOLUME);
        SetVolume(loadedVolume);
    }

    // Save
    public void SaveSettings()
    {
        SaveDifficultyLevel();
        SavePlayerName();
        SaveVolume();
    }

    public void SaveDifficultyLevel()
    {
        PlayerPrefs.SetString(DIFFICULTY_KEY, difficultyLevel.ToString());
    }

    public void SavePlayerName()
    {
        PlayerPrefs.SetString(PLAYER_NAME_KEY, playerName);
    }

    public void SaveVolume()
    {
        PlayerPrefs.SetFloat(VOLUME_KEY, volume);
    }

    // Setter
    public void SetDifficultyLevel(DifficultyLevel newDifficultyLevel)
    {
        Debug.Log("Diff: " + newDifficultyLevel);
        difficultyLevel = newDifficultyLevel;
        SaveDifficultyLevel();
    }

    public void SetPlayerName(string newPlayerName)
    {
        Debug.Log("Name: " + newPlayerName);
        playerName = newPlayerName;
        SavePlayerName();
    }

    public void SetVolume(float newVolume)
    {
        Debug.Log("Vol: " + newVolume);
        volume = newVolume;
        SaveVolume();
    }

    // Getter
    public DifficultyLevel GetDifficultyLevel()
    {
        return difficultyLevel;
    }

    public string GetPlayerName()
    {
        return playerName;
    }

    public float GetVolume()
    {
        return volume;
    }
}
