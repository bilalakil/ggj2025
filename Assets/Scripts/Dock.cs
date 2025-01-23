using UnityEngine;

public class Dock : MonoBehaviour, IDock
{
    public DockType DockType { get; protected set; }
    public Transform Transform => transform;
    public bool HasVisitor { get; protected set; }

    public Transform fishTransform;
    
    public virtual void DockVisitor(IDockable visitor)
    {
        AudioManager.I.Play(AudioManager.I.Refs.FishDockOn, transform.position);
        HasVisitor = true;
    }

    public virtual void UndockVisitor()
    {
        AudioManager.I.Play(AudioManager.I.Refs.FishDockOff, transform.position);
        HasVisitor = false;
    }
}
