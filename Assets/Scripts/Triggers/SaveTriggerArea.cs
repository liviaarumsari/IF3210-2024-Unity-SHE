using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTriggerArea : MonoBehaviour
{
    [SerializeField] private BannerMsg bannerMsg;
    [SerializeField] private SaveUI saveUi;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        PlayerMovementSimple player = other.GetComponent<PlayerMovementSimple>();
        if (player != null)
        {
            bannerMsg.Show();
            player.inSaveArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        bannerMsg.Hide();
        saveUi.Hide();
    }
}
