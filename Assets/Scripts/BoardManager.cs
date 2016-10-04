using UnityEngine;

public class BoardManager : MonoBehaviour
{

    public int Length = 21;

    public int Height = 21;

    public GameObject Block;

    public GameObject Grass;

    public GameObject Crate;

    private GameObject[,] _grid;

    public GameObject Camera;

    void Start()
    {
        var parentGrid = new GameObject("Grid");
        _grid = new GameObject[Length, Height];
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Length; x++)
            {
                var isEdge = x == 0 || y == 0 || x == Length - 1 || y == Height - 1;
                var isColumn = x % 2 == 0 && y % 2 == 0;

                const int border = 3;
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
                else if (Random.Range(0, 100) % 7 == 0)
                {
                    prefab = Crate;
                }

                AddTile(prefab, x, y, parentGrid.transform);
            }
        }

        Camera.transform.position = new Vector3(Length / 2, Height / 2);
    }

    public void AddTile(GameObject prefab, float x, float y, Transform parent = null)
    {
        var tile = (GameObject)Instantiate(prefab, new Vector3(x, y, 100), Quaternion.identity, parent);
        _grid[Mathf.RoundToInt(x), Mathf.RoundToInt(y)] = tile;
    }

    public GameObject GetTileGameObject(float x, float y)
    { 
        if (x > Length - 1 || x < 0 || y > Height - 1 || y < 0)
        {
            return null;
        }

        return _grid[Mathf.RoundToInt(x), Mathf.RoundToInt(y)];
    }

    public Tile GetTile(float x, float y)
    {
        var gameObject = GetTileGameObject(x, y);
        if (gameObject == null)
        {
            return null;
        }

        return gameObject.GetComponent<Tile>();
    }

    public Tile GetTile(Vector2 position)
    {
        return GetTile(position.x, position.y);
    }
}
