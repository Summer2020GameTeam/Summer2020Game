using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomLoadingManagerComponent : MonoBehaviour
{
    [SerializeField]
    private List<string> ConnectedScenes;
    private bool hasLoaded;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasLoaded && collision.gameObject.tag == "Player")
        {
            foreach (string scene in ConnectedScenes)
            {
                RoomLoadingMaster.Master.RequestSceneLoad(scene);
            }
            hasLoaded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (hasLoaded && collision.gameObject.tag == "Player")
        {
            foreach (string scene in ConnectedScenes)
            {
                RoomLoadingMaster.Master.RequestSceneUnload(scene);
            }
            hasLoaded = false;
        }

    }
}
