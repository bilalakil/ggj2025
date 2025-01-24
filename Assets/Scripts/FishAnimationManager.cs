using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FishAnimationManager : MonoBehaviour
{
    private static readonly int IsDockedProperty = Animator.StringToHash("IsDocked");
    
    private Animator animator;
    private Fish fish;

    public void Awake()
    {
        animator = GetComponent<Animator>();
        fish = GetComponentInParent<Fish>();
    }

    public void OnEnable()
    {
        fish.OnDockedChanged += HandleDockedChanged;
        HandleDockedChanged();
    }

    public void OnDisable()
    {
        if (fish != null) fish.OnDockedChanged -= HandleDockedChanged;
    }

    // ReSharper disable once UnusedMember.Global - used by animation events
    public void TriggerShoot()
    {
        fish.Shoot();
    }

    private void HandleDockedChanged()
    {
        animator.SetBool(IsDockedProperty, ((IDockable)fish).IsCurrentlyDocked);
    }
}
