//-------------------------------------------------------------------------------------
//	FlashSpeed.cs
//-------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class FlashSpeed : SuperPower
{
    //Has to have its own version of Activate()）
    public override void Activate()
    {
        Debug.Log("--------------------------FlashSpeed SuperPower Activate!--------------------");
        //make own unique features.
        Move(100f);
        PlaySound("Flash Speed");
        SpawnParticles("Flash Particles");
    }
}
