using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;

    public Dictionary<string, Queue<GameObject>> poolDicitionary;

    #region Singleton
    public static ObjectPooler instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    void Start()
    {
        poolDicitionary = new Dictionary<string, Queue<GameObject>>();
        foreach (var pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDicitionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDicitionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool doesnt contain key");
            return null;
        }

        GameObject objectToSpawn = poolDicitionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        IPooledObject obj = objectToSpawn.GetComponent<IPooledObject>();
        if(obj != null)
        {
            obj.OnObjectSpawned();
        }
        poolDicitionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }

}
