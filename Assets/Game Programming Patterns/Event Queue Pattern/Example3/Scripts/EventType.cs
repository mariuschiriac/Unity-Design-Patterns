//-------------------------------------------------------------------------------------
//	EventType.cs
//-------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

namespace EventQueuePatternExample3
{
    /// <summary>
    /// Message type enumeration
    /// </summary>
    public enum EventType
    {
        None,

        //UI event message
        UI_Event1 = 1,
        UI_Event2 = 2,
        UI_Event3 = 3,
        UI_Event4 = 4,
        UI_Event5 = 5,

        //Render event message
        Render_Event1 = 100,
        Render_Event2,
    }
}