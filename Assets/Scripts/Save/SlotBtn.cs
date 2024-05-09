using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SlotBtn : MonoBehaviour
{
    private int slot;
    GameManager gameManager;
    [SerializeField] private SlotInfo slotInfo;
    [SerializeField] private Image plusIcon;
    [SerializeField] private SavePanel saveUI;
    [SerializeField] private Image currentText;
    // Start is called before the first frame update
    void Start()
    {
        this.slot = transform.GetSiblingIndex();
        gameManager = GameManager.Instance;
        GameState gameState = GameManager.Instance.currentGameState;
        CheckSlot();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckSlot()
    {
        GameState[] gameStates = gameManager.GetAllSavedStates();

        bool isSlotTaken = gameStates.Length >= slot && gameStates[slot] != null;
        if (isSlotTaken)
        {
            slotInfo.SetGameState(gameStates[slot]);
            plusIcon.gameObject.SetActive(false);   
        } else
        {
            plusIcon.gameObject.SetActive(true);    
            slotInfo.gameObject.SetActive(false);
        }

        currentText.gameObject.SetActive(GameManager.Instance.currentGameState == gameStates[slot]);
    }

    public void OnClick()
    {
        Debug.Log("SELECTED SLOT = " + slot);
        saveUI.SetSelectedSlot(slot);
    }
}
