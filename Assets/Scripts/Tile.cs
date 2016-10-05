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

    public void AttemptDestroy()
    {
        if (_isApplicationQuitting || !Destroyable)
            return;

        Destroy(gameObject);
    }

    void OnDestroy()
    {
        if (_isApplicationQuitting || !Destroyable)
            return;

        _boardManager.AddTile(DestroyedTile, Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
    }
}
