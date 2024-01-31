
using UnityEngine;

public class CubeSpawn : MonoBehaviour
{
    public GameObject cubePrefab;
    ObjectPooler pooler;

    private void Start()
    {
        pooler = ObjectPooler.instance;
    }

    void FixedUpdate()
    {
        pooler.SpawnFromPool("Cube", transform.position, Quaternion.identity);
    }
}
