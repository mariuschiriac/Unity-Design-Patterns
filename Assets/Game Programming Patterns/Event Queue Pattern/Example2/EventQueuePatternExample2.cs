//-------------------------------------------------------------------------------------
//	EventQueuePatternExample2.cs
// reference ：
// [1] https://www.cnblogs.com/SeaSwallow/p/6543957.html
// [2] Event System Dispatcher :https://www.assetstore.unity3d.com/en/#!/content/12715
//-------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace EventQueuePatternExample2
{
    public class EventQueuePatternExample2 : MonoBehaviour
    {
        // Use this for initialization
        void Start()
        {
            //Add the monitoring of the "START" message identification, and trigger OnReceiveMessageHandler processing after receiving the message.
            MessageDispatcher.AddListener("START", OnReceiveMessageHandler);

            ///Add monitoring of "START" and designated "Filter" filter identification information, 
            ///and trigger OnReceiveMessageHandler processing after receiving the transmission with 
            ///"START" information identification and "Filter" filtering identification information.
            MessageDispatcher.AddListener("START", OnReceiveMessageHandler, "Filter");

            //Add a listener for the "Custom" message identifier, and trigger OnReceiveMessageHandler processing after receiving the message.
            MessageDispatcher.AddListener("Custom", OnReceiveCustomMessageHandler);

            Debug.Log("The initialization is successful, please press the number keys 1, 2, 3 on the keyboard to send the message.");
        }

        // Update is called once per frame
        void Update()
        {

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Message sMessage = new Message();
                sMessage.Type = "START";
                sMessage.Filter = "Filter";
                sMessage.Data = "Hello, I have filtered information";
                //MessageDispatcher sends a message out, the message identifier is "START", and the filter identifier is "Filter".
                MessageDispatcher.SendMessage(sMessage);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Message sMessage = new Message();
                sMessage.Type = "START";
                sMessage.Data = "Hello~~~I am a delayed message";

                //Send after 1 second delay
                sMessage.Delay = 1f;

                //MessageDispatcher sends a message out, the identifier of the message is "START".
                MessageDispatcher.SendMessage(sMessage);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                MyCustomMessage mcm = new MyCustomMessage();
                mcm.MyCustomString = "I am the extra data of the custom message";
                mcm.Type = "Custom";
                MessageDispatcher.SendMessage(mcm);
            }
        }

        /// <summary>
        /// Message recipient
        /// </summary>
        /// <param name="sMessage"></param>
        private void OnReceiveMessageHandler(IMessage sMessage)
        {
            Debug.Log(sMessage.Data.ToString());
        }


        private void OnReceiveCustomMessageHandler(IMessage sMessage)
        {
            Debug.Log((sMessage as MyCustomMessage).MyCustomString);
        }

    }

    /// <summary>
    /// Custom message
    /// </summary>
    public class MyCustomMessage : Message
    {
        public string MyCustomString;
    }


    /// <summary>
    /// The interface of Message class related information is like a short message or a notification.
    /// Message is sent by the MessageDispatcher class to the object of the person who monitors the corresponding information.
    /// The object can pass a command or some data to another or more classes that monitor corresponding information through the MessageDispatcher class.
    /// You can customize the data information you pass by inheriting this interface
    /// </summary>
    public interface IMessage
    {

        /// <summary>
        /// The identifier of the information, which can be any value
        /// </summary>
        string Type { get; set; }


        /// <summary>
        /// Information filtering identification of the listener
        /// </summary>
        string Filter { get; set; }


        /// <summary>
        /// The delay time of information sending, in seconds
        /// </summary>
        float Delay { get; set; }


        /// <summary>
        /// The core data of information
        /// </summary>
        object Data { get; set; }

        /// <summary>
        /// Define whether the information has been sent
        /// </summary>
        bool IsSent { get; set; }

        /// <summary>
        /// Reset this information instance
        /// </summary>
        void Reset();
    }



    /// <summary>
    /// The base class of the message
    /// </summary>
    public class Message : IMessage
    {
        protected object mData = null;
        public object Data
        {
            get { return mData; }
            set { mData = value; }
        }

        protected float mDelay = 0;
        public float Delay
        {
            get { return mDelay; }
            set { mDelay = value; }
        }

        protected bool mIsSent = false;
        public bool IsSent
        {
            get { return mIsSent; }
            set { mIsSent = value; }
        }

        protected string mType = String.Empty;
        public string Type
        {
            get { return mType; }
            set { mType = value; }
        }

        protected string mFilter = String.Empty;
        public string Filter
        {
            get { return mFilter; }
            set { mFilter = value; }
        }

        public void Reset()
        {
            mType = String.Empty;
            mData = null;
            mIsSent = false;
            mFilter = string.Empty;
            mDelay = 0.0f;
        }
    }


    /// <summary>
    /// It is used to perform processing after the information is received by the listener.
    /// </summary>
    /// <param name="rMessage"></param>
    public delegate void MessageHandler(IMessage rMessage);

    /// <summary>
    /// Message distribution management management class
    /// </summary>
    public class MessageDispatcher
    {
        /// <summary>
        /// Create a stub of MessageDispatcher in Unity to handle the sending of delayed messages
        /// </summary>
        private static MessageDispatcherStub sStub = (new GameObject("MessageDispatcherStub")).AddComponent<MessageDispatcherStub>();

        /// <summary>
        /// When some information is not monitored, the processing of information sending is handled by the delegate.
        /// </summary>
        public static MessageHandler MessageNotHandled = null;

        /// <summary>
        /// Store the information indicating the delay
        /// </summary>
        private static List<IMessage> mMessages = new List<IMessage>();

        /// <summary>
        /// Processing corresponding to the main storage information
        /// The first string is the identification of the information, the second string is the identification of the monitoring and filtering information, and the third is the processing of the successful information transmission
        /// </summary>
        private static Dictionary<string, Dictionary<string, MessageHandler>> mMessageHandlers = new Dictionary<string, Dictionary<string, MessageHandler>>();

        /// <summary>
        /// Clear the list of all delay information
        /// </summary>
        public static void ClearMessages()
        {
            mMessages.Clear();
        }

        /// <summary>
        /// Add a monitor for information
        /// </summary>
        /// <param name="rMessageType">ID of monitored information</param>
        /// <param name="rFilter">ID of the monitored information filtering</param>
        /// <param name="rHandler">The processing after the monitored information, that is, the processing after the information is sent by the dispatcher</param>
        public static void AddListener(string rMessageType, MessageHandler rHandler, string rFilter = "")
        {
            Dictionary<string, MessageHandler> lRecipientDictionary = null;

            //Find out whether this information is included in the list of corresponding processing of the information, and if it includes the corresponding filtering and processing.
            if (mMessageHandlers.ContainsKey(rMessageType))
            {
                lRecipientDictionary = mMessageHandlers[rMessageType];
            }
            //If there is no identification of this information processing, create it
            else
            {
                lRecipientDictionary = new Dictionary<string, MessageHandler>();
                mMessageHandlers.Add(rMessageType, lRecipientDictionary);
            }

            //Check whether the information filtering set contains the identification of the filtering information, "" means no filtering identification
            if (!lRecipientDictionary.ContainsKey(rFilter))
            {
                lRecipientDictionary.Add(rFilter, null);
            }

            //Because it is a reference, it will be automatically updated to mMessageHandlers.
            lRecipientDictionary[rFilter] += rHandler;
        }

        /// <summary>
        /// Send message out
        /// </summary>
        /// <param name="rMessage"></param>
        public static void SendMessage(IMessage rMessage)
        {
            //Whether the listener receiving the message is lost, the default is true
            bool lReportMissingRecipient = true;

            //If the information has a delay time, add the information to the delayed information list and wait for the trigger.
            if (rMessage.Delay > 0)
            {
                if (!mMessages.Contains(rMessage))
                {
                    mMessages.Add(rMessage);
                }

                //Avoid triggering the following missing listener code block
                lReportMissingRecipient = false;
            }
            //If there is no delay, just check whether the corresponding processing list of the information contains this information
            else if (mMessageHandlers.ContainsKey(rMessage.Type))
            {
                //Get the corresponding information processing
                Dictionary<string, MessageHandler> lHandlers = mMessageHandlers[rMessage.Type];

                //Traverse the logo of the filtering of the belief information
                foreach (string lFilter in lHandlers.Keys)
                {
                    if (lHandlers[lFilter] == null)
                    {
                        continue;
                    }

                    if (lFilter.Equals(rMessage.Filter))
                    {
                        lHandlers[lFilter](rMessage);
                        rMessage.IsSent = true;
                        lReportMissingRecipient = false;
                    }
                }
            }

            if (lReportMissingRecipient)
            {
                if (MessageNotHandled == null)
                {
                    Debug.LogWarning(string.Format("MessageDispatcher cannot recognize Message.Type: {0} or Message.Filter specified by message filtering", rMessage.Type, rMessage.Filter));
                }
                else
                {
                    MessageNotHandled(rMessage);
                }
            }
        }

        /// <summary>
        /// Responsible for processing the transmission of the delayed information list in each frame
        /// </summary>
        public static void Update()
        {
            //Processing delayed message delivery
            for (int i = 0; i < mMessages.Count; i++)
            {
                IMessage lMessage = mMessages[i];

                // Reduce delay time
                lMessage.Delay -= Time.deltaTime;
                if (lMessage.Delay < 0) { lMessage.Delay = 0; }

                // If it's time, send it immediately
                if (!lMessage.IsSent && lMessage.Delay == 0)
                {
                    SendMessage(lMessage);
                    mMessages.RemoveAt(i);
                }
            }
        }
    }

    public sealed class MessageDispatcherStub : MonoBehaviour
    {
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        void Update()
        {
            MessageDispatcher.Update();
        }

        public void OnDisable()
        {
            MessageDispatcher.ClearMessages();
        }
    }



}

