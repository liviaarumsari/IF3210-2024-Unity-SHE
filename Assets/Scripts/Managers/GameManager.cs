using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        for (int i = 0; i < savedGameStates.Length; i++)
        {
            LoadGameState(i);
        }
    }

    public void StartNewGame()
    {
        currentGameState = new GameState(gameSettingsManager);
    }

    public void SaveCurrentGame(int slot)
    {
        if (slot >= 0 && slot < savedGameStates.Length && GameConfig.StageToSceneName[currentGameState.currentStage] == Scene.SceneName.IdleScene)
        {
            Debug.Log("Saving game state with player name " + currentGameState.playerName);
            currentGameState.SaveGame();
            savedGameStates[slot] = currentGameState;
            SaveGameState(slot);
            SceneManager.LoadScene(Scene.GetSceneName(Scene.SceneName.MainMenu));
        }
    }

    public void LoadSavedGame(int slot)
    {
        if (slot >= 0 && slot < savedGameStates.Length)
        {
            if (savedGameStates[slot] != null)
            {
                currentGameState = savedGameStates[slot];
                currentGameState.LoadGame();
            }
        }
    }

    void SaveGameState(int slot)
    {
        string filename = string.Format(GAME_STATES_FILENAME, slot);
        string path = Path.Combine(Application.persistentDataPath, filename);
        string json = JsonUtility.ToJson(savedGameStates[slot]);
        File.WriteAllText(path, json);

        // TODO: remove debugging line
        Debug.Log("Saving to : " + filename);
        Debug.Log("Saving to path : " + path);
        Debug.Log("json : " + json);
    }

    void LoadGameState(int slot)
    {
        string filename = string.Format(GAME_STATES_FILENAME, slot);
        string path = Path.Combine(Application.persistentDataPath, filename);

        if (!File.Exists(path))
        {
            savedGameStates[slot] = null;
            return;
        }

        string json = File.ReadAllText(path);
        savedGameStates[slot] = JsonUtility.FromJson<GameState>(json);
        
        // TODO: remove debugging line
        Debug.Log("Read from : " + path);
        Debug.Log("Read json : " + json);
        Debug.Log("Read slot : " + savedGameStates[slot].ToString());
    }

    public GameState[] GetAllSavedStates()
    {
        return savedGameStates;
    }
}
