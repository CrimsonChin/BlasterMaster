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

        void Start()
        {
            _boardManager = FindObjectOfType<BoardManager>();
        }

        // used by animator
        public void Explode()
        {
            Explode(gameObject.transform.position);
            Destroy(gameObject);
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
                    break;
                }

                var nextTile = _boardManager.GetTile(adjacentTileVector + direction);
                var tilePrefab = nextTile.TileType == TileType.Obstacle ? blastEnd : blastExtension;
                Instantiate(tilePrefab, new Vector3(adjacentTileVector.x, adjacentTileVector.y, 10), Quaternion.identity);
                adjacentTile.AttemptDestroy();
            }
        }

        private void Explode(Vector2 position)
        {
            Instantiate(Middle, new Vector3(position.x, position.y, 10), Quaternion.identity);

            GenerateBombTrail(position, Vector2.up, Vertical, Top);
            GenerateBombTrail(position, Vector2.right, Horizontal, Right);
            GenerateBombTrail(position, Vector2.down, Vertical, Bottom);
            GenerateBombTrail(position, Vector2.left, Horizontal, Left);
        }
    }
}