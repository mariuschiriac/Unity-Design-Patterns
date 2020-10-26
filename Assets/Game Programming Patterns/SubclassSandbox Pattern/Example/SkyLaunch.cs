//-------------------------------------------------------------------------------------
//	SkyLaunch.cs
//-------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

//Subclasses
public class SkyLaunch : SuperPower
{
    //Has to have its own version of Activate()
    public override void Activate()
    {
        Debug.Log("--------------------------SkyLaunch SuperPower Activate!--------------------");
        //make own unique features.
        Move(10f);
        PlaySound("SkyLaunch");
        SpawnParticles("SkyLaunch Particles");
    }
}
