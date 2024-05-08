using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SlotBtn : MonoBehaviour
{
    private int slot;
    private Button btn;
    GameManager gameManager;
    [SerializeField] private SlotInfo slotInfo;
    [SerializeField] private Image plusIcon;
    [SerializeField] private SaveUI saveUI;
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
        foreach (GameState gameState in gameStates)
        {
            Debug.Log(gameState);
            if (gameState == null)
            {
                Debug.Log("[DEBUG] masih kosong nih bos");
            }
        }

        bool isSlotTaken = gameStates.Length >= slot && gameStates[slot] != null;
        Debug.Log("[DEBUG] IS SLOT TAKEN " + slot + isSlotTaken.ToString());    
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
        saveUI.SetSelectedSlot(slot);   
    }
}
