using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    class Bomb : MonoBehaviour
    {
        public float ExplodeTime;

        public int Power = 1;

        public GameObject Middle;
        public GameObject Top;
        public GameObject Bottom;
        public GameObject Left;
        public GameObject Right;
        public GameObject Horizontal;
        public GameObject Vertical;

        public LayerMask ObstacleLayer;

        private Player _player;
        private CircleCollider2D _collider;

        private bool _exploded;

        public void Start()
        {
            _player = FindObjectOfType<Player>();

            _collider = GetComponent<CircleCollider2D>();

            Invoke("Detonate", ExplodeTime);
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (!_exploded && other.CompareTag("Explosion"))
            {
                CancelInvoke("Detonate");
                Detonate();
            }
        }

        public void OnTriggerExit2D(Collider2D col)
        {
            _collider.isTrigger = false;
        }

        private IEnumerator CreateExplosions(Vector2 direction)
        {
            for (int i = 1; i <= Power; i++)
            {
                Vector2 position = transform.position;

                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, i, ObstacleLayer);
                if (!hit.collider)
                {
                    Vector3 a = position + direction * i;
                    a.z = 10;
                    var prefab = GetExplosionPrefab(direction, i == Power);
                    Instantiate(prefab, a, Quaternion.identity);
                }
                else
                {
                    var tile = hit.transform.GetComponent<Tile>();
                    if (tile != null && tile.Destroyable)
                    {
                        tile.AttemptDestroy();
                    }
                    break;
                }

                yield return new WaitForSeconds(0);
            }
        }

        private void Detonate()
        {
            _player.ActiveBombs--;
            _exploded = true;

            GetComponent<SpriteRenderer>().enabled = false;

            Instantiate(Middle, transform.position, Quaternion.identity);
            StartCoroutine(CreateExplosions(Vector2.right));
            StartCoroutine(CreateExplosions(Vector2.down));
            StartCoroutine(CreateExplosions(Vector2.up));
            StartCoroutine(CreateExplosions(Vector2.left));

            Destroy(gameObject, 1f);
        }

        private GameObject GetExplosionPrefab(Vector2 direction, bool isEnd)
        {
            if (direction == Vector2.up)
            {
                return isEnd ? Top : Vertical;
            }

            if (direction == Vector2.down)
            {
                return isEnd ? Bottom : Vertical;
            }

            if (direction == Vector2.left)
            {
                return isEnd ? Left : Horizontal;
            }

            //right
            return isEnd ? Right : Horizontal;
        }

        /*
        private void GenerateBombTrail(Vector2 position, Vector2 direction, GameObject blastExtension, GameObject blastEnd)
        {
            for (int i = 1; i <= Power; i++)
            {
                var adjacentTileVector = position + direction * i;
                var adjacentTile = _boardManager.GetTile(adjacentTileVector);
                if (adjacentTile.TileType == TileType.Obstacle)
                {
                    break;
                }

                GameObject tilePrefab = blastEnd;
                if (i != Power)
                {
                    var nextTile = _boardManager.GetTile(adjacentTileVector + direction);
                    tilePrefab = nextTile.TileType == TileType.Obstacle ? blastEnd : blastExtension;
                }

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
        */
    }
}