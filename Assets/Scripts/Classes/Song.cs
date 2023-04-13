using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Song
{
    public string SongName;
    public AudioClip Intro;
    public List<AudioClip> MainParts;
    public List<AudioClip> Connections;
    public AudioClip Outro;

    private AudioClip _current;

    public AudioClip Next(bool end = false)
    {
        if (_current == null)
        {
            _current = Intro;
        }
        else if (end)
        {
            _current = Outro;
        }
        else if (_current == Intro)
        {
            _current = MainParts.Random();
        }
        else if (MainParts.Contains(_current))
        {
            _current = Connections.Random();
        }

        return _current;
    }
}