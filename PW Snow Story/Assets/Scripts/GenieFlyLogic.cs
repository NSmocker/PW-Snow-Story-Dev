using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GenieFlyLogic : MonoBehaviour
{ 

    [Header("States")]
    public bool isFollow;
    public bool isInIdle;
    
    [Header("States")]
    
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
        if(current_velocity<0.1f)current_velocity = 0f;
        /**********************OLD MOVE************************/       
       /* if (current_distance > min_distance)
        {
            rb.linearVelocity = directionPointer.transform.forward * current_velocity;
            var point_coords = new Vector3(flyPoint.position.x, transform.position.y, flyPoint.position.z);
            transform.LookAt(point_coords);
        }
        */
        /**********************NEW MOVE************************/
      
      
          isFollow = current_distance > min_distance;
          isInIdle = current_distance < min_distance;   
        
        if (isFollow)
        {
            rb.MovePosition(Vector3.MoveTowards(transform.position, flyPoint.position, current_velocity * Time.fixedDeltaTime));
            var point_coords = new Vector3(flyPoint.position.x, transform.position.y, flyPoint.position.z);
            transform.LookAt(point_coords);
        }
        
        if (isInIdle)
        {
            float perlin = Mathf.PerlinNoise(Time.time * 0.1f,0);
            var floatingPoint = flyPoint.position + new Vector3(0,perlin,0);
            rb.MovePosition(Vector3.MoveTowards(transform.position, floatingPoint, 1 * Time.fixedDeltaTime));
            
        }
    }
    
    void Update()
    {
       
        
    }
}
