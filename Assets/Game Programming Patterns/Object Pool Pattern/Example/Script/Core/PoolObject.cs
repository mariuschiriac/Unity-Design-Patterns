//-------------------------------------------------------------------------------------
//	PoolObject.cs
//-------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

namespace ObjectPoolPatternExample
{

    /// <summary>
    /// Components of objects in the object pool
    /// </summary>
    public class PoolObject : MonoBehaviour
    {

        public string PoolName;
        //Is it in the pool (not yet used, to be used)
        public bool IsPooled;
    }
}
