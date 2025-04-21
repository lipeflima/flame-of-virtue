using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    public AudioSource avoid, changeWeapon, woodHit, oreHit;
    public ParticleSystem woodPS, orePS;
    public AudioSource[] footsteps;
    int index;
    public void SetStep()
    {
        if (index > footsteps.Length - 1)
        {
            index = 0;
        }
        else
        {
            footsteps[index].Play();
        }
        
        index++;
    }
    public void SetAvoid() 
    { 
        avoid.Play(); 
    }
    public void SetChangeWeapon()
    {
        changeWeapon.Play();
    }
    public void HitWood()
    {
        woodHit.Play();
        woodPS.Play();
    }
    public void HitOre()
    {
        oreHit.Play();
        orePS.Play();
    }
}
