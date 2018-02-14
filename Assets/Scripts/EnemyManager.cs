using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int RemainingEnemyCount()
    {
        var trolls = GetComponents<Troll>().ToList();
        return trolls.Count;
    }
}
