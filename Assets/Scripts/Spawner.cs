using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{

    public List<GameObject> cubes;
    ObjectPooler pooler;

    public float spawnRate = 2f;
    private float timer = 0;

    [SerializeField] private float leftPosition = 2f;
    [SerializeField] private float rightPosition = -2f;

    private float forwardPosition = 5f;
    private float backwardPosition = -8f;

    public bool useRandomPosition = true;
    public bool useRandomGameObject = false;


    void Start()
    {
        pooler = ObjectPooler.instance;
    }

    private void Update()
    {
        Spawn();
    }

    void Spawn()
    {        
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
            return;
        }

        Vector3 position = useRandomPosition ?
            new Vector3(Random.Range(leftPosition, rightPosition), transform.position.y, transform.position.z + Random.Range(forwardPosition, backwardPosition)) :
            transform.position;

        GameObject selectedCube = useRandomGameObject ? cubes[Random.Range(0, cubes.Count)] : cubes[0];

       pooler.SpawnFromPool("Cube", position, Quaternion.identity);
        
        timer = 0f;
    }
}
