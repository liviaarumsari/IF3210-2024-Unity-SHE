using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager Instance { get; private set; }
    GameSettingsManager gameSettingsManager;

    // Current game state
    public GameState currentGameState { get; private set; }

    // Saved game states
    GameState[] savedGameStates = new GameState[3];

    // Constants
    const string GAME_STATES_FILENAME = "gameState_{0}.json";

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
    }

    void Start()
    {
        gameSettingsManager = GameSettingsManager.Instance;
    }

    public void StartNewGame()
    {
        currentGameState = new GameState(gameSettingsManager);
    }

    public void SaveCurrentGame(int slot)
    {
        if (slot >= 0 && slot < savedGameStates.Length)
        {
            currentGameState.SaveGame();
            savedGameStates[slot] = currentGameState;
            SaveGameState(slot);
        }
    }

    public void LoadSavedGame(int slot)
    {
        if (slot >= 0 && slot < savedGameStates.Length)
        {
            LoadGameState(slot);
            if (savedGameStates[slot] != null)
            {
                currentGameState = savedGameStates[slot];
                currentGameState.ResumeGame();
            }
        }
    }

    void SaveGameState(int slot)
    {
        string filename = string.Format(GAME_STATES_FILENAME, slot);
        string path = Path.Combine(Application.persistentDataPath, filename);
        string json = JsonUtility.ToJson(savedGameStates[slot]);
        File.WriteAllText(path, json);
    }

    void LoadGameState(int slot)
    {
        string filename = string.Format(GAME_STATES_FILENAME, slot);
        string path = Path.Combine(Application.persistentDataPath, filename);

        if (!File.Exists(path))
        {
            return;
        }

        string json = File.ReadAllText(path);
        savedGameStates[slot] = JsonUtility.FromJson<GameState>(json);
    }

    public GameState[] GetAllSavedStates()
    {
        for (int i = 0; i < savedGameStates.Length; i++)
        {
            LoadGameState(i);
        }
        return savedGameStates;
    }
}
