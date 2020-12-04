/// <summary>
/// A simple example of a class inheriting from a command pattern
/// This handles execution of the command as well as unexecution of the command
/// </summary>

using UnityEngine;

class ChangeColorCommand : Command
{
    private Color _color;
    private Color _previousColor;
    private ChangeColorReceiver _receiver;
    private SpriteRenderer _sr;

    //Constructor
    public ChangeColorCommand(ChangeColorReceiver reciever, Color color, GameObject gameObjectTarget)
    {
        this._receiver = reciever;
        this._color = color;
        this._sr = gameObjectTarget.GetComponent<SpriteRenderer>();
        this._previousColor = _sr.color;
    }

    //Execute new command
    public override void Execute()
    {
        _receiver.Execute(_sr, _color);
    }

    //Undo last command
    public override void UnExecute()
    {
        _receiver.Execute(_sr, _previousColor);
    }

    //So we can show this command in debug output easily
    public override string ToString()
    {
        return "ChangeColorCommand : from " +getColorName(_previousColor) + " to " + getColorName(_color);
    }

    private string getColorName(Color c)
    {
        string result = "unknown";
        result = c.r > 0.8f ? "red" : result;
        result = c.b > 0.8f ? "blue" : result;
        result = c.g > 0.8f ? "green" : result;
        return result;
    }

}
