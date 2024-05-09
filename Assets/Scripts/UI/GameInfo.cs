using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameInfo : MonoBehaviour
{
    GameManager gameManager;

    public TMP_Text playerName;
    public TMP_Text quest;
    public TMP_Text timestamp;
    public Text score;

    void Start()
    {
        if (gameManager == null)
        {
            gameManager = GameManager.Instance;
        }

        if (gameManager.currentGameState == null) { return; }

        playerName.text = gameManager.currentGameState.playerName;
        quest.text = "Quest" + gameManager.currentGameState.GetStageName();
    }

    void Update()
    {
        if (gameManager.currentGameState == null) { return; }

        GameState currentGameState = gameManager.currentGameState;

        // Update Score
        score.text = currentGameState.score.ToString();

        // TODO: Update Timestamp Text
        timestamp.text = DateTime.Now.ToString("HH:mm:ss");
    }
}
