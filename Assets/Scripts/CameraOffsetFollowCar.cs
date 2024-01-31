using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCar : MonoBehaviour
{
    public Transform carTransform;  // Reference to the car's transform
    public Vector3 offset = new Vector3(0f, 0.6f,-3.9f);



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        if (carTransform != null)
        {
            transform.position = carTransform.position + offset;
            transform.LookAt(carTransform.position);
        }
    }
}
