using System;
using UnityEngine;

public class Fish : MonoBehaviour, IDockable
{
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

    public Vector3 origin { get; private set; }

    private Vector3 mousePosition;
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
    }

    private Vector3 GetMousePos()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDown()
    {
        if (SessionManager.I.IsPlaying) return;

        OnSelectFish?.Invoke(this);
        mousePosition = Input.mousePosition - GetMousePos();
    }

    private void OnMouseUp()
    {
        if (SessionManager.I.IsPlaying) return;

        OnReleaseFish?.Invoke(this);
    }

    private void OnMouseDrag()
    {
        if (SessionManager.I.IsPlaying) return;

        var target = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);
        transform.position = ProjectPoint(target);
    }

    private Vector3 ProjectPoint(Vector3 point)
    {
        var dotProd = Vector3.Dot(Vector3.up, point - origin);
        var vertProj = point - (dotProd * Vector3.up);
        return vertProj;
    }

    private void OnDrawGizmos()
    {
        var mousePos = Input.mousePosition - mousePosition;
        var target = Camera.main.ScreenToWorldPoint(mousePos);
        var result = Vector3.Dot(target, Vector3.up);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(target, .5f);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(ProjectPoint(target), .5f);
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
