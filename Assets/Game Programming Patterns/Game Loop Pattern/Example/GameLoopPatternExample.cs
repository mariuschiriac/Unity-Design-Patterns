//-------------------------------------------------------------------------------------
//	GameLoopPatternExample.cs
//-------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;



namespace GameLoopPatternExample
{

    public class GameLoopPatternExample : MonoBehaviour
    {
        GameLoopManager GameLoop = new GameLoopManager();

        void Start()
        {
            //DoGameLoop();
            Debug.Log("Unity has a built-in game loop mode, namely Update( ), " +
                "according to the original implementation in the Game Programming Mode book will cause stuck. " +
                "Only the code frame is reserved here, and no calls are made.");
        }

        void Update()
        {

        }

        /// <summary>
        /// GameLoop
        /// </summary>
        void DoGameLoop()
        {
            if (GameLoop==null)
            {
                GameLoop = new GameLoopManager();
            }

            GameLoop.DoGameLoop();
        }
    }


    /// <summary>
    /// Game loop manager
    /// </summary>
    public class GameLoopManager
    {

        /// <summary>
        /// Granularity of game updates
        /// </summary>
        public const float MS_PER_UPDATE = 0.06F;
       
        /// <summary>
        /// Play Game Loop
        /// </summary>
       public  void DoGameLoop()
        {
            double previous = Time.realtimeSinceStartup;
            double lag = 0.0;
            if (Time.realtimeSinceStartup==0f)
            {
                return;
            }
            while (true)
            {
                //current time
                double current = Time.realtimeSinceStartup;
                //Elapsed time
                double elapsed = current - previous;
                previous = current;
                lag += elapsed;
                ProcessInput();

                while (lag >= MS_PER_UPDATE)
                {
                    Update();
                    lag -= MS_PER_UPDATE;
                }

                Render();
            }
        }


        void ProcessInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("[GameLoopManager]You pressed the space key on the keyboard!");
            }
        }

        void Render()
        {
            //do render
        }


        void Update()
        {
            //do update
        }

    }



}
