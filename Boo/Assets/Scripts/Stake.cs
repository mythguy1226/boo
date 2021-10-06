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
    private float speed = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        // Lifetime of the stake is only half a second
        Destroy(gameObject, 1.0f);

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
        velocity = direction * speed;

        stakePosition += velocity;

        transform.position = stakePosition;
    }
}
