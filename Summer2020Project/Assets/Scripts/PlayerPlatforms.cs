using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPlatforms : MonoBehaviour
{
    public bool inPlatform = false;
    public bool delayedCollision = false;
    public GameObject player;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        transform.position = player.transform.position;

        if (Input.GetKeyUp(KeyCode.DownArrow) && inPlatform == false)
        {
            Physics2D.IgnoreLayerCollision(8, 9, false);
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow) && inPlatform == true)
        {
            delayedCollision = true;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Physics2D.IgnoreLayerCollision(8, 9, true);
        }
        
    }



    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Platform")
        {
            inPlatform = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
    
        if (other.gameObject.tag == "Platform")
        {
            inPlatform = false;

            //Grace period if OnKeyUp is triggered while in a platform
            if (delayedCollision == true)
            {
                Physics2D.IgnoreLayerCollision(8, 9, false);
                delayedCollision = false;
            }
         }       
    }

}
