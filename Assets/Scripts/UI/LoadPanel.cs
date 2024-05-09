using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPanel : MonoBehaviour
{
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


    public void OnLoadBtnClick()
    {
        GameManager.Instance.LoadSavedGame(selectedSlot);
        Hide();
    }
}
