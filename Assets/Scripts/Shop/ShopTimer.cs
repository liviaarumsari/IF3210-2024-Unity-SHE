using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopTimer : MonoBehaviour
{
    private TMP_Text timerText;
    private int countdownTime = 1 * 60;  // 3 mins
    [SerializeField] private ShopUI shopUi;
    void Start()
    {
        timerText = gameObject.GetComponent<TMP_Text>();   
        StartCoroutine(CountdownTimer());
    }
    void Update()
    {

    }

    IEnumerator CountdownTimer()
    {
        while (countdownTime > 0)
        {
            UpdateTimerDisplay();
            yield return new WaitForSeconds(1);
            countdownTime--;
        }

        UpdateTimerDisplay();
        shopUi.isWithinTime = false;
        shopUi.Hide();
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(countdownTime / 60);
        int seconds = Mathf.FloorToInt(countdownTime % 60);
        timerText.text = "Time to shop:\n" + string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
