using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool Destroyable;
    public GameObject DestroyedTile;

    private GenerateLevel _generateLevel;
    private bool _isApplicationQuitting;

    void Start()
    {
        _generateLevel = FindObjectOfType<GenerateLevel>();
    }

    void OnApplicationQuit()
    {
        _isApplicationQuitting = true;
    }

    void OnDestroy()
    {
        if (_isApplicationQuitting || !Destroyable)
            return;

        _generateLevel.AddTile(DestroyedTile, transform.position.x, transform.position.y);
    }
}
