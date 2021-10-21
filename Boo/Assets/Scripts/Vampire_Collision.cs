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

    // Update is called once per frame
    void Update()
    {
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

    // Handle Vampire Collisions
    private void OnCollisionStay2D(Collision2D collision)
    {
        // Handle Enemy Collisions
        if (collision.collider.name.Contains("Enemy"))
        {
            // Get Attack Input
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Destroy the Enemy
                Destroy(collision.gameObject);
            }
        }
    }
}
