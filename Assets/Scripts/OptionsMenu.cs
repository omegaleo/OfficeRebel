using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : InstancedBehaviour<OptionsMenu>
{
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;
    [SerializeField] private Slider _voiceSlider;

    [SerializeField] private GameObject _panel;

    private bool _settingValues = false;
    
    private void Start()
    {
        GameManager.Instance.OnOptions += Toggle;
    }

    public void SetVolumes()
    {
        if (_settingValues) return;
        
        AudioController.Instance.SetMusicVolume(_musicSlider.value);
        AudioController.Instance.SfxVolume = Mathf.Clamp(_sfxSlider.value, 0f, 1f);
        AudioController.Instance.VoiceVolume = Mathf.Clamp(_voiceSlider.value, 0f, 1f);
    }

    public void Toggle()
    {
        _panel.SetActive(!_panel.activeSelf);

        _settingValues = true;
        
        if (_panel.activeSelf)
        {
            _musicSlider.value = AudioController.Instance.GetMusicVolume();
            _sfxSlider.value = AudioController.Instance.SfxVolume;
            _voiceSlider.value = AudioController.Instance.VoiceVolume;
        }
        
        _settingValues = false;
    }

    public void RestartGame()
    {
        _panel.SetActive(false);
        GameManager.Instance.RestartGame();
    }
}
