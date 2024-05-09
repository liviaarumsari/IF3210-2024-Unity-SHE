using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadBtn : MonoBehaviour
{
    private int slot;
    GameManager gameManager;
    [SerializeField] private SlotInfo slotInfo;
    [SerializeField] private LoadPanel loadPanel;
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
        }
        else
        {
            Debug.Log("[DEBUG] slot" + slot + "NOT TAKEN");
            slotInfo.SetEmpty();
        }
    }

    public void OnClick()
    {
        loadPanel.SetSelectedSlot(slot);
    }
}
