using UnityEngine;

namespace Assets.Scripts
{
    class Bomb : MonoBehaviour
    {
        private BoardManager _boardManager;
        public int Power = 1;

        public GameObject Middle;
        public GameObject Top;
        public GameObject Bottom;
        public GameObject Left;
        public GameObject Right;
        public GameObject Horizontal;
        public GameObject Vertical;
        private EnemyManager _enemyManager;
        private Player _player;

        void Start()
        {
            _boardManager = FindObjectOfType<BoardManager>();
            _enemyManager = FindObjectOfType<EnemyManager>();
            _player = FindObjectOfType<Player>();
        }

        // used by animator
        public void Explode()
        {
            Explode(gameObject.transform.position);
            Destroy(gameObject);
            _player.ActiveBombs--;
        }

        private void GenerateBombTrail(Vector2 position, Vector2 direction, GameObject blastExtension, GameObject blastEnd)
        {
            for (int i = 1; i <= Power; i++)
            {
                var adjacentTileVector = position + (direction * i);
                var adjacentTile = _boardManager.GetTile(adjacentTileVector);
                if (adjacentTile.TileType == TileType.Obstacle)
                {
                    break;
                }

                if (i == Power)
                {
                    Instantiate(blastEnd, new Vector3(adjacentTileVector.x, adjacentTileVector.y, 10), Quaternion.identity);
                    adjacentTile.AttemptDestroy();
                    _enemyManager.DestroyEnemyAtLocation(adjacentTileVector);
                    break;
                }

                var nextTile = _boardManager.GetTile(adjacentTileVector + direction);
                var tilePrefab = nextTile.TileType == TileType.Obstacle ? blastEnd : blastExtension;
                Instantiate(tilePrefab, new Vector3(adjacentTileVector.x, adjacentTileVector.y, 10), Quaternion.identity);
                adjacentTile.AttemptDestroy();
                _enemyManager.DestroyEnemyAtLocation(adjacentTileVector);
            }
        }

        private void Explode(Vector2 position)
        {
            Instantiate(Middle, new Vector3(position.x, position.y, 10), Quaternion.identity);
            _enemyManager.DestroyEnemyAtLocation(new Vector2(position.x, position.y));

            GenerateBombTrail(position, Vector2.up, Vertical, Top);
            GenerateBombTrail(position, Vector2.right, Horizontal, Right);
            GenerateBombTrail(position, Vector2.down, Vertical, Bottom);
            GenerateBombTrail(position, Vector2.left, Horizontal, Left);
        }
    }
}