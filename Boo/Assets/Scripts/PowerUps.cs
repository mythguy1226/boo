using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActivePowerUp
{
    Disguise,
    Invisible,
    Invincible,
    None
}

// For future use with more levels
public enum CurrentLevel
{
    One
}

public class PowerUps : MonoBehaviour
{
    //[SerializeField]
    public List<SpriteRenderer> enemyObjects = new List<SpriteRenderer>();

    public SpriteRenderer player;
    public SpriteRenderer disguiseSpriteRenderer;
    public SpriteRenderer invisibleSpriteRenderer;
    public SpriteRenderer invincibleSpriteRenderer;
    public SpriteRenderer enemy1;
    public SpriteRenderer enemy2;

    private SpriteRenderer emptySpriteRenderer;

    private GameObject placeholderObject;

    // Leave empty
    public GameObject originalPlayer;

    public Sprite disguiseSprite;
    public Sprite playerSprite;
    public double disguisePowerUpTimer = 10.0;

    // Will be the same as player, have to make it transparent or make it so that humans cannot see vampire in code
    public Sprite invisibleSprite;
    public double invisiblePowerUpTimer = 10.0;

    // Will be the same as player, but should change colors or be rainbow colored like star powerup in mario games
    public Sprite invincibleSprite;
    public double invinciblePowerUpTimer = 10.0;

    public double Timer = 0.0;

    public ActivePowerUp activePowerUp;

    // For future use with more levels
    public CurrentLevel currentLevel;

    void Start()
    {
        // Default/ Replace value when testing each Power Up
        activePowerUp = ActivePowerUp.None;

        // Sets up placeholder gameobject that humans will seek while vampire has gone invisible/disguised
        placeholderObject = new GameObject();
        placeholderObject.transform.position = new Vector3(8,20,0);

        originalPlayer = enemy1.GetComponent<Enemy>().player;

        // Adds enemy objects into enemy list
        enemyObjects.Add(enemy1);
        enemyObjects.Add(enemy2);
    }

    // Update is called once per frame
    void Update()
    {
        //Timer += Time.deltaTime;

        // Will run while no power up is active
        if(activePowerUp == ActivePowerUp.None)
        {
            if (CheckForCollision(player, disguiseSpriteRenderer, activePowerUp))
            {
                activePowerUp = ActivePowerUp.Disguise;
            }

            if (CheckForCollision(player, invisibleSpriteRenderer, activePowerUp))
            {
                activePowerUp = ActivePowerUp.Invisible;
            }

            if (CheckForCollision(player, invincibleSpriteRenderer, activePowerUp))
            {
                activePowerUp = ActivePowerUp.Invincible;
            }
        }

        // Disguise
        #region
        // Poweup for Disguise is active
        if (activePowerUp == ActivePowerUp.Disguise)
        {
            if(CheckForCollision(player, disguiseSpriteRenderer, activePowerUp))
            {
                // Will disable the Disguise Power Up (Hiding it once the player touches it)
                disguiseSpriteRenderer.GetComponent<SpriteRenderer>().enabled = false;

                // Changes Player Sprite to disguise
                
                // Will stop the player animation (allows for the changing of sprite for disguise function)
                player.GetComponent<Animator>().enabled = false;

                // Changes sprite to that of human
                player.sprite = disguiseSprite;

                // Stops humans from going after player (Switches gameobject human is tracking)
                enemy1.GetComponent<Enemy>().player = placeholderObject;
                enemy2.GetComponent<Enemy>().player = placeholderObject;
            }

            Timer += Time.deltaTime;

            if (disguisePowerUpTimer <= Timer)
            {
                // Sets player sprite back to vampire and allows animation to play
                // Possibly have to change when merging with pushes (10/25)
                player.GetComponent<Animator>().enabled = true;

                player.sprite = playerSprite;

                enemy1.GetComponent<Enemy>().player = originalPlayer;
                enemy2.GetComponent<Enemy>().player = originalPlayer;

                // Will set power up status back to none
                activePowerUp = ActivePowerUp.None;

                // Resets timer variable
                Timer = 0.0;

                // Moves sprite out of game boundries, making it inacessible
                disguiseSpriteRenderer.transform.position = new Vector3(100, 100, 0);
            }
        }
        #endregion

        // Insivisble
        #region
        if (activePowerUp == ActivePowerUp.Invisible)
        {
            if (CheckForCollision(player, invisibleSpriteRenderer, activePowerUp))
            {
                // Will disable the Invisible Power Up (Hiding it once the player touches it)
                invisibleSpriteRenderer.GetComponent<SpriteRenderer>().enabled = false;

                // Will stop the player animation (allows for the changing of sprite for invisible function)
                player.GetComponent<Animator>().enabled = false;

                // Changes sprite to that of human
                player.sprite = invisibleSprite;

                // Stops humans from going after player (Switches gameobject human is tracking)
                enemy1.GetComponent<Enemy>().player = placeholderObject;
                enemy2.GetComponent<Enemy>().player = placeholderObject;
            }

            Timer += Time.deltaTime;

            if (invisiblePowerUpTimer <= Timer)
            {
                // Possibly have to change when merging with pushes (10/25)
                // Sets player sprite back to vampire and allows animation to play
                // Possibly have to change when merging with pushes (10/25)
                player.GetComponent<Animator>().enabled = true;

                player.sprite = playerSprite;

                // Makes it so that humans now attack the player when they see the player
                enemy1.GetComponent<Enemy>().player = originalPlayer;
                enemy2.GetComponent<Enemy>().player = originalPlayer;

                // Will set power up status back to none
                activePowerUp = ActivePowerUp.None;

                // Resets timer variable
                Timer = 0.0;

                // Moves sprite out of game boundries, making it inacessible
                invisibleSpriteRenderer.transform.position = new Vector3(100, 100, 0);
            }
        }
        #endregion

        // Invincible
        #region
        if (activePowerUp == ActivePowerUp.Invincible)
        {
            if (CheckForCollision(player, invincibleSpriteRenderer, activePowerUp))
            {
                // Will disable the StarPower Power Up (Hiding it once the player touches it)
                invincibleSpriteRenderer.GetComponent<SpriteRenderer>().enabled = false;

                // Will stop the player animation (allows for the changing of sprite for invincible function)
                player.GetComponent<Animator>().enabled = false;

                // Changes sprite to that of human
                player.sprite = invincibleSprite;

                // Will make player immune/ deactivate player taking damage (when merged with new push)
            }

            // Will make it so that by touching a human will instantly kill them instead of having to press space button
            for(int i=1; i<enemyObjects.Count; i++)
            {
                if(CheckForCollision(player, enemyObjects[i], activePowerUp))
                {
                    enemyObjects[i].color = Color.red;
                }
            }

            Timer += Time.deltaTime;

            if (invinciblePowerUpTimer <= Timer)
            {
                // Possibly have to change when merging with pushes (10/25)
                // Sets player sprite back to vampire and allows animation to play
                // Possibly have to change when merging with pushes (10/25)
                player.GetComponent<Animator>().enabled = true;

                player.sprite = playerSprite;

                // Will set power up status back to none
                activePowerUp = ActivePowerUp.None;

                // Resets timer variable
                Timer = 0.0;

                // Moves sprite out of game boundries, making it inacessible
                invincibleSpriteRenderer.transform.position = new Vector3(100, 100, 0);
            }
        }
        #endregion
    }

    bool CheckForCollision(SpriteRenderer player, SpriteRenderer obj, ActivePowerUp powerUp)
    {
        bool isHitting = false;

        switch(powerUp)
        {
            case ActivePowerUp.Disguise:

                // First - Checks for Collision
                if (obj.bounds.min.x < player.bounds.max.x &&
                    obj.bounds.max.x > player.bounds.min.x &&
                    obj.bounds.max.y > player.bounds.min.y &&
                    obj.bounds.min.y < player.bounds.max.y)
                {
                    isHitting = true;
                }

                break;

            case ActivePowerUp.Invisible:

                // First - Checks for Collision
                if (obj.bounds.min.x < player.bounds.max.x &&
                    obj.bounds.max.x > player.bounds.min.x &&
                    obj.bounds.max.y > player.bounds.min.y &&
                    obj.bounds.min.y < player.bounds.max.y)
                {
                    isHitting = true;
                }

                break;

            case ActivePowerUp.Invincible:

                // First - Checks for Collision
                if (obj.bounds.min.x < player.bounds.max.x &&
                    obj.bounds.max.x > player.bounds.min.x &&
                    obj.bounds.max.y > player.bounds.min.y &&
                    obj.bounds.min.y < player.bounds.max.y)
                {
                    isHitting = true;
                }

                break;

            case ActivePowerUp.None:

                // First - Checks for Collision
                if (obj.bounds.min.x < player.bounds.max.x &&
                    obj.bounds.max.x > player.bounds.min.x &&
                    obj.bounds.max.y > player.bounds.min.y &&
                    obj.bounds.min.y < player.bounds.max.y)
                {
                    isHitting = true;
                }

                break;
        }

        return isHitting;
    }
}
