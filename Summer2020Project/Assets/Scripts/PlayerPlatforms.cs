using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPlatforms : MonoBehaviour
{
    public bool inPlatform = false;
    public bool delayedCollision = false;
    public GameObject player;
    public GameObject uiding;
    

    // Start is called before the first frame update
    void Start()
    {
       
        Debug.Log("inplatform= " + inPlatform);
        Debug.Log("Delayedcol= " + delayedCollision);
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = player.transform.position;

        if (Input.GetKeyUp(KeyCode.DownArrow) && inPlatform == false)
        {
            uiding.GetComponent<Image>().color = Color.white;
            Physics2D.IgnoreLayerCollision(8, 9, false);
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow) && inPlatform == true)
        {
            uiding.GetComponent<Image>().color = Color.white;
            delayedCollision = true;
            Debug.Log("Delayedcol= " + delayedCollision);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Physics2D.IgnoreLayerCollision(8, 9, true);
            uiding.GetComponent<Image>().color = Color.green;
        }
        
    }



    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Platform")
        {
            inPlatform = true;
            Debug.Log("inplatform= " + inPlatform);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
 
        if (other.gameObject.tag == "Platform")
        {
            inPlatform = false;
            Debug.Log("inplatform= " + inPlatform);
            if (delayedCollision == true)
            {
                Physics2D.IgnoreLayerCollision(8, 9, false);
                delayedCollision = false;
                Debug.Log("Delayedcol= " + delayedCollision);
            }
         }       
    }


  
   



    //old code, works but not the 'right way'
    /* Collider2D otherCollider;

     private void OnTriggerEnter2D(Collider2D other)
     {

         if (other.gameObject.tag == "Platform")
         {
             if (transform.position.y > other.transform.position.y + 0.2)
             {
                 Debug.Log("Hit van boven");
                 otherCollider.isTrigger = false;
             }
             else
             {
                 Debug.Log("hit niet van boven");
                 otherCollider = other.gameObject.GetComponent<Collider2D>();
                 otherCollider.isTrigger = true;
             }
         }
     }
     */



}
