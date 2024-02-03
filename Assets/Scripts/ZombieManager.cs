using System.Collections;
using System.Linq;
using UnityEngine;

public class CollisionDetection : MonoBehaviour, IPooledObject
{
    public Collider mainCollider;
    public GameObject zombieRig;
    public GameObject target;
    public Animator animator;
    private GameManagerScript gameManagerScript;

    private Collider[] colliders;
    private Rigidbody[] rigidbodies;

    private ZombieState currentState = ZombieState.Walking;

    [SerializeField]
    private float hitspeed;

    [SerializeField]
    private float zombieSpeed;

    private enum ZombieState
    {
        Walking,
        Ragdoll
    }

    void Start()
    {
        GetRagdollBits();
        RagdollModeOff();
        gameManagerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();
    }

    IEnumerator TriggerZombieEffectWithDelay()
    {
        yield return new WaitForSeconds(0.95f);
    }


    void Update()
    {
        switch (currentState)
        {
            case ZombieState.Walking:
                WalkingBehaviour();
                break;
            case ZombieState.Ragdoll:
                StartCoroutine(TriggerZombieEffectWithDelay());
                break;
        }

    }



    private void OnCollisionEnter(Collision collision)

    {
        if (collision.gameObject.tag == "Zombie")
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }


        if (collision.gameObject.tag == "Car")
        {
            RagdollModeOn();

            ContactPoint contact = collision.contacts[0];
            Vector3 impactPoint = contact.point;

            float forceMagnitude = Mathf.Lerp(1, 400f, 10f);

            Vector3 forceDirection = impactPoint - transform.position;
            forceDirection.Normalize();

            Vector3 force = forceMagnitude * forceDirection;
            TriggerRagdoll(force, impactPoint);
        }

    }

    void RagdollModeOn()
    {
        currentState = ZombieState.Ragdoll;
        animator.enabled = false;

        foreach (var collider in colliders)
        {
            collider.enabled = true;
        }

        foreach (var rigidBody in rigidbodies)
        {
            rigidBody.isKinematic = false;
        }

        mainCollider.enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    void RagdollModeOff()
    {
        currentState = ZombieState.Walking;

        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }

        foreach (var rigidBody in rigidbodies)
        {
            rigidBody.isKinematic = true;
        }

        animator.enabled = true;
        animator.speed = 1.5f;
        mainCollider.enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
    }

    void GetRagdollBits()
    {
        colliders = zombieRig.GetComponentsInChildren<Collider>();
        rigidbodies = zombieRig.GetComponentsInChildren<Rigidbody>();
    }

    public void TriggerRagdoll(Vector3 force, Vector3 hitPoint)
    {
        Rigidbody hitRigidbody = rigidbodies.OrderBy(rigidbody => Vector3.Distance(rigidbody.position, hitPoint)).First();
        hitRigidbody.AddForceAtPosition(force, hitPoint, ForceMode.Impulse);
    }

    private void WalkingBehaviour()
    {
        Vector3 directionToTarget = target.transform.position - transform.position;

        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * zombieSpeed);
    }

    public void OnObjectSpawned()
    {
        if (this != null)
        {
            GetRagdollBits();
            RagdollModeOff();
        }
    }
}
