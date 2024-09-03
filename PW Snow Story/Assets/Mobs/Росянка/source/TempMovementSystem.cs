using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempMovementSystem : MonoBehaviour
{
    public List<Transform> wey_points;
    public CharacterController hitbox_link;
    public float move_speed;

    public float min_distance_to_come = 0.5f;
    public float current_distance_to_come;

    public int current_way_point_index;

    public float custom_gravity = 9.81f;

    private bool isWaiting = false;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        // ...
    }

    void Update()
    {
        if (!isWaiting)
        {
            current_distance_to_come = Vector3.Distance(hitbox_link.transform.position, wey_points[current_way_point_index].position);

            if (current_distance_to_come > min_distance_to_come)
            {
                // Move towards the waypoint
                Vector3 direction_to_waypoint = (wey_points[current_way_point_index].position - hitbox_link.transform.position).normalized;
                hitbox_link.transform.rotation = Quaternion.LookRotation(direction_to_waypoint, Vector3.up);
                velocity = direction_to_waypoint * move_speed;
                hitbox_link.Move(velocity * Time.deltaTime);
            }
            else
            {
                // Arrived at the waypoint, wait for 3 seconds
                isWaiting = true;
                Invoke("ResumeMovement", 3f);
                velocity = Vector3.zero; // Stop movement
            }
        }

        // Apply gravity
        if (!isWaiting)
        {
            velocity.y -= custom_gravity * Time.deltaTime;
            hitbox_link.Move(velocity * Time.deltaTime);
        }
    }

    void ResumeMovement()
    {
        isWaiting = false;
        current_way_point_index = (current_way_point_index + 1) % wey_points.Count;
    }
}