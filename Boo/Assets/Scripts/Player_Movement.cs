using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    // Fields
    public float speed = 3.0f;
    public Vector3 direction = Vector3.zero;
    public Vector3 startPos;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        #region

        // Moves Up
        if(Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            direction = Vector3.up;
            animator.SetBool("is_up", true);
        }
        else animator.SetBool("is_up", false);

        // Moves Down
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(-Vector3.up * speed * Time.deltaTime);
            direction = -Vector3.up;
            animator.SetBool("is_down", true);
        }
        else animator.SetBool("is_down", false);

        // Moves Right
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            direction = Vector3.right;
            animator.SetBool("is_right", true);
        }
        else animator.SetBool("is_right", false);

        // Moves Left
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-Vector3.right * speed * Time.deltaTime);
            direction = -Vector3.right;
            animator.SetBool("is_left", true);
        }
        else animator.SetBool("is_left", false);

        // Get the player sprite bounds
        Vector3 vampMax = gameObject.GetComponent<SpriteRenderer>().bounds.max;
        Vector3 vampMin = gameObject.GetComponent<SpriteRenderer>().bounds.min;
        Vector3 vampExtents = gameObject.GetComponent<SpriteRenderer>().bounds.extents;

        // Get the cam bounds
        Vector3 camBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        float camWidth = camBounds.x;
        float camHeight = camBounds.y;

        // Keep the player from going out of the bounds of the screen
        if (vampMax.x > camWidth) // Right
        {
            transform.position = new Vector3(camWidth - vampExtents.x, transform.position.y, transform.position.z);
        }
        if(vampMax.y > camHeight) // Top
        {
            transform.position = new Vector3(transform.position.x, camHeight - vampExtents.y, transform.position.z);
        }
        if (vampMin.x < -camWidth) // Left
        {
            transform.position = new Vector3(-camWidth + vampExtents.x, transform.position.y, transform.position.z);
        }
        if (vampMin.y < -camHeight) // Bottom
        {
            transform.position = new Vector3(transform.position.x, -camHeight + vampExtents.y, transform.position.z);
        }


        #endregion
    }
}
