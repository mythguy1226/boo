using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Health : MonoBehaviour
{
    // UI Fields
    public GameObject heartContainer;
    public GameObject GameManager;
    [SerializeField] GameObject heartOBJ;
    [SerializeField] GameObject brokenHeartOBJ;

    // Method that draws the hearts
    public void DrawHearts(int numberHearts, int maxHealth)
    {
        // Destroy all objects to reset the UI state
        foreach(Transform t in this.transform)
        {
            Destroy(t.gameObject);
        }

        // Initial X-Spacer
        float spacer = 0.5f;

        // Iterate through the amount of hearts
        for (int i = 0; i < maxHealth; i++)
        {
            // Typical transform for all heart elements before being spaced
            Vector3 tempTransform = new Vector3(transform.position.x * spacer, transform.position.y - 30, transform.position.z);

            // Set Heart Objects
            if (i +1 <= numberHearts)
            {
                GameObject g = Instantiate(heartOBJ, tempTransform, Quaternion.identity);
                g.transform.parent = transform;
                spacer += 0.5f; // Add space between elements
            }
            // Set Broken Heart Objects
            else
            {
                GameObject g = Instantiate(brokenHeartOBJ,tempTransform, Quaternion.identity);
                g.transform.parent = transform;
                spacer += 0.5f; // Add space between elements
            }
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
