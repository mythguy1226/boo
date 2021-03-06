using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Fields
    public int lives = 3;
    public GameObject player;
    public static GameObject manager;
    [SerializeField] UI_Health hSystem;
    // Start is called before the first frame update
    void Start()
    {
        manager = gameObject;
        hSystem.DrawHearts(3, 3);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if lives are out
        if(lives <= 0)
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }

    // Method for respawning player
    public void Respawn()
    {
        // Subtract the amount of lives
        lives--;
        hSystem.DrawHearts(lives, 3);

        // Send the player back to the beginning of the level
        Vector3 startPos = player.GetComponent<Player_Movement>().startPos;
        player.transform.position = startPos;
    }
}
