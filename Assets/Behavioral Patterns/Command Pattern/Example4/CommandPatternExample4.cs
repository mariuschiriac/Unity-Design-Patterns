//-------------------------------------------------------------------------------------
//	CommandPatternExample4.cs
//-------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CommandPatternExample4
{
    public class CommandPatternExample4 : MonoBehaviour
    {
        void Start()
        {
            Invoker theInvoker = new Invoker();

            Command theCommand = null;

            theCommand = new ConcreteCommand1(new Receiver1(), "hi");
            theInvoker.AddCommand(theCommand);
            theCommand = new ConcreteCommand2(new Receiver2(), 666);
            theInvoker.AddCommand(theCommand);

            theInvoker.ExecuteCommand();
        }

    }


    public abstract class Command
    {
        public abstract void Execute();
    }


    public class ConcreteCommand1 : Command
    {
        Receiver1 m_Receiver = null;
        string m_Command = "";

        public ConcreteCommand1(Receiver1 Receiver, string param)
        {
            m_Receiver = Receiver;
            m_Command = param;
        }

        public override void Execute()
        {
            m_Receiver.Action(m_Command);
        }
    }

    public class ConcreteCommand2 : Command
    {
        Receiver2 m_Receiver = null;
        int m_Param = 0;

        public ConcreteCommand2(Receiver2 Receiver, int Param)
        {
            m_Receiver = Receiver;
            m_Param = Param;
        }

        public override void Execute()
        {
            m_Receiver.Action(m_Param);
        }
    }

    public class Receiver1
    {
        public Receiver1() { }
        public void Action(string param)
        {
            Debug.Log("Receiver1.Action:Command[" + param + "]");
        }
    }

    public class Receiver2
    {
        public Receiver2() { }
        public void Action(int Param)
        {
            Debug.Log("Receiver2.Action:Param[" + Param.ToString() + "]");
        }
    }


    public class Invoker
    {
        List<Command> m_Commands = new List<Command>();

       
        public void AddCommand(Command theCommand)
        {
            m_Commands.Add(theCommand);
        }


        public void ExecuteCommand()
        {
         
            foreach (Command theCommand in m_Commands)
                theCommand.Execute();
          
            m_Commands.Clear();
        }
    }







}