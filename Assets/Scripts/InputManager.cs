using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public List<Transform> docks = new List<Transform>();
    public float dockingDistance = 1f;
    public float rotationPerMouseWheel = 10;

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

    public void Update()
    {
        CheckMouseWheelInput();
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

    private void CheckMouseWheelInput()
    {
        var input = Input.mouseScrollDelta.y;
        if (input == 0) return;
        
        var (hoverTarget, _) = GetHoverTarget<IRotateable>();
        if (hoverTarget == null) return;
        
        // TODO: Consider framerate independence
        var rotation = input * rotationPerMouseWheel;
        hoverTarget.AddRotation(rotation);
    }

    private (T component, GameObject containingObject) GetHoverTarget<T>()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out var hitInfo) 
            ? (hitInfo.transform.GetComponent<T>(), hitInfo.transform.gameObject)
            : (default, null);
    }
}
