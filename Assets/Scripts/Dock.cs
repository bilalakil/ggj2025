using UnityEngine;

public class Dock : MonoBehaviour, IDock
{
    public DockType DockType { get; protected set; }
    public Transform fishTransform;



}
