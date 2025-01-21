    using UnityEngine;

    public class Rotateable : MonoBehaviour, IRotateable
    {
        public void AddRotation(float angle)
        {
            transform.Rotate(Vector3.up, angle);
        }
    }