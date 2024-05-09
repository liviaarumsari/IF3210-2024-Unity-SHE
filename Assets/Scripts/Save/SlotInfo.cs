using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SlotInfo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGameState(GameState gameState)
    {
        Debug.Log("[DEBUG] masuk SET GAME STATE");
        SetTextField("Quest", "Quest " + gameState.GetStageName());
        Debug.Log("DEBUG DATA SAVED: " + gameState.lastSavedTime.ToString());
        SetTextField("DateSaved", gameState.lastSavedTime.DateTime.ToString("dd/MM/yy"));  
    }

    void SetTextField(string objectName, string value)
    {
        GameObject textObject = transform.Find(objectName).gameObject;
        if (textObject != null)
        {
            TMP_Text textComponent = textObject.GetComponent<TMP_Text>();

            if (textComponent != null)
            {
                Debug.Log("[DEBUG] UPDATINGGG");
                textComponent.text = value;
            }
            else
            {
                Debug.LogWarning("Text component not found on the child GameObject.");
            }
        }
    }
}
