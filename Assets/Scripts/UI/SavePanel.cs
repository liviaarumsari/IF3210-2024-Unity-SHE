using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SavePanel : MonoBehaviour
{
    [SerializeField] private BannerMsg saveMsg;
    private int selectedSlot;

    private void Awake()
    {
        
    }

    private void Start()
    {
        Hide();
    }

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {

    }

    public void Show()
    {
        saveMsg.Hide();
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetSelectedSlot(int slot)
    {
        selectedSlot = slot;
    }


    public void OnSaveBtnClick()
    {
        Debug.Log("SELECTED SLOT = " + selectedSlot);
        GameManager.Instance.SaveCurrentGame(selectedSlot);
    }
}
