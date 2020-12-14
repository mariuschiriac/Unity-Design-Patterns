using UnityEngine;

public class PooledObject : MonoBehaviour
{
    private ObjectPool pool;
    public void Init(ObjectPool pool)
    {
        this.pool = pool;
    }

    private void OnDisable()
    {
        pool.GiveObject(this);
    }
}
