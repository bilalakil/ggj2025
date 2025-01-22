using System;
using UnityEngine;

public class Fish : MonoBehaviour, IDockable
{
    public DockType dockTarget { get; protected set; } = DockType.Coral;
    public IDock CurrentlyDockedTo { get; protected set; }
    public bool IsCurrentlyDocked => CurrentlyDockedTo != null;

    [SerializeField] private float bubbleShootInterval = 2.0f;
    public float bubbleLifetime = 3.0f;

    private float timeSinceLastBubbleShot;
    
    public Bullet bubblePrefab;

    public Vector3 origin { get; private set; }

    [SerializeField] private float yOffset = 2f;
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

    public void Update()
    {
        TickShooter();
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
        transform.position = new Vector3(target.x, yOffset, target.z);
    }

    public void ResetPositionAndUndock()
    {
        Undock();
        transform.position = origin;
    }
    
    private void TickShooter()
    {
        if (!IsCurrentlyDocked) return;

        timeSinceLastBubbleShot += Time.deltaTime;
        if (timeSinceLastBubbleShot < bubbleShootInterval) return;
        timeSinceLastBubbleShot -= bubbleShootInterval;

        Shoot();
    }

    protected virtual void Shoot()
    {
        Debug.Log($"Basic shoot!");
        Instantiate(bubblePrefab, transform.position, transform.rotation).GetComponent<Bullet>().Initialise(bubbleLifetime);
    }
}
