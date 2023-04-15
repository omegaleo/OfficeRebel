using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI pointsText;

    [SerializeField] WorkDayTimer timer;

    private void Start()
    {
        Player.Instance.StoleItem += OnItemStolen;
        UpdatePoints();
    }

    private void OnItemStolen()
    {
        //timerText.text = timer.FormatTimeDisplay();
        UpdatePoints();
    }

    private void UpdatePoints()
    {
        pointsText.text =
            $@"Money: ${Player.Instance.Money}{Environment.NewLine}Items Stolen: {Player.Instance.ItemsStolen.ToString()}";
    }
}
