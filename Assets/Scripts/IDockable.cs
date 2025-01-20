public enum DockTarget
{
    Coral,
    Fish,
}

public interface IDockable
{
    public DockTarget dockTarget { get; protected set; }
}
