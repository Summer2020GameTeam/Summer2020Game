using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomLoadingMaster : MonoBehaviour
{
    public static RoomLoadingMaster Master;

    private EventList<string> SceneLoadRequests = new EventList<string>();
    private EventList<string> SceneUnloadRequests = new EventList<string>();
    private List<string> LoadedScenes = new List<string>();

    public class EventList<T> : List<T>
    {
        public event ListChangedEventDelegate ListChanged;
        public delegate void ListChangedEventDelegate();

        public new void Add(T item)
        {
            base.Add(item);
            if (ListChanged != null
                && ListChanged.GetInvocationList().Any())
            {
                ListChanged();
            }
        }
    }

    public void RequestSceneLoad(string scene)
    {
        SceneLoadRequests.Add(scene);
        SceneUnloadRequests.Remove(scene);
    }

    public void RequestSceneUnload(string scene)
    {
        SceneLoadRequests.Remove(scene);
        SceneUnloadRequests.Add(scene);
    }

    private void Awake()
    {
        if(Master == null)
        {
            Master = this;
        }

        CheckForLoadedScenes();

        SceneLoadRequests.ListChanged += HandleLoadRequests;
        SceneUnloadRequests.ListChanged += HandleUnloadRequests;
    }

    private void CheckForLoadedScenes()
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            LoadedScenes.Add(SceneManager.GetSceneAt(i).name);
        };
    }

    private void HandleLoadRequests()
    {
        List<string> ScenesToLoad = new List<string>();

        foreach (string request in SceneLoadRequests)
        {
            if (!LoadedScenes.Contains(request))
            {
                Debug.Log("Load " + request);
                ScenesToLoad.Add(request);
            }
        }

        foreach (string request in ScenesToLoad)
        {
            SceneManager.LoadSceneAsync(request, LoadSceneMode.Additive);
            LoadedScenes.Add(request);
        }
    }

    private void HandleUnloadRequests()
    {
        List<string> ScenesToUnload = new List<string>();

        foreach (string request in SceneUnloadRequests)
        {
            if (!SceneLoadRequests.Contains(request))
            {
                Debug.Log("Unload " + request);
                ScenesToUnload.Add(request);
            }
        }

        foreach (string request in ScenesToUnload)
        {
            SceneManager.UnloadSceneAsync(request);
            LoadedScenes.Remove(request);
        }
    }

}
