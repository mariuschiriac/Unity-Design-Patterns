//-------------------------------------------------------------------------------------
//	DuckingState.cs
//-------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class DuckingState : HeroineBaseState
{
    private Heroine _heroine;
    public DuckingState(Heroine heroine)
    {
        _heroine = heroine;
        Debug.Log("------------------------Heroine in DuckingState~!");
    }


    public void Update()
    {

    }

    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            return;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            Debug.Log("get GetKeyUp.UpArrow!");
            _heroine.SetHeroineState(new StandingState(_heroine));
        }
    }
}
