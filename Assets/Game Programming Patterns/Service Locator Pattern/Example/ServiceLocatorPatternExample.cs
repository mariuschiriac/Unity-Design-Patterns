//-------------------------------------------------------------------------------------
//	ServiceLocatorPatternExample.cs
//-------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;
using System;

namespace ServiceLocatorPatternExample
{
    public class ServiceLocatorPatternExample : MonoBehaviour
    {
        void Start()
        {
            //Register to the service locator
            TheAudioPlayer audio = new TheAudioPlayer();
            ServiceLocator.RegisterService(audio);
        }

        void Update()
        {
            //play sound
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                var audio=ServiceLocator.GetAudioService();
                if (audio!=null)
                {
                    audio.PlaySound(1);
                }
            }

            //ending sound
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                var audio = ServiceLocator.GetAudioService();
                if (audio != null)
                {
                    audio.StopSound(1);
                }
            }

            //End all sounds
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                var audio = ServiceLocator.GetAudioService();
                if (audio != null)
                {
                    audio.StopAllSounds();
                }
            }

            //Registration log audio class
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                ServiceLocator.EnableAudioLogging();
            }
        }


    }



    /// <summary>
    /// Service locator management class
    /// </summary>
    public class ServiceLocator
    {
        static IAudio AudioService_;
        static NullAudio NullAudioService_;

        public static IAudio GetAudioService() { return AudioService_; }

        /// <summary>
        /// Registration Service
        /// </summary>
        /// <param name="service"></param>
        public static void RegisterService(IAudio service)
        {
            if (service == null)
            {
                // Revert to null service.
                AudioService_ = NullAudioService_;
            }
            else
            {
                AudioService_ = service;
            }
            Debug.Log("[ServiceLocator]Finish Register Service!");
        }

        /// <summary>
        ///  Register audio class with log
        /// </summary>
        public static void EnableAudioLogging()
        {
            // Decorate the existing service.
            IAudio service = new LoggedAudio(ServiceLocator.GetAudioService());

            // Swap it in.
            RegisterService(service);
        }

    }



    /// <summary>
    ///audio port 
    /// </summary>
    public interface IAudio
    {
        void PlaySound(int soundID);
        void StopSound(int soundID);
        void StopAllSounds();
    };

    /// <summary>
    /// The actual audio playback implementation class
    /// </summary>
    public class TheAudioPlayer : IAudio
    {
        public void PlaySound(int soundID)
        {
            // Play sound using console audio api...
            Debug.Log("Play Sound ! ID = "+soundID.ToString());
        }

        public void StopSound(int soundID)
        {
            // Stop sound using console audio api...
            Debug.Log("Stop Sound ! ID = " + soundID.ToString());
        }

        public void StopAllSounds()
        {
            // Stop all sounds using console audio api...
            Debug.Log("Stop All Sound ! ");
        }
    };

    /// <summary>
    ///  null audio class
    /// </summary>
    public class NullAudio : IAudio
    {
        public void PlaySound(int soundID) { /* Do nothing. */ }
        public void StopSound(int soundID) { /* Do nothing. */ }
        public void StopAllSounds() { /* Do nothing. */ }
    };

    /// <summary>
    /// Audio class with log
    /// </summary>
    class LoggedAudio : IAudio
    {

        IAudio wrapped_;
        public LoggedAudio(IAudio wrapped)
        {
            wrapped_ = wrapped;
        }

        public void PlaySound(int soundID)
        {
            Log("[LoggedAudio]Play sound!");
            wrapped_.PlaySound(soundID);
        }

        public void StopSound(int soundID)
        {
            Log("[LoggedAudio]Stop sound!");
            wrapped_.StopSound(soundID);
        }

        public void StopAllSounds()
        {
            Log("[LoggedAudio]Stop all sounds!");
            wrapped_.StopAllSounds();
        }

        private void Log(string message)
        {
            Debug.LogError(message);
            // Code to log message...
        }
    }



}



