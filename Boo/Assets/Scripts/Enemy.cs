using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Movement
{
    // Fields for Enemy
    public List<Vector3> path;
    int pathIndex;
    bool pathReversed;
    bool canShoot;

    // Method at start
    protected override void Start()
    {
        // Init fields
        pathIndex = 0;
        pathReversed = false;
        canShoot = true;
        base.Start();
    }

    // Method for Steering the Zombie
    protected override void CalculateSteeringForces()
    {
        // Start force at 0
        Vector3 ultimateForce = Vector3.zero;

        // Make the enemy move towards the first coordinate of the path
        // if a path exists
        if(path.Count > 0)
        {
            ultimateForce += Seek(path[pathIndex]);
        }
        // Clamp the magnitude then apply the final force
        ultimateForce = Vector3.ClampMagnitude(ultimateForce, 1);
        ApplyForce(ultimateForce);
    }

    // Use Base Update plus adding the new updates
    protected override void Update()
    {
        // Check if the enemy has reached one of its paths
        if(Vector3.Distance(transform.position, path[pathIndex]) <= 0.1f)
        {
            // If the path is reversed then subtract from index
            // if at end of path then reverse direction again
            if(pathReversed)
            {
                if(pathIndex - 1 >= 0)
                {
                    pathIndex--;
                }
                else
                {
                    pathReversed = false;
                    pathIndex++;
                }
            }
            // If the path isnt reversed then add to the index
            // if at end of path then reverse direction again
            else
            {
                if (pathIndex + 1 < path.Count)
                {
                    pathIndex++;
                }
                else
                {
                    pathReversed = true;
                    pathIndex--;
                }
            }
        }

        // Check if the player is in front
        if(DetectPlayer())
        {
            // If player is detected check if the enemy can shoot
            if (canShoot)
            {
                Instantiate(stake, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                canShoot = false;
                Invoke("SetCanShoot", 1.0f); // Cooldown for shooting
            }
        }

        base.Update();
    }

    // Getter method for human's direction
    public Vector3 GetDirection()
    {
        return direction;
    }

    // Getter method for human's velocity
    public Vector3 GetVelocity()
    {
        return velocity;
    }

    // Method that sets the enemy able to shoot
    public void SetCanShoot()
    {
        canShoot = true;
    }
}
