using Assets.Scripts;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public TileType TileType;
    public bool Destroyable;
    public GameObject DestroyedTile;

    private BoardManager _boardManager;
    private bool _isApplicationQuitting;

    void Start()
    {
        _boardManager = FindObjectOfType<BoardManager>();
    }

    void OnApplicationQuit()
    {
        _isApplicationQuitting = true;
    }

    void OnDestroy()
    {
        if (_isApplicationQuitting || !Destroyable)
            return;

        _boardManager.AddTile(DestroyedTile, transform.position.x, transform.position.y);
    }
}
