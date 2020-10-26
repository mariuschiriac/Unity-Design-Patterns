//-------------------------------------------------------------------------------------
//	GroundDive.cs
//-------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class GroundDive : SuperPower
{
    //Has to have its own version of Activate()（）
    public override void Activate()
    {
        Debug.Log("--------------------------GroundDive SuperPower Activate!--------------------");
        //make own unique features. 
        Move(15f);
        PlaySound("GroundDive");
        SpawnParticles("GroundDive Particles");
    }
}
