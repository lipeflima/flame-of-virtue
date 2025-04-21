using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JLGames;
//using Knife.Effects;

public class ParticlesPlay : MonoBehaviour, IObjectPool
{
    public ParticleSystem[] particleSys;
    public bool useEmit;
    public bool alreadyPlay;
    float nextTimeToCheck;
    public float checkRate = 2;
    void Update()
    {
        if(Time.time >= nextTimeToCheck)
        {
            nextTimeToCheck = Time.time + 1f/checkRate;

            if (!alreadyPlay)
            {
                Check();
                alreadyPlay = true;
            }
        }
    }
    void OnEnable()
    {
        alreadyPlay = false;
    }
    void OnDisable()
    {
        alreadyPlay = false;
    }
    void Check()
    {
        for (int i = 0; i < particleSys.Length; i++)
        {
            particleSys[i].Play();
        }
    }
    public void OnObjectSpawn()
    {
        if (useEmit)
        {
            //GetComponent<ParticleGroupEmitter>().Emit(1);
        }
    }
}
