using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using UnityEngine;
using UnityEngine.Serialization;

public class AudioController : InstancedBehaviour<AudioController>
{
    [SerializeField] private List<VoiceLine> _voiceLines = new List<VoiceLine>();
    [SerializeField] private List<SoundEffect> _soundEffects = new List<SoundEffect>();
    [SerializeField] private List<Song> _soundtrack = new List<Song>();

    [Range(0f,1f)] public float SfxVolume = 0.5f; 
    [Range(0f,1f)] public float VoiceVolume = 0.5f; 
    
    private AudioSource _musicSource;

    private Song _currentSong;

    private Coroutine _currentLoop = null;
    
    private void Start()
    {
        // Going to be necessary when we implement the Options menu and need to adjust music volume
        _musicSource = GetComponent<AudioSource>();
        NextSong();
    }

    private IEnumerator MusicLoop()
    {
        while (_musicSource.enabled)
        {
            yield return new WaitForSeconds(1f);

            if (!_musicSource.isPlaying)
            {
                _musicSource.clip = _currentSong.Next();
                _musicSource.Play();
            }
        }
    }

    public void NextSong()
    {
        if (_currentLoop != null)
        {
            StopCoroutine(_currentLoop);
        }
        
        _currentSong = _soundtrack.Where(x => x != _currentSong).ToList().Random();

        _currentLoop = StartCoroutine(MusicLoop());
    }
    
    public void PlaySoundEffect(SoundEffectType type)
    {
        var sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.volume = SfxVolume;
        sfxSource.loop = false;

        var sfx = _soundEffects.Where(x => x.Type == type).ToList().Random();

        if (sfx != null)
        {
            sfxSource.clip = sfx.Clip;
            sfxSource.Play();
        }

        StartCoroutine(DestroyOnFinish(sfxSource));
    }
    
    public void PlayVoiceLine(VoiceLineType type)
    {
        if (Narrator.Instance.IsDuctTaped) return;
        
        var voiceSource = gameObject.AddComponent<AudioSource>();
        voiceSource.volume = VoiceVolume;
        voiceSource.loop = false;

        var voiceLine = _voiceLines.Where(x => x.Type == type).ToList().Random();

        if (voiceLine != null)
        {
            voiceSource.clip = voiceLine.Clip;
            voiceSource.Play();
        }

        StartCoroutine(DestroyOnFinish(voiceSource));
    }

    private IEnumerator DestroyOnFinish(AudioSource source)
    {
        while (source.isPlaying)
        {
            yield return new WaitForSeconds(1f);
        }
        
        Destroy(source);
    }
    
    public void SetMusicVolume(float volume)
    {
        _musicSource.volume = Mathf.Clamp(volume, 0f, 1f);
    }

    public float GetMusicVolume() => (_musicSource != null) ? _musicSource.volume : 1f;
}
