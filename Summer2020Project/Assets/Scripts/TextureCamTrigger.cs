using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureCamTrigger : MonoBehaviour
{
    [SerializeField]
    private Camera textureCam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            textureCam.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            textureCam.enabled = false;
        }
    }
}
