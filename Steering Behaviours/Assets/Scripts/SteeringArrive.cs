﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringArrive : MonoBehaviour 
{
    private Move move;
    private SteeringQueue queue;

    public float stop_area_radius = 0.2f;
    public float slow_area_radius = 2.0f;
    public float time_to_target = 0.1f;

	// Use this for initialization
	void Start()
    {
        move = GetComponent<Move>();
        queue = GetComponent<SteeringQueue>();
	}
	
	// Update is called once per frame
	void Update()
    {
        if (!queue.is_in_queue)
        {
            Vector3 distance = move.target.transform.position - transform.position;
            float slow_factor = distance.magnitude / slow_area_radius;

            // Finding desired velocity
            Vector3 desired_velocity = distance.normalized * move.max_velocity;

            // Finding desired deceleration
            // To decelerate we only use the distance.
            if (distance.magnitude <= slow_area_radius)
                desired_velocity *= slow_factor;

            // Finding desired acceleration
            // To accelerate we divide by the time we want the object to be accelerated. 
            Vector3 desired_accel = (desired_velocity - move.velocity);

            if (distance.magnitude >= slow_area_radius)
                desired_accel /= time_to_target;

            // Cap desired acceleration
            if (desired_accel.magnitude >= move.max_acceleration)
                desired_accel = desired_accel.normalized * move.max_acceleration;

            // Add steering force
            Vector3 steering_force = desired_accel;
            move.AddVelocity(steering_force);
        }     
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(move.target.transform.position, slow_area_radius);
        Gizmos.DrawWireSphere(move.target.transform.position, stop_area_radius);
    }
}