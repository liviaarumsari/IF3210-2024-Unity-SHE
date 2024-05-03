using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShopCustomer 
{
    void BuyPet(Pet.PetType petType);
    void SetInShopArea(bool inShopArea);
}
