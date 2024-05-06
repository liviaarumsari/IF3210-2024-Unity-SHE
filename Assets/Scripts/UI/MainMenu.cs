using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    GameManager gameManager;
   
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    void Update()
    {
        
    }

    public void OnPlayBtnClick()
    {
        gameManager.StartNewGame();
    }
}
