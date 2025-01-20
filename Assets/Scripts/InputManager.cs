using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public List<Transform> docks = new List<Transform>();
    public float dockingDistance = 1f;

    void Start()
    {
        var dockObjects = FindObjectsByType<Dock>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (var dockObject in dockObjects)
        {
            docks.Add(dockObject.transform);
        }
        Fish.OnSelectFish += OnStartMovingFish;
        Fish.OnReleaseFish += OnReleaseFish;
    }

    private void OnDestroy()
    {
        Fish.OnSelectFish -= OnStartMovingFish;
    }

    private void OnStartMovingFish(DockType dockTarget)
    {
        Debug.Log($"Fish selected with dockTarget {dockTarget}");
        // TODO: Do some fancy shader target indicator
    }

    private void OnReleaseFish(Fish fish)
    {
        bool isDocked = false;
        foreach (var dock in docks)
        {
            if (Vector3.Distance(fish.transform.position, dock.position) < dockingDistance)
            {
                fish.SetPosition(dock.position);
                isDocked = true;                    
                break;
            }
        }
        if (!isDocked)
        {
            fish.SetPosition(fish.origin);
        }
    }
}
