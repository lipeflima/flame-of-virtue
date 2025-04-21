using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JLGames;
public class SoundPlay : MonoBehaviour, IObjectPool
{
    public AudioSource sound;
    bool played;
    void OnEnable()
    {

    }
    void OnDisable() 
    {
        played = false;
    }
    public void OnObjectSpawn()
    {
        sound.Play();
    }
}
