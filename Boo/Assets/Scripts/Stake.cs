using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stake : MonoBehaviour
{
    // Fields needed for applying movement to the bullet
    public GameObject player;
    private Vector3 direction;
    private Vector3 stakePosition;
    private Vector3 velocity;
    private Vector3 acceleration = Vector3.zero;
    private float accelerationRate = 0.002f;

    // Start is called before the first frame update
    void Start()
    {
        // Lifetime of the stake is only half a second
        Destroy(gameObject, 0.5f);

        // Apply initial stake velocity, position, and direction to be the same as the human's
        direction = player.gameObject.transform.position - transform.position;
        direction.Normalize();
        velocity = Vector3.zero;
        stakePosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculations for applying acceleration to the stake
        acceleration = direction * accelerationRate;
        velocity += acceleration;

        stakePosition += velocity;

        transform.position = stakePosition;
    }
}
