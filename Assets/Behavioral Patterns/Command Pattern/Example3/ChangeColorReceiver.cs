using UnityEngine;

public class ChangeColorReceiver
{
    public void Execute(SpriteRenderer sr, Color color)
    {
        sr.color = color;
    }
}
