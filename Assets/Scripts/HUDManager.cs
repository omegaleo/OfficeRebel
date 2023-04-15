using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;
using TMPro;

public class HUDManager : InstancedBehaviour<HUDManager>
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI pointsText;

    [SerializeField] WorkDayTimer timer;

    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private TMP_Text _gameOverText;

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

    public void GameOver()
    {
        _gameOverPanel.SetActive(true);
        _gameOverText.text = String.Format(_gameOverText.text, Player.Instance.Money, Player.Instance.ItemsStolen);
    }
}
