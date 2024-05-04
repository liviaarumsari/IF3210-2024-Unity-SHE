using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTriggerArea : MonoBehaviour
{
    [SerializeField] private BannerMsg bannerMsg;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        PlayerMovementSimple player = other.GetComponent<PlayerMovementSimple>();
        if (player != null)
        {
            bannerMsg.Show();
            player.inQuestArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        this.bannerMsg.Hide();
    }
}
