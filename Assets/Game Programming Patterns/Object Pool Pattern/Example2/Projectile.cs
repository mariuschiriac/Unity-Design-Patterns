using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : PooledObject
{

    void Update()
    {
        transform.position += Vector3.right * 2 * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
    }
}
