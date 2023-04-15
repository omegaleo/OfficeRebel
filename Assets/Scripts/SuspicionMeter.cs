using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuspicionMeter : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Image _knob;
    [SerializeField] private List<Sprite> _faces;

    private void Start()
    {
        GameManager.Instance.OnSuspicionIncreased += OnSuspicionIncreased;
    }

    private void OnSuspicionIncreased()
    {
        _slider.value = GameManager.Instance.Suspicion;
        SetFace();
    }

    public void SetSuspicion()
    {
        GameManager.Instance.Suspicion = Mathf.RoundToInt(_slider.value);
        // Set the BossRadius
        BossRadius.Instance.SetRadius();
        SetFace();
        ITDoor.Instance.SetDoor();
    }

    public void SetFace()
    {
        switch (GameManager.Instance.Suspicion)
        {
            case >= 8:
                _knob.sprite = _faces[3];
                break;
            case >= 5:
                _knob.sprite = _faces[2];
                break;
            case >= 3:
                _knob.sprite = _faces[1];
                break;
            default:
                _knob.sprite = _faces[0];
                break;
        }
    }
}
