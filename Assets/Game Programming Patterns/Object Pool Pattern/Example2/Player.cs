using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public ObjectPool myPool;

    void Start()
    {
        StartCoroutine(Firing());
    }

    IEnumerator Firing()
    {
        int i = 0;
        while (true)
        {
            GameObject go = myPool.TakeObject();
            go.transform.position = transform.position;
            print(i++);
            yield return new WaitForSeconds(1f);
        }
    }
}
