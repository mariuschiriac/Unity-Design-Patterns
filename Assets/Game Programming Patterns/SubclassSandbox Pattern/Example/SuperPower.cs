//-------------------------------------------------------------------------------------
//	SuperPower.cs
//-------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

//This is the base class
public abstract class SuperPower
{
    //Abstract interface for sub class
    public abstract void Activate();

    //Some of the tool methods given to the child class
    protected void Move(float speed)
    {
        Debug.Log("Moving with speed " + speed + "!(speed)");
    }

    protected void PlaySound(string coolSound)
    {
        Debug.Log("Playing sound " + coolSound+ "!(coolSound)");
    }

    protected void SpawnParticles(string particles)
    {
        Debug.Log("Spawn Particles "+ particles+ "!(particles)");
    }
}
