using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class EnemyManager : MonoBehaviour
{
    public GameObject Troll;
    public List<GameObject> Enemies;


    void Awake()
    {
        var board = GetComponent<BoardManager>();
        
        // These would normally be player start zones so we can use these safely until enemies are randomly generated
        Instantiate(Troll, new Vector3(1, board.Height, 10), Quaternion.identity);

        Instantiate(Troll, new Vector3(board.Length, board.Height, 10), Quaternion.identity);

        Instantiate(Troll, new Vector3(board.Length, 1, 10), Quaternion.identity);

        Enemies = GameObject.FindGameObjectsWithTag("Enemy").ToList();
    }

    public void DestroyEnemyAtLocation(Vector2 location)
    {
        for (var i = Enemies.Count - 1; i >= 0; i--)
        {
            var enemy = Enemies[i];
            var enemyScript = enemy.GetComponent<Troll>();
            if (enemyScript.GetRoundedPosition() == location)
            {
                Enemies.Remove(enemy);
                enemyScript.Die();
            }
        }
    }
}
