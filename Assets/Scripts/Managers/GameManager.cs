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
    List<GameState> savedGameStates = new List<GameState>();

    // Constants
    const int MAX_GAME_STATES = 3;
    const string GAME_STATES_FILENAME = "gameStates.json";

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

    public void SaveCurrentGame()
    {
        currentGameState.SaveGame();

        savedGameStates.Add(currentGameState);

        if (savedGameStates.Count > MAX_GAME_STATES)
        {
            savedGameStates.RemoveAt(0);
        }

        SaveAllGameStates();
    }

    public void LoadSavedGame(int index)
    {
        if (index >= 0 && index < savedGameStates.Count)
        {
            currentGameState = savedGameStates[index];
            currentGameState.ResumeGame();
        }
    }

    void SaveAllGameStates()
    {
        string path = Path.Combine(Application.persistentDataPath, GAME_STATES_FILENAME);
        string json = JsonUtility.ToJson(new { gameStates = savedGameStates });
        File.WriteAllText(path, json);
    }

    void LoadAllGameStates()
    {
        string path = Path.Combine(Application.persistentDataPath, GAME_STATES_FILENAME);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            savedGameStates = JsonUtility.FromJson<List<GameState>>(json);
        }
    }

    public List<GameState> GetSavedGameStates()
    {
        LoadAllGameStates();
        return savedGameStates;
    }
}