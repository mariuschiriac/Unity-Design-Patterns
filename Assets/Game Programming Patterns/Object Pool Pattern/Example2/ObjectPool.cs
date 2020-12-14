using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public Stack<PooledObject> pool;
    public GameObject prefab;

    private void Awake()
    {
        pool = new Stack<PooledObject>();
    }

    public GameObject TakeObject()
    {
        GameObject go;
        if(pool.Count == 0)
        {
            go = Instantiate(prefab, transform);
            PooledObject pooledObject = go.GetComponent<PooledObject>();
            pooledObject.Init(this);
        }
        else
        {
            go = pool.Pop().gameObject;
        }
        go.SetActive(true);
        return go;
    }

    public void GiveObject(PooledObject pooledObject)
    {
        pool.Push(pooledObject);
    }
}
