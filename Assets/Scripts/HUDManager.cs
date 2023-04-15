using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI pointsText;

    [SerializeField] WorkDayTimer timer;

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        timerText.text = timer.FormatTimeDisplay();
        pointsText.text = "Value: " + Player.Instance.Money.ToString() + "\n" +
                          "Stolen: " + Player.Instance.ItemsStolen.ToString();
    }
}
