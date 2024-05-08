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
        Debug.Log(gameStates.ToString());
    }
}
