//-------------------------------------------------------------------------------------
//	SingletonPatternExample3.cs
//-------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

namespace SingletonPatternExample3
{

    public class SingletonPatternExample3 : MonoBehaviour
    {
        void Start()
        {
            //A
            SingletonA.Instance.DoSomething();

            //B
            SingletonB.Instance.DoSomething();
        }

    }

    /// <summary>
    /// Singleton base class (abstract class, generic, other classes only need to inherit this class to become singleton class)
    /// Those who inherit this class become a singleton class
    /// </summary>
    public abstract class Singleton<T>
        where T : class, new()
    {
        private static T _instance = null;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new T();
                    return _instance;
                }
                return _instance;
            }
        }

        protected virtual void Awake()
        {
            _instance = this as T;
        }
    }


    /// <summary>
    /// Singleton inherited from Singleton<T>
    /// </summary>
    public class SingletonA : Singleton<SingletonA>
    {
        public void DoSomething()
        {
            Debug.Log("SingletonA:DoSomething!");
        }
    }


    public class SingletonB : Singleton<SingletonB>
    {
        public void DoSomething()
        {
            Debug.Log("SingletonB:DoSomething!");
        }
    }


}