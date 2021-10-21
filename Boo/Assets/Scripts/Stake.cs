using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stake : MonoBehaviour
{
    // Fields needed for applying movement to the stake
    public GameObject player;
    private Vector3 direction;
    private Vector3 stakePosition;
    private Vector3 velocity;
    private float speed = 6.0f;
    public GameObject manager;

    // Start is called before the first frame update
    void Start()
    {
        // Lifetime of the stake is only two seconds
        Destroy(gameObject, 2.0f);

        // Apply initial stake velocity, position, and direction to be the same as the human's
        direction = player.gameObject.transform.position - transform.position;
        direction.Normalize();
        velocity = Vector3.zero;
        stakePosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculations for applying velocity to the stake
        velocity = direction * speed * Time.deltaTime;

        stakePosition += velocity;

        transform.position = stakePosition;
    }

    // Handle Stake Collisions
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Handle Player Collisions
        if(collision.collider.name.Contains("Player"))
        {
            // Destroy the Stake and Respawn the Player
            Destroy(gameObject);
            manager.GetComponent<GameManager>().Respawn();
        }

        // Handle Obstacle Collisions
        if (collision.collider.name.Contains("Obstacle"))
        {
            // Destroy the Stake
            Destroy(gameObject);
        }
    }
}
