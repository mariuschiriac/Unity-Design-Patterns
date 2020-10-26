using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace ObjectPoolPatternExample
{
    public class ObjectPoolExample : MonoBehaviour
    {

        public string poolName;

        // public List<GameObject> ObjectList = new List<GameObject>();
        public List<PoolObject> ObjectList = new List<PoolObject>();

        public void Start()
        {
           
            PoolManager.Instance.Init();
           
            GetObjectFromPool();
        }


        public void GetObjectFromPool()
        {
            if (ObjectList == null)
            {
                return;
            }

            Vector3 pos = new Vector3();
            pos.x = Random.Range(-5, 6);
            pos.y = 0f;
            pos.z = Random.Range(-5, 6);

            PoolObject po = PoolManager.Instance.GetObjectFromPool(poolName, pos, Quaternion.identity);
            if (po)
            {
                ObjectList.Add(po);
            }
        }

        public void ReturnOneObjectToPool()
        {
            if (ObjectList == null || ObjectList.Count <= 0)
            {
                return;
            }

            PoolManager.Instance.ReturnObjectToPool(ObjectList[0]);
            ObjectList.Remove(ObjectList[0]);
        }


        public void ReturnAllObjectToPool()
        {
            if (ObjectList == null)
            {
                return;
            }

            foreach (PoolObject po in ObjectList)
            {
                PoolManager.Instance.ReturnObjectToPool(po);
            }
            ObjectList.Clear();
        }
    }
}