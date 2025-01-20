using System;
using UnityEngine;

public class Fish : MonoBehaviour, IDockable
{
    public DockType dockTarget { get; protected set; }
    public Vector3 origin { get; private set; }

    [SerializeField] private float yOffset = 2f;
    private Vector3 mousePosition;
    public static Action<DockType> OnSelectFish;
    public static Action<Fish> OnReleaseFish;

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
        OnSelectFish?.Invoke(dockTarget);
        mousePosition = Input.mousePosition - GetMousePos();
    }

    private void OnMouseUp()
    {
        OnReleaseFish?.Invoke(this);
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    private void OnMouseDrag()
    {
        var target = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);
        transform.position = new Vector3(target.x, yOffset, target.z);
    }
}
