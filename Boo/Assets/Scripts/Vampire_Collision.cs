using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollisionCheckMethod
{
    AABB
}

public class Vampire_Collision : MonoBehaviour
{
    [SerializeField]
    List<SpriteRenderer> objects = new List<SpriteRenderer>();

    public CollisionCheckMethod checkMethod;

    // Fields for checking obstacle collisions
    public List<GameObject> obstacles;
    Vector3 direction;
    float speed;

    // Update is called once per frame
    void Update()
    {
        direction = GetComponent<Player_Movement>().direction;
        speed = GetComponent<Player_Movement>().speed;
        bool isPlayerHit = false;

        // Remove If statement and else statement to have collision automatically detected among objects
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 1; i < objects.Count; i++)
            {
                if (CheckForCollision(objects[0], objects[i], checkMethod))
                {
                    objects[i].color = Color.red;

                    isPlayerHit = true;
                }

                else
                {
                    objects[i].color = Color.white;
                }
            }

            if (isPlayerHit == true)
            {
                objects[0].color = Color.red;
            }

            else
            {
                objects[0].color = Color.white;
            }
        }

        else
        {
            objects[0].color = Color.white;
        }

        // Check Collisions for Obstacles and dont let the player walk through them
        foreach (GameObject obstacle in obstacles)
        {
            // Get the bounds for each object
            Vector3 obstMax = obstacle.GetComponent<SpriteRenderer>().bounds.max;
            Vector3 obstMin = obstacle.GetComponent<SpriteRenderer>().bounds.min;
            Vector3 vampMax = gameObject.GetComponent<SpriteRenderer>().bounds.max;
            Vector3 vampMin = gameObject.GetComponent<SpriteRenderer>().bounds.min;

            // If there is collision stop the player from moving into the obstacle
            if (obstMin.x < vampMax.x &&
                obstMax.x > vampMin.x &&
                obstMax.y > vampMin.y &&
                obstMin.y < vampMax.y)
            {
                // Move the Player in the opposite direction they are trying to do
                gameObject.transform.Translate(-direction * (speed + 0.5f) * Time.deltaTime);
            }
        }
    }

    bool CheckForCollision(SpriteRenderer objA, SpriteRenderer objB, CollisionCheckMethod collisionCheck)
    {
        bool isHitting = false;

        switch(collisionCheck)
        {
            case CollisionCheckMethod.AABB:
                
                if(objB.bounds.min.x < objA.bounds.max.x &&
                    objB.bounds.max.x > objA.bounds.min.x &&
                    objB.bounds.max.y > objA.bounds.min.y &&
                    objB.bounds.min.y < objA.bounds.max.y)
                {
                    isHitting = true;
                }

                break;
        }

        return isHitting;
    }
}
