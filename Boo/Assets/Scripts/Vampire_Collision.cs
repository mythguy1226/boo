using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollisionCheckMethod
{
    AABB
}

public class Vampire_Collision : MonoBehaviour
{
    public CollisionCheckMethod checkMethod;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Animation for attacks
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("is_attack", true);
        }
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
