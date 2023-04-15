using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuspicionMeter : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private void Start()
    {
        GameManager.Instance.OnSuspicionIncreased += OnSuspicionIncreased;
    }

    private void OnSuspicionIncreased()
    {
        _slider.value = GameManager.Instance.Suspicion;
    }

    public void SetSuspicion()
    {
        GameManager.Instance.Suspicion = Mathf.RoundToInt(_slider.value);
    }
}
