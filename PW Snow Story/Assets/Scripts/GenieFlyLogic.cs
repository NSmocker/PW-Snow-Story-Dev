using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenieFlyLogic : MonoBehaviour
{ 
    public Transform flyPoint;
    public float flightSpeed = 10f;

    
    public float radius = 1f;

 
    private Rigidbody rb;
    public Vector3 targetPosition;
    public float rotationSpeed = 10f;

    public void MakeJerk()
    {
       
       Vector3 direction = flyPoint.position - transform.position;
       float distance = direction.magnitude;
       targetPosition = transform.position + direction;
        if (distance > radius)
        {
            Vector3 targetVelocity = direction.normalized * flightSpeed*direction.magnitude;
            rb.linearVelocity = targetVelocity;
        }
        else
        {
            // Character has reached the target point
            Debug.Log("Character has arrived at the target point.");
        }

    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        
       
        MakeJerk();
    }
    void Update()
    {
         
        
        Vector3 direction = targetPosition - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}
