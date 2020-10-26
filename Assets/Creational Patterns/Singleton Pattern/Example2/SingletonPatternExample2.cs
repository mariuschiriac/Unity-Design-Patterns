//-------------------------------------------------------------------------------------
//	SingletonPatternExample2.cs
//-------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

namespace SingletonPatternExample2
{
    public class SingletonPatternExample2 : MonoBehaviour
    {
        void Start()
        {
            RenderManager.Instance.Show();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                RenderManager.Instance.Show();
            }
        }
    }


    public class RenderManager
    {
        private static RenderManager instance;
        public static RenderManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RenderManager();
                }
                return instance;
            }
        }


        public void Show()
        {
            Debug.Log("RenderManager is a Singleton !");
        }
    }




}