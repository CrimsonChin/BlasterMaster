using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject Player;

    public GameObject Troll;

    void Start()
    {
        Instantiate(Player, new Vector3(1, 1, 50), Quaternion.identity);

        // TODO enemies should not be here
        Instantiate(Troll, new Vector3(1, 13, 10), Quaternion.identity);
    }
}
