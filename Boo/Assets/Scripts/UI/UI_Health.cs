using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Health : MonoBehaviour
{

    public GameObject heartContainer;
    public GameObject GameManager;
    [SerializeField] GameObject heartOBJ;
    [SerializeField] GameObject brokenHeartOBJ;


    public void DrawHearts(int numberHearts, int maxHealth)
    {
        foreach(Transform t in this.transform)
        {
            Destroy(t.gameObject);
        }
        float spacer = 1;
        for (int i = 0; i < maxHealth; i++)
        {

            Vector3 tempTransform = new Vector3(transform.position.x * spacer, transform.position.y, transform.position.z);

            if (i +1 <= numberHearts)
            {
                 GameObject g = Instantiate(heartOBJ, tempTransform, Quaternion.identity);
                g.transform.parent = transform;
                spacer += .3f;
            }
            else
            {
                GameObject g = Instantiate(brokenHeartOBJ,tempTransform, Quaternion.identity);
                g.transform.parent = transform;
                spacer += .3f;
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
