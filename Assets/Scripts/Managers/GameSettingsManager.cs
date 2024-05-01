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
    public Button lowDifficultyButton;
    public Button mediumDifficultyButton;
    public Button highDifficultyButton;
    public TMPro.TMP_InputField playerNameInputField;
    public Slider volumeSlider;
    public AudioSource backgroundMusic;

    // Constants
    const string DIFFICULTY_KEY = "Difficulty";
    const string PLAYER_NAME_KEY = "PlayerName";
    const string VOLUME_KEY = "Volume";

    const DifficultyLevel DEFAULT_DIFFICULTY_LEVEL = DifficultyLevel.Low;
    const string DEFAULT_PLAYER_NAME = "Karina";
    const float DEFAULT_VOLUME = 1f;

    const float DEFAULT_OPACITY = 1f;
    const float NOT_SELECTED_OPACITY = 0.4f;

    // Private Variables
    DifficultyLevel difficultyLevel;
    string playerName;
    float volume;

    Dictionary<DifficultyLevel, Button> difficultyButtons = new Dictionary<DifficultyLevel, Button>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Initialize the difficulty buttons dictionary
            difficultyButtons[DifficultyLevel.Low] = lowDifficultyButton;
            difficultyButtons[DifficultyLevel.Medium] = mediumDifficultyButton;
            difficultyButtons[DifficultyLevel.High] = highDifficultyButton;

            LoadVolume();
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

    // Load
    public void LoadSettings()
    {
        LoadDifficultyLevel();

        LoadPlayerName();
        SetPlayerNameInputField();

        SetVolumeSlider();
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

    void SetPlayerNameInputField()
    {
        playerNameInputField.SetTextWithoutNotify(playerName);
    }

    void LoadVolume()
    {
        float loadedVolume = PlayerPrefs.GetFloat(VOLUME_KEY, DEFAULT_VOLUME);
        SetVolume(loadedVolume);
    }

    void SetVolumeSlider()
    {
        volumeSlider.SetValueWithoutNotify(volume);
    }

    void SetBackgroundMusicVolume()
    {
        backgroundMusic.volume = volume;
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
    void SetDifficultyLevel(DifficultyLevel newDifficultyLevel)
    {
        difficultyLevel = newDifficultyLevel;
        UpdateButtonOpacities(newDifficultyLevel);
        SaveDifficultyLevel();
    }

    public void SetPlayerName(string newPlayerName)
    {
        playerName = newPlayerName;
        SavePlayerName();
    }

    public void SetVolume(float newVolume)
    {
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

    // Button Event Handler
    public void OnLowButtonClicked()
    {
        SetDifficultyLevel(DifficultyLevel.Low);
    }

    public void OnMediumButtonClicked()
    {
        SetDifficultyLevel(DifficultyLevel.Medium);
    }

    public void OnHighButtonClicked()
    {
        SetDifficultyLevel(DifficultyLevel.High);
    }

    // Buttons Opacity Related
    void UpdateButtonOpacities(DifficultyLevel selectedDifficulty)
    {
        foreach (var pair in difficultyButtons)
        {
            SetButtonOpacity(pair.Value, pair.Key == selectedDifficulty ? DEFAULT_OPACITY : NOT_SELECTED_OPACITY);
        }
    }

    void SetButtonOpacity(Button button, float opacity)
    {
        Color color = button.image.color;
        color.a = opacity;
        button.image.color = color;
    }
}
