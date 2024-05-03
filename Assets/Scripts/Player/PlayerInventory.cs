using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventory : MonoBehaviour, IShopCustomer
{
    private int coins = 0;
    private Pet.PetType[] pets = {};
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyPet(Pet.PetType petType)
    {
        int cost = Pet.GetCost(petType);
        if (coins >= cost)
        {
            pets.Append(petType);
            coins -= cost;
        }
    }

    public void SetInShopArea(bool inShopArea) { 
       PlayerMovementSimple playerMovement = this.gameObject.GetComponent<PlayerMovementSimple>();  
       if (playerMovement != null) {
            playerMovement.inShopArea = inShopArea;
       } 
    }
}
