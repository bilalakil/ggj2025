using System;
using UnityEngine;

public class Fish : MonoBehaviour, IDockable
{
    private static Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
    
    public DockType dockTarget { get; protected set; } = DockType.Coral;
    public event Action OnDockedChanged;
    public IDock dockedToBacking;

    public IDock CurrentlyDockedTo
    {
        get => dockedToBacking;
        protected set
        {
            if (value == dockedToBacking) return;
            dockedToBacking = value;
            OnDockedChanged?.Invoke();
        }
    }

    public float bubbleLifetime = 3.0f;

    public Bullet bubblePrefab;

    private new Camera camera;
    private Vector3 origin;
    private Vector3 originYOnly;

    public static Action<Fish> OnSelectFish;
    public static Action<Fish> OnReleaseFish;
    
    public virtual void DockTo(IDock dock)
    {
        CurrentlyDockedTo = dock;
        transform.position = dock.Transform.position;
    }

    public virtual void Undock()
    {
        CurrentlyDockedTo = null;
    }

    private void Start()
    {
        origin = transform.position;
        originYOnly = new Vector3(0, origin.y, 0);
        camera = Camera.main;
    }

    private void OnMouseDown()
    {
        if (SessionManager.I?.IsPlaying == true) return;

        OnSelectFish?.Invoke(this);
    }

    private void OnMouseUp()
    {
        if (SessionManager.I?.IsPlaying == true) return;

        OnReleaseFish?.Invoke(this);
    }

    private void OnMouseDrag()
    {
        if (SessionManager.I?.IsPlaying == true) return;

        var mouseRay = camera.ScreenPointToRay(Input.mousePosition);
        if (!groundPlane.Raycast(mouseRay, out var distance)) return;
        var groundPosition = mouseRay.GetPoint(distance);
        transform.position = groundPosition + originYOnly;
    }

    public void ResetPositionAndUndock()
    {
        Undock();
        transform.position = origin;
    }
    
    public virtual void Shoot()
    {
        Instantiate(bubblePrefab, transform.position, transform.rotation).GetComponent<Bullet>().Initialise(bubbleLifetime);
    }
}
