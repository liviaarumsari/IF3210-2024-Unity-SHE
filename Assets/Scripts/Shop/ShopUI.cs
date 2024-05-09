using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private BannerMsg shopMsg;
    private IShopCustomer customer;
    public bool isWithinTime = true;

    private void Awake()
    {
    }

    private void Start()
    {
        Debug.Log("[DEBUG] start is within time" + isWithinTime);
        Hide();
        isWithinTime = true;
        Debug.Log("[DEBUG] start is within time 2" + isWithinTime);
    }

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Show(IShopCustomer customer)
    {
        if (isWithinTime)
        {
            shopMsg.Hide();
            this.customer = customer;
            gameObject.SetActive(true);
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
