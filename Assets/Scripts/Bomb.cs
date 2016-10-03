using UnityEngine;

namespace Assets.Scripts
{
    class Bomb : MonoBehaviour
    {
        public float DetonoteTime = 3f;
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

        //public void Detonate()
        //{
        //    Detonate(DetonoteTime);
        //}

        //public void Detonate(float fuseTime)
        //{
        //    Invoke("Explode", fuseTime);
        //}

        // used by animator
        public void Explode()
        {
            Debug.Log(gameObject.transform.position);

            var pos = gameObject.transform.position;
            DestroyTile(pos.x, pos.y + 1);
            DestroyTile(pos.x, pos.y + -1);
            DestroyTile(pos.x + 1, pos.y);
            DestroyTile(pos.x - 1, pos.y);

            Destroy(gameObject);

            Explode(gameObject.transform.position.x, gameObject.transform.position.y);
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
                    break;
                }

                Tile tile = _boardManager.GetTile(x + i + 1, y); // look ahead
                Instantiate(tile.TileType == TileType.Obstacle ? Right : Horizontal, new Vector3(x + i, y, 10), Quaternion.identity);
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
                    break;
                }

                Tile tile = _boardManager.GetTile(x + i - 1, y); // look ahead
                Instantiate(tile.TileType == TileType.Obstacle ? Left : Horizontal, new Vector3(x - i, y, 10), Quaternion.identity);

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
                    break;
                }

                Tile tile = _boardManager.GetTile(x, y + i + 1); // look ahead
                Instantiate(tile.TileType == TileType.Obstacle ? Top : Vertical, new Vector3(x, y + i, 10), Quaternion.identity);
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
                    break;
                }

                Tile t = _boardManager.GetTile(x, y - i - 1); // look ahead
                Instantiate(t.TileType == TileType.Obstacle ? Bottom : Vertical, new Vector3(x, y - i, 10), Quaternion.identity);

            }
        }

        private void DestroyTile(float x, float y)
        {
            var tileGameObject = _boardManager.GetTileGameObject(x, y);
            var tile = tileGameObject.GetComponent<Tile>();

            if (tile != null && tile.Destroyable)
            {
                Destroy(tileGameObject);
            }
        }
    }
}