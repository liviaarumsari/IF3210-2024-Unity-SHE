using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet : MonoBehaviour
{
    public enum PetType
    {
        AttackPet,
        HealPet
    }

    public static int GetCost(PetType petType)
    {
        switch (petType)
        {
            default:
            case PetType.AttackPet: return 200;
            case PetType.HealPet: return 300;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
