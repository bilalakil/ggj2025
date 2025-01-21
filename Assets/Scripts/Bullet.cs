using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float lifetime = 3.0f;

    public void Start()
    {
        Destroy(this.gameObject, lifetime);
    }
    
    public void Update()
    {
        transform.Translate(transform.forward * speed*Time.deltaTime);
    }
}
