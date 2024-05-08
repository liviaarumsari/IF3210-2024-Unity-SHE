using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGame : MonoBehaviour
{
    GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
    }

    public void GetSavedGamesList()
    {
        GameState[] gameStates = gameManager.GetAllSavedStates();

        for (int i = 0; i < 3; i++)
        {
            if (gameStates[i] != null)
            {
                // TODO: Remove debugging line
                Debug.Log("Loading slot: " + i.ToString());
                Debug.Log(gameStates[i].ToString());
                Debug.Log(gameStates[i].currentStageIndex);
                Debug.Log(gameStates[i].lastSavedTime);
            }
        }
    }
}
