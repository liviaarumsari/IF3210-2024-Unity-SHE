using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene04 : MonoBehaviour
{
    GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
    }

    void Update()
    {

    }

    public void Next()
    {
        gameManager.currentGameState.AdvanceToNextStage();
    }
}
