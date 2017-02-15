using UnityEngine;

public class Lootable : MonoBehaviour
{
    public GameObject[] PowerUps;

    public void GenerateLoot()
    {
        int randomIndex = Random.Range(0, PowerUps.Length);

        var position = new Vector3(transform.position.x, transform.position.y, 30);
        var loot = Instantiate(PowerUps[randomIndex], position, Quaternion.identity);
        Destroy(loot, 3f);
    }
}
