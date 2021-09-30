using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    // Physics fields
    protected Vector3 position;
    protected Vector3 direction;
    protected Vector3 velocity;
    public GameObject player;
    public GameObject stake;

    [Min(0.0001f)]
    public float mass = 1;
    public float maxSpeed = 10;

    // Field for collision detector
    protected CollisionDetection detector;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        position = transform.position;
        direction = Vector3.right;
        velocity = Vector3.zero;

        detector = GetComponent<CollisionDetection>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        CalculateSteeringForces();

        // Position will change depending on velocity
        position += velocity * Time.deltaTime;

        // Change the Sprite's Position
        transform.position = position;

        // Grab our current direction from velocity
        direction = velocity.normalized;

        // Make the Sprite look at the direction
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
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

    // Method used to avoid obstacles
    public bool DetectPlayer()
    {

        // Check if the player is in front of the enemy
        Vector3 plrPos = player.transform.position;
        Vector3 eToP = plrPos - transform.position;
        eToP.Normalize();
        float dotProd = Vector3.Dot(eToP, direction);

        // If the player is in front of the enemy then attack
        if (dotProd > 0)
        {
            // Calculate the FOV
            float angle = Vector3.Angle(direction, eToP);
            if (angle < 30 || Vector3.Distance(plrPos, transform.position) < 5)
            {
                return true;
            }
        }
        return false;
    }


    // Abstract Method for Calculating the Steering Forces
    protected abstract void CalculateSteeringForces();
}
