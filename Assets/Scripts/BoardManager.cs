using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public int Length = 5;

    public int Height = 5;

    public GameObject Block;

    public GameObject Grass;

    public GameObject Crate;

    private GameObject[,] _board;

    public GameObject Camera;

    void Start()
    {
        var parentGrid = new GameObject("Board");
        _board = new GameObject[Length + 2, Height + 2];
        for (var y = 0; y < Height + 2; y++)
        {
            for (var x = 0; x < Length + 2; x++)
            {
                var isEdge = x == 0 || y == 0 || x == Length + 1 || y == Height + 1;
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

        Camera.transform.position = new Vector3(Length / 2f, Height / 2f);
    }

    public void AddTile(GameObject prefab, int x, int y, Transform parent = null)
    {
        var tile = Instantiate(prefab, new Vector3(x, y, 100), Quaternion.identity, parent);
        _board[x, y] = tile;
    }
}
