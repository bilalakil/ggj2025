public enum DockType
{
    Coral,
    Fish,
}

public interface IDock
{
    public DockType DockType { get; }
}
