using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTriggerArea : MonoBehaviour
{
    [SerializeField] private BannerMsg shopMsg;
    [SerializeField] private ShopUI shopUi;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        Component[] components = other.gameObject.GetComponents(typeof(Component));
        IShopCustomer customer = other.GetComponent<IShopCustomer>();
        Debug.Log("[DEBUG] is shop within time" + shopUi.isWithinTime);
        if (customer != null && shopUi.isWithinTime)
        {
            shopMsg.Show();
            customer.SetInShopArea(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IShopCustomer customer = other.GetComponent<IShopCustomer>();
        if (customer != null)
        {
            shopUi.Hide();
            customer.SetInShopArea(false);
        }
        this.shopMsg.Hide();    
    }
}
