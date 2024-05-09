using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    GameSettingsManager gameSettingsManager;

    // UI References
    public Button lowDifficultyButton;
    public Button mediumDifficultyButton;
    public Button highDifficultyButton;
    public TMPro.TMP_InputField playerNameInputField;
    public Slider volumeSlider;
    public AudioSource backgroundMusic;

    // Constants
    const float DEFAULT_OPACITY = 1f;
    const float NOT_SELECTED_OPACITY = 0.4f;

    Dictionary<DifficultyLevel, Button> difficultyButtons = new Dictionary<DifficultyLevel, Button>();

    // Start is called before the first frame update
    void Start()
    {
        if (gameSettingsManager == null)
        {
            gameSettingsManager = GameSettingsManager.Instance;
        }

        // Initialize the difficulty buttons dictionary
        difficultyButtons[DifficultyLevel.Low] = lowDifficultyButton;
        difficultyButtons[DifficultyLevel.Medium] = mediumDifficultyButton;
        difficultyButtons[DifficultyLevel.High] = highDifficultyButton;

        // Update the UI based on data from game settings manager
        GetAndSetExistingData();
    }

    void GetAndSetExistingData()
    {
        SetDifficultyLevel(gameSettingsManager.GetDifficultyLevel());
        SetPlayerName(gameSettingsManager.GetPlayerName());
        SetVolume(gameSettingsManager.GetVolume());
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

    public void OnPlayerNameChanged(string newName)
    {
        gameSettingsManager.SetPlayerName(newName);
    }

    public void OnSfxVolumeChanged(float newVolume)
    {
        gameSettingsManager.SetVolume(newVolume);
    }

    // Setter
    void SetDifficultyLevel(DifficultyLevel newDifficultyLevel)
    {
        gameSettingsManager.SetDifficultyLevel(newDifficultyLevel);
        UpdateButtonOpacities(newDifficultyLevel);
    }

    public void SetPlayerName(string playerName)
    {
        playerNameInputField.SetTextWithoutNotify(playerName);
    }

    public void SetVolume(float volume)
    {
        volumeSlider.SetValueWithoutNotify(volume);
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
