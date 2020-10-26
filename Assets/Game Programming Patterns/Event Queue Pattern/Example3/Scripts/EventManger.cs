//-------------------------------------------------------------------------------------
//	EventManger.cs
//-------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EventQueuePatternExample3
{
    /// <summary>
    /// Message management class-singleton
    /// </summary>
    public class EventManger
    {
        private static EventManger _Instance;
        public static EventManger Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new EventManger();
                }
                return _Instance;
            }
        }

        public delegate void EventHandler(BaseEventMsg Msg);

        // Message ID, proxy storage dictionary Map-<(int)EventType, EventHandler proxy>
        private Dictionary<int, EventHandler> m_EventHandlerMap = new Dictionary<int, EventHandler>();

        /// <summary>
        /// Registration issue
        /// </summary>
        public void RegisterEvent(EventType eventType, EventHandler eventHandler)
        {

            if (m_EventHandlerMap == null)
            {
                m_EventHandlerMap = new Dictionary<int, EventHandler>();
            }
            int eventTypeID = (int)eventType;
            if (m_EventHandlerMap.ContainsKey(eventTypeID))
            {
                m_EventHandlerMap[eventTypeID] += eventHandler;
            }
            else
            {
                m_EventHandlerMap.Add(eventTypeID, eventHandler);
            }

        }

        /// <summary>
        /// Deregister event-deregister all registration messages under this EventType
        /// </summary>
        public void UnRegisterEvent(EventType eventType)
        {
            int eventTypeID = (int)eventType;

            if (m_EventHandlerMap == null)
            {
                return;
            }

            if (m_EventHandlerMap.ContainsKey(eventTypeID))
            {
                m_EventHandlerMap.Remove(eventTypeID);
            }
        }

        /// <summary>
        /// Unregister event-unregister the EventHandler specified under this EventType
        /// </summary>
        public void UnRegisterEvent(EventType eventType, EventHandler eventHandler)
        {
            int eventTypeID = (int)eventType;

            if (m_EventHandlerMap == null)
            {
                return;
            }
            //Delete the message response specified by eventHandler
            if (m_EventHandlerMap.ContainsKey(eventTypeID))
            {
                m_EventHandlerMap[eventTypeID] -= eventHandler;
            }
        }



        /// <summary>
        /// Find the event message bound to the corresponding message ID from the message ID, agent storage dictionary Map
        /// </summary>
        public void SendEvent(EventType eventType, BaseEventMsg Msg)
        {
            int eventTypeID = (int)eventType;

            if (m_EventHandlerMap == null)
            {
                return;
            }

            if (m_EventHandlerMap.ContainsKey(eventTypeID))
            {
                if (m_EventHandlerMap[eventTypeID] != null)
                {
                    m_EventHandlerMap[eventTypeID](Msg);
                }
            }
        }

        public void SendEvent(EventType eventType, params object[] inParams)
        {
            BaseEventMsg Msg = new BaseEventMsg();
            if (Msg != null)
            {
                Msg.SetEventMsg(eventType, inParams);
                SendEvent(eventType, Msg);
            }
        }
    }


    public class BaseEventMsg
    {
        public EventType MsgType;
        public object[] Params = null;

        public BaseEventMsg()
        {

        }

        public BaseEventMsg(EventType inMsgType, params object[] InParams)
        {
            MsgType = inMsgType;
            Params = InParams;
        }

        public void SetEventMsg(EventType inMsgType, params object[] InParams)
        {
            MsgType = inMsgType;
            Params = InParams;
        }

    }

}
