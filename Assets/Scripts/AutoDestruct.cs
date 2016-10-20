using UnityEngine;

public class AutoDestruct : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 0.1f);
    }
}
