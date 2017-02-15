using UnityEngine;

public class AutoDestruct : MonoBehaviour
{
    public void Start()
    {
        Destroy(gameObject, 0.1f);
    }
}
