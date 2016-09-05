using UnityEngine;
using System.Collections;

public class GenerateLevel : MonoBehaviour
{

    public int Length = 21;

    public int Height = 21;

    public GameObject Block;

    public GameObject Grass;

    public GameObject Crate;

    public GameObject A;

    private GameObject[,] _grid;

    // Use this for initialization
    void Start()
    {
        _grid = new GameObject[Length, Height];
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Length; x++)
            {
                var isEdge = x == 0 || y == 0 || x == Length - 1 || y == Length - 1;
                var isColumn = x % 2 == 0 && y % 2 == 0;

                var border = 3;
                var isStartZoneA = x < border && y < border;
                var isStartZoneB = x < border && y >= Height - border;
                var isStartZoneC = x >= Length - border && y < border;
                var isStartZoneD = x >= Length - border && y >= Height - border;

                var prefab = Grass;
                if (isEdge || isColumn)
                {
                    prefab = Block;
                }
                else if (isStartZoneA || isStartZoneB || isStartZoneC || isStartZoneD)
                {
                    prefab = Grass;
                }
                else if (Random.Range(0, 100) % 3 == 0)
                {
                    prefab = Crate;
                }

                var tile = (GameObject)Instantiate(prefab, new Vector3(x, y, 50), Quaternion.identity);
                _grid[x, y] = tile;

            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject GetTile(float x, float y)
    {
        return _grid[Mathf.RoundToInt(x), Mathf.RoundToInt(y)];
    }
}
