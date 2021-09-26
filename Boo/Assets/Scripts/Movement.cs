using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    // Physics fields
    protected Vector3 position;
    protected Vector3 direction;
    protected Vector3 velocity;

    [Min(0.0001f)]
    public float mass = 1;
    public float maxSpeed = 10;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        position = transform.position;
        direction = Vector3.right;
        velocity = Vector3.zero;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        CalculateSteeringForces();

        position += velocity * Time.deltaTime;

        transform.position = position;

        // Grab our current direction from velocity
        direction = velocity.normalized;
    }

    // Method for applying forces on an object
    public void ApplyForce(Vector3 force)
    {
        velocity += force / mass;
    }

    // Method for getting the steering force for seeking
    public Vector3 Seek(Vector3 targetPos)
    {
        // Calculate Desired Velocity
        Vector3 desiredVelocity = targetPos - position;

        // Set Desired equal to the max speed
        desiredVelocity = desiredVelocity.normalized * maxSpeed;

        // Calculate the seek steering force
        Vector3 seekingForce = desiredVelocity - velocity;

        // Return the seek steering force
        return seekingForce;
    }

    // Second overload method for seeking where you 
    // take an object as a parameter
    public Vector3 Seek(GameObject target)
    {
        // Return the seek steering force
        return Seek(target.transform.position);
    }

    // Abstract Method for Calculating the Steering Forces
    protected abstract void CalculateSteeringForces();
}
