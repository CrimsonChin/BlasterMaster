using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject Troll;

    void Awake()
    {
        var board = GetComponent<BoardManager>();
        
        // These would normally be player start zones so we can use these safely until enemies are randomly generated
        Instantiate(Troll, new Vector3(1, board.Height, 10), Quaternion.identity);

        Instantiate(Troll, new Vector3(board.Length, board.Height, 10), Quaternion.identity);

        Instantiate(Troll, new Vector3(board.Length, 1, 10), Quaternion.identity);
    }
}
