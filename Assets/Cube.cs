using UnityEngine;

public class Cube : MonoBehaviour, IPooledObject
{
    public float upforce = 1f;
    public float sideforce = 1f;

    public void OnObjectSpawned()
    {
        float xForce = Random.Range(-sideforce, sideforce);
        float yForce = Random.Range(upforce / 2f, upforce);
        float zForce = Random.Range(-sideforce, sideforce);

        Vector3 force = new Vector3(xForce, yForce, zForce);
        GetComponent<Rigidbody>().velocity = force;
    }
}
