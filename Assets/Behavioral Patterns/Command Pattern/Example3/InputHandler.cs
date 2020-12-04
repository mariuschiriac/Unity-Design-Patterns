/// <summary>
/// The 'Invoker' class that makes calls to execute the commands
/// </summary>

using UnityEngine;
using System.Collections.Generic;

public class InputHandler : MonoBehaviour
{
    public float moveDistance = 10f;
    public GameObject objectTarget;

    private MoveReceiver moveReceiver;
    private ChangeColorReceiver changeColorReceiver;
    private List<Command> commands = new List<Command>();
    private int currentCommandNum = 0;

    void Start()
    {
        moveReceiver = new MoveReceiver();
        changeColorReceiver = new ChangeColorReceiver();

        if (objectTarget == null)
        {
            Debug.LogError("objectTarget must be assigned via inspector");
            this.enabled = false;
        }
    }


    public void Undo()
    {
        if (currentCommandNum > 0)
        {
            currentCommandNum--;
            Command Command = commands[currentCommandNum];
            Command.UnExecute();
        }
    }

    public void Redo()
    {
        if (currentCommandNum < commands.Count)
        {
            Command Command = commands[currentCommandNum];
            currentCommandNum++;
            Command.Execute();
        }
    }

    private void InsertCommand(Command command)
    {
        commands.Insert(currentCommandNum++, command);
    }

    private void Move(MoveDirection direction)
    {
        Command command = new MoveCommand(moveReceiver, direction, moveDistance, objectTarget);
        command.Execute();
        InsertCommand(command);
    }

    private void ChangeColor(Color color)
    {
        Command command = new ChangeColorCommand(changeColorReceiver, color, objectTarget);
        command.Execute();
        InsertCommand(command);
    }


    //Simple move commands to attach to UI buttons
    public void MoveUp() { Move(MoveDirection.up); }
    public void MoveDown() { Move(MoveDirection.down); }
    public void MoveLeft() { Move(MoveDirection.left); }
    public void MoveRight() { Move(MoveDirection.right); }

    public void Blue() { ChangeColor(Color.blue); }
    public void Red() { ChangeColor(Color.red); }
    public void Green() { ChangeColor(Color.green); }

    //Shows what's going on in the command list
    void OnGUI()
    {
        string label = "   start";
        if (currentCommandNum == 0)
        {
            label = ">" + label;
        }
        label += "\n";

        for (int i = 0; i < commands.Count; i++)
        {
            if (i == currentCommandNum - 1)
                label += "> " + commands[i].ToString() + "\n";
            else
                label += "   " + commands[i].ToString() + "\n";

        }
        GUI.Label(new Rect(0, 0, 400, 800), label);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveUp();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveDown();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Redo();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            Undo();
        }
    }
}
