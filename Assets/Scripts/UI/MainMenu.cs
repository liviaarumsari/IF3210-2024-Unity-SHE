using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] LoadPanel loadPanel;
   
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

    public void OnLoadButtonClick()
    {
        loadPanel.gameObject.SetActive(true);
    }

    public void ExitGame()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
