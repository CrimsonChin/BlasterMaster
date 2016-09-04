using UnityEngine;

public class DropBomb : MonoBehaviour
{
    public GameObject Bomb;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(Bomb, new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), 25f), Quaternion.identity);
        }
    }
}
