using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpotlight : MonoBehaviour
{

    public GameObject player;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);
    }
}
