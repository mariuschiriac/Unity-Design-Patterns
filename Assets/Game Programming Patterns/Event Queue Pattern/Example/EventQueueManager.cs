//-------------------------------------------------------------------------------------
//	EventQueueManager.cs
// Reference :https://github.com/GandhiGames/message_queue
// http://gandhigames.co.uk/message-queue/
//-------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace EventQueuePatternExample
{

    public class EventQueueManager
    {
        private static EventQueueManager _instance;
        public static EventQueueManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EventQueueManager();
                }
                return _instance;
            }
        }

        //Generic proxy
        public delegate void EventDelegateX<T>(T e) where T : GameEvent;
        //General agent
        private delegate void EventDelegateX(GameEvent e);

        private Dictionary<System.Type, EventDelegateX> DelegatesMap = new Dictionary<System.Type, EventDelegateX>();
        private Dictionary<System.Delegate, EventDelegateX> DelegateLookupMap = new Dictionary<System.Delegate, EventDelegateX>();

        /// <summary>
        /// Listener
        /// </summary>
        public void AddListener<T>(EventDelegateX<T> del) where T : GameEvent
        {

            EventDelegateX internalDelegate = (e) => { del((T)e); };

            if (DelegateLookupMap.ContainsKey(del) && DelegateLookupMap[del] == internalDelegate)
            {
                return;
            }

            DelegateLookupMap[del] = internalDelegate;

            //Join delegates
            EventDelegateX tempDel;
            if (DelegatesMap.TryGetValue(typeof(T), out tempDel))
            {
                DelegatesMap[typeof(T)] = tempDel += internalDelegate;
            }
            else {
                DelegatesMap[typeof(T)] = internalDelegate;
            }
        }

        public void RemoveListener<T>(EventDelegateX<T> del) where T : GameEvent
        {
            EventDelegateX internalDelegate;
            if (DelegateLookupMap.TryGetValue(del, out internalDelegate))
            {
                EventDelegateX tempDel;
                if (DelegatesMap.TryGetValue(typeof(T), out tempDel))
                {
                    tempDel -= internalDelegate;
                    if (tempDel == null)
                    {
                        DelegatesMap.Remove(typeof(T));
                    }
                    else {
                        DelegatesMap[typeof(T)] = tempDel;
                    }
                }

                DelegateLookupMap.Remove(del);
            }
        }
        /// <summary>
        /// Add events to the queue
        /// </summary>
        public void AddEventToQueue(GameEvent e)
        {
            EventDelegateX del;
            if (DelegatesMap.TryGetValue(e.GetType(), out del))
            {
                del.Invoke(e);
            }
        }

    }

    /// <summary>
    /// Event Priority enum
    /// </summary>
    public enum MessagePriority
    {
        Low,
        Medium,
        High
    }

    /// <summary>
    /// Event Interfaces
    /// </summary>
    public interface IMessageEvent
    {
        DateTime timeRaised { get; }
        float displayTime { get; }
        MessagePriority priority { get; }
        object message { get; }
    }

    /// <summary>
    /// Event entity class
    /// </summary>
    public class MessageEvent : GameEvent, IMessageEvent
    {
        public DateTime timeRaised { private set; get; }
        public MessagePriority priority { private set; get; }
        public float displayTime { private set; get; }
        public object message { private set; get; }

        public MessageEvent(object message, float displayTime, MessagePriority priority)
        {
            this.message = message;
            this.displayTime = displayTime;
            this.priority = priority;
            timeRaised = DateTime.Now;
        }
    }


    /// <summary>
    /// Event abstract class
    /// </summary>
    public abstract class GameEvent
    {

    }

}
