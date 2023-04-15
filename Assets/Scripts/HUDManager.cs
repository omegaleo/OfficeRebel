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

    private bool _canRestart = false;
    
    private void Start()
    {
        Player.Instance.StoleItem += OnItemStolen;
        UpdatePoints();

        GameManager.Instance.OnAnyKey += OnAnyKey;
    }

    private void OnAnyKey()
    {
        if (_gameOverPanel.activeSelf && _canRestart)
        {
            GameManager.Instance.RestartGame();
        }
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
        _canRestart = false;
        StartCoroutine(CanResetGame());
    }

    private IEnumerator CanResetGame()
    {
        yield return new WaitForSeconds(0.5f);
        _canRestart = true;
    }
}
