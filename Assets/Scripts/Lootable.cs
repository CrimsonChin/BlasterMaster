using UnityEngine;

public class Lootable : MonoBehaviour
{
    public GameObject[] PowerUps;

    private bool _isShuttingDown;

    void OnDestroy()
    {
        if (_isShuttingDown) //  || Application.isLoadingLevel)
        {
            return;
        }

        bool isLootable = Mathf.Floor(Random.value) == 0;
        if (isLootable)
        {
            GenerateLoot();
        }
    }

    void OnApplicationQuit()
    {
        _isShuttingDown = true;
    }

    private void GenerateLoot()
    {
        int randomIndex = Random.Range(0, PowerUps.Length);

        var position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 10);
        var loot = Instantiate(PowerUps[randomIndex], position, Quaternion.identity);
        Destroy(loot, 3f);
    }
}
