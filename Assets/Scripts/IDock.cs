using UnityEngine;

public enum DockType
{
    Coral,
    Fish,
}

public interface IDock
{
    public DockType DockType { get; }
    public Transform Transform { get; }
    public bool HasVisitor { get; }
    /// <summary>
    /// Use <see cref="Dock"/>!
    /// </summary>
    public void DockVisitor(IDockable visitor);
    /// <summary>
    /// Use <see cref="Undock"/>!
    /// </summary>
    public void UndockVisitor();

    public static void Dock(IDock dock, IDockable visitor)
    {
        Undock(visitor);
        dock.DockVisitor(visitor);
        visitor.DockTo(dock);
    }

    public static void Undock(IDockable visitor)
    {
        if (visitor.CurrentlyDockedTo == null) return;
        visitor.CurrentlyDockedTo.UndockVisitor();
        visitor.Undock();
    }
}
