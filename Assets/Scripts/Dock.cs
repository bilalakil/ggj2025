using UnityEngine;

public class Dock : MonoBehaviour, IDock
{
    public DockType DockType { get; protected set; }
    public Transform Transform => transform;
    public bool HasVisitor { get; protected set; }

    public Transform fishTransform;
    
    public virtual void DockVisitor(IDockable visitor)
    {
        HasVisitor = true;
    }

    public virtual void UndockVisitor()
    {
        HasVisitor = false;
    }
}
