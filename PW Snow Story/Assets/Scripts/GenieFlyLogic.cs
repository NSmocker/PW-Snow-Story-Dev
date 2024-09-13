using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenieFlyLogic : MonoBehaviour
{ 
    public Transform flyPoint;
    public DirectionPointer directionPointer;

    public AnimationCurve accelerationCurve;

    public float maxVelocity = 10f;
    public float current_velocity;

    public float current_distance;
    
    public float min_distance = 1f;
    public float max_distance = 10f;
    

 
    private Rigidbody rb;
    public Vector3 targetPosition;
    public float rotationSpeed = 10f;
 
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        directionPointer.point= flyPoint;
    }
    void FixedUpdate()
    {
        current_distance = Vector3.Distance(transform.position, flyPoint.position);
        
        current_velocity = accelerationCurve.Evaluate(current_distance / max_distance) * maxVelocity;
        if(current_velocity<1)current_velocity = 0f;
        if (current_distance > min_distance)
        {
            rb.linearVelocity = directionPointer.transform.forward * current_velocity;
            var point_coords = new Vector3(flyPoint.position.x, transform.position.y, flyPoint.position.z);
            transform.LookAt(point_coords);
        }
    }
    
    void Update()
    {
       
        
    }
}
