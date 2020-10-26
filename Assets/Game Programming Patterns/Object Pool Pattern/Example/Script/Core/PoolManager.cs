//-------------------------------------------------------------------------------------
//	PoolManager.cs
//-------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace ObjectPoolPatternExample
{

    /// <summary>
    /// Object pool class
    /// </summary>
    public class Pool
    {
        //Stack of objects available in the pool
        private Stack<PoolObject> availableObjStack = new Stack<PoolObject>();
        private bool fixedSize;
        private GameObject poolObjectPrefab;
        private int poolSize;
        private string poolName;

        public Pool(string poolName, GameObject poolObjectPrefab, int initialCount, bool fixedSize)
        {
            this.poolName = poolName;
            this.poolObjectPrefab = poolObjectPrefab;
            this.poolSize = initialCount;
            this.fixedSize = fixedSize;
            //populate the pool
            for (int index = 0; index < initialCount; index++)
            {
                AddObjectToPool(NewObjectInstance());
            }
        }

        /// <summary>
        /// Add objects to the pool, complexity o(1)
        /// </summary>
        /// <param name="po"></param>
        private void AddObjectToPool(PoolObject po)
        {
            //add to pool
            po.gameObject.SetActive(false);
            availableObjStack.Push(po);
            po.IsPooled = true;
        }

        /// <summary>
        /// Create a new object instance
        /// </summary>
        /// <returns></returns>
        private PoolObject NewObjectInstance()
        {
            GameObject go = (GameObject)GameObject.Instantiate(poolObjectPrefab);
            PoolObject po = go.GetComponent<PoolObject>();
            if (po == null)
            {
                po = go.AddComponent<PoolObject>();
            }
            //set name
            po.PoolName = poolName;
            return po;
        }

        //o(1)
        /// <summary>
        /// Get an available object in the pool, if there is no available object in the pool, create a new object instance
        /// </summary>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <returns></returns>
        public PoolObject NextAvailableObject(Vector3 position, Quaternion rotation)
        {
            PoolObject po = null;
            //There are available objects in the pool, directly fetch from the pool
            if (availableObjStack.Count > 0)
            {
                po = availableObjStack.Pop();
            }
            //There are no objects available in the pool, new one
            else if (fixedSize == false)
            {
                //increment size var, this is for info purpose only
                poolSize++;
                Debug.Log(string.Format("Growing pool {0}. New size: {1}", poolName, poolSize));
                //create new object
                po = NewObjectInstance();
            }
            else
            {
                Debug.LogWarning("No object available & cannot grow pool: " + poolName);
            }

            //Set the behavior of the object
            GameObject result = null;
            if (po != null)
            {
                po.IsPooled = false;
                result = po.gameObject;
                result.SetActive(true);

                result.transform.position = position;
                result.transform.rotation = rotation;
            }

            return po;

            }

        /// <summary>
        /// Return the specified object to the pool, complexity o(1)
        /// </summary>
        /// <param name="po"></param>
        public void ReturnObjectToPool(PoolObject po)
        {

            if (poolName.Equals(po.PoolName))
            {

                // We could have used availableObjStack.Contains(po) to check if this object is in pool.
                // While that would have been more robust, it would have made this method O(n) 
                if (po.IsPooled)
                {
                    Debug.LogWarning(po.gameObject.name + " is already in pool. Why are you trying to return it again? Check usage.");
                }
                else
                {
                    AddObjectToPool(po);
                }

            }
            else
            {
                Debug.LogError(string.Format("Trying to add object to incorrect pool {0} {1}", po.PoolName, poolName));
            }
        }
    }

    /// <summary>
    /// PoolManager
    /// </summary>
    public class PoolManager : MonoBehaviour
    {
        /// <summary>
        /// Singleton
        /// </summary>
        private static PoolManager instance;
        public static PoolManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PoolManager();
                }
                return instance;
            }
        }

        /// <summary>
        /// Index of multiple object pools <object pool name, corresponding pool>
        /// </summary>
        private Dictionary<string, Pool> poolMap = new Dictionary<string, Pool>();
        public Dictionary<string, Pool> PoolMap
        {
            get { return poolMap; }
            set { poolMap = value; }
        }

        [Header("[Object pool information, (modification is invalid at runtime)]")]
        public PoolInfo[] poolInfo;


  
        PoolManager()
        {
            //set instance
            instance = this;
        }

  
        public void Init()
        {
            //check for duplicate names
            CheckForDuplicatePoolNames();
            //create pools
            CreatePools();
        }


        /// <summary>
        /// Check whether the object pool has the same name
        /// </summary>
        private void CheckForDuplicatePoolNames()
        {
            for (int index = 0; index < poolInfo.Length; index++)
            {
                string poolName = poolInfo[index].poolName;
                if (poolName.Length == 0)
                {
                    Debug.LogError(string.Format("Pool {0} does not have a name!", index));
                }
                for (int internalIndex = index + 1; internalIndex < poolInfo.Length; internalIndex++)
                {
                    if (poolName.Equals(poolInfo[internalIndex].poolName))
                    {
                        Debug.LogError(string.Format("Pool {0} & {1} have the same name. Assign different names.", index, internalIndex));
                    }
                }
            }
        }

        private void CreatePools()
        {
            foreach (PoolInfo currentPoolInfo in poolInfo)
            {

                Pool pool = new Pool(currentPoolInfo.poolName, currentPoolInfo.prefab, currentPoolInfo.poolSize, currentPoolInfo.fixedSize);

                Debug.Log("Creating Pool: " + currentPoolInfo.poolName);

                //add to mapping dict
                poolMap[currentPoolInfo.poolName] = pool;
            }
        }


        /// <summary>
        ///  null in case the pool does not have any object available & can grow size is false.
        /// </summary>
        /// <param name="poolName">name</param>
        /// <param name="position">position</param>
        /// <param name="rotation">rotation</param>
        /// <returns></returns>
        public PoolObject GetObjectFromPool(string poolName, Vector3 position, Quaternion rotation)
        {
            //GameObject result = null;
            PoolObject result = null;

            if (poolMap.ContainsKey(poolName))
            {
                Pool pool = poolMap[poolName];
                result = pool.NextAvailableObject(position, rotation);
                //scenario when no available object is found in pool
                if (result == null)
                {
                    Debug.LogWarning("No object available in pool. Consider setting fixedSize to false.: " + poolName);
                }

            }
            else
            {
                Debug.LogError("Invalid pool name specified: " + poolName);
            }

            return result;
        }

        public void ReturnObjectToPool(PoolObject po)
        {
            //PoolObject po = go.GetComponent<PoolObject>();
            if (po == null)
            {
                Debug.LogWarning("Specified object is not a pooled instance: " + po.name);
            }
            else
            {
                if (poolMap.ContainsKey(po.PoolName))
                {
                    Pool pool = poolMap[po.PoolName];
                    pool.ReturnObjectToPool(po);
                }
                else
                {
                    Debug.LogWarning("No pool available with name: " + po.PoolName);
                }
            }
        }
    }


    /// <summary>
    /// OP information
    /// </summary>
    [System.Serializable]
    public class PoolInfo
    {
        public string poolName;
        public GameObject prefab;
        public int poolSize;
        public bool fixedSize;
    }
}
