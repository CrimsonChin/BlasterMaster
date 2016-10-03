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
            Explode(gameObject.transform.position.x, gameObject.transform.position.y);
            Destroy(gameObject);
        }

        private void Explode(float x, float y)
        {
            Instantiate(Middle, new Vector3(x, y, 10), Quaternion.identity);

            // handle Right
            for (int i = 1; i <= Power; i++)
            {
                Tile thisTile = _boardManager.GetTile(x + i, y);
                if (thisTile.TileType == TileType.Obstacle)
                {
                    break;
                }

                if (i == Power)
                {
                    Instantiate(Right, new Vector3(x + i, y, 10), Quaternion.identity);
                    thisTile.AttemptDestroy();
                    break;
                }

               
                Tile tile = _boardManager.GetTile(x + i + 1, y); // look ahead
                Instantiate(tile.TileType == TileType.Obstacle ? Right : Horizontal, new Vector3(x + i, y, 10), Quaternion.identity);
                thisTile.AttemptDestroy();
            }

            // left
            for (int i = 1; i <= Power; i++)
            {
                Tile thisTile = _boardManager.GetTile(x - i, y);
                if (thisTile.TileType == TileType.Obstacle)
                {
                    break;
                }

                if (i == Power)
                {
                    Instantiate(Left, new Vector3(x - i, y, 10), Quaternion.identity);
                    thisTile.AttemptDestroy();
                    break;
                }

                Tile tile = _boardManager.GetTile(x - i - 1, y); // look ahead
                Instantiate(tile.TileType == TileType.Obstacle ? Left : Horizontal, new Vector3(x - i, y, 10), Quaternion.identity);
                thisTile.AttemptDestroy();

            }

            // top
            for (int i = 1; i <= Power; i++)
            {
                Tile thisTile = _boardManager.GetTile(x, y + i);
                if (thisTile.TileType == TileType.Obstacle)
                {
                    break;
                }

                if (i == Power)
                {
                    Instantiate(Top, new Vector3(x, y + i, 10), Quaternion.identity);
                    thisTile.AttemptDestroy();
                    break;
                }

                Tile tile = _boardManager.GetTile(x, y + i + 1); // look ahead
                Instantiate(tile.TileType == TileType.Obstacle ? Top : Vertical, new Vector3(x, y + i, 10), Quaternion.identity);
                thisTile.AttemptDestroy();
            }

            // bottom
            for (int i = 1; i <= Power; i++)
            {
                Tile thisTile = _boardManager.GetTile(x, y - i);
                if (thisTile.TileType == TileType.Obstacle)
                {
                    break;
                }

                if (i == Power)
                {
                    Instantiate(Bottom, new Vector3(x, y - i, 10), Quaternion.identity);
                    thisTile.AttemptDestroy();
                    break;
                }

                Tile tile = _boardManager.GetTile(x, y - i - 1); // look ahead
                Instantiate(tile.TileType == TileType.Obstacle ? Bottom : Vertical, new Vector3(x, y - i, 10), Quaternion.identity);
                thisTile.AttemptDestroy();

            }
        }
    }
}