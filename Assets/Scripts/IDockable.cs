using System;

public interface IDockable
{
    public DockType dockTarget { get; }
    public event Action OnDockedChanged;
    public IDock CurrentlyDockedTo { get; }
    /// <summary>
    /// Use <see cref="IDock.Dock"/>!
    /// </summary>
    public void DockTo(IDock dock);
    /// <summary>
    /// Use <see cref="IDock.Undock"/>!
    /// </summary>
    public void Undock();
    public bool IsCurrentlyDocked => CurrentlyDockedTo != null;
}
