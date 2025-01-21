public interface IDockable
{
    public DockType dockTarget { get; }
    public IDock CurrentlyDockedTo { get; }
    /// <summary>
    /// Use <see cref="IDock.Dock"/>!
    /// </summary>
    public void DockTo(IDock dock);
    /// <summary>
    /// Use <see cref="IDock.Undock"/>!
    /// </summary>
    public void Undock();
}
