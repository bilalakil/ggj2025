using UnityEngine;

public class DisableForWebGL : MonoBehaviour
{
#if UNITY_WEBGL
    public void Awake()
    {
        gameObject.SetActive(false);
    }
#endif
}
