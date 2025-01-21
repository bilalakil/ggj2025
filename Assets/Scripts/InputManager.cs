using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public float dockingDistance = 1f;
    public float rotationPerMouseWheel = 10;
    
    private readonly List<Dock> docks = new();

    void Start()
    {
        var dockObjects = FindObjectsByType<Dock>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (var dockObject in dockObjects)
        {
            docks.Add(dockObject);
        }
        Fish.OnSelectFish += OnStartMovingFish;
        Fish.OnReleaseFish += OnReleaseFish;
    }

    private void OnDestroy()
    {
        Fish.OnSelectFish -= OnStartMovingFish;
        Fish.OnReleaseFish -= OnReleaseFish;
    }

    public void Update()
    {
        CheckMouseWheelInput();
    }

    private void OnStartMovingFish(Fish fish)
    {
        IDock.Undock(fish);
        // TODO: Do some fancy shader target indicator
    }

    private void OnReleaseFish(Fish fish)
    {
        bool isDocked = false;
        foreach (var dock in docks)
        {
            if (
                dock.HasVisitor ||
                Vector3.Distance(fish.transform.position, dock.transform.position) > dockingDistance
            ) continue;
            
            IDock.Dock(dock, fish);
            isDocked = true;                    
            break;
        }
        
        if (!isDocked)
        {
            fish.ResetPositionAndUndock();
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
