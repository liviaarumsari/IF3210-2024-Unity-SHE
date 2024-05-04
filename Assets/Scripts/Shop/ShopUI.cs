using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private BannerMsg shopMsg;
    private Transform petsContainer;
    private IShopCustomer customer;
    private Button closeBtn;

    private void Awake()
    {
        petsContainer = transform.Find("PetsContainer");
        closeBtn = transform.Find("CloseButton").GetComponent<Button>();
    }

    private void Start()
    {
        closeBtn.onClick.AddListener(Hide);
        Hide();
    }

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show(IShopCustomer customer)
    {
        shopMsg.Hide(); 
        this.customer = customer;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
