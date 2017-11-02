﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    public GameObject target;
    public float max_velocity;

    private Vector3 velocity = Vector3.zero;
    private float rotation = 0.0f;

    public void AddSteeringForce(Vector3 force)
    {
        velocity += force;
    }

    public Vector3 GetVelocity()
    {
        return velocity;
    }

    public void SetVelocity(Vector3 new_velocity)
    {
        velocity = new_velocity;
    }

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Cap velocity
		if (velocity.magnitude > max_velocity)
        {
            velocity = velocity.normalized * max_velocity;
        }

        // Move
        transform.position += velocity * Time.deltaTime;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.forward);
    }
}