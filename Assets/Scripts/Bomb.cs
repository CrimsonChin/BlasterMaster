using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class Bomb : MonoBehaviour
    {
        public float DetonoteTime = 3f;
        private GenerateLevel _generateLevel;

        void Start()
        {
            _generateLevel = FindObjectOfType<GenerateLevel>();
            Detonate();
        }

        public void Detonate()
        {
            Detonate(DetonoteTime);
        }

        public void Detonate(float fuseTime)
        {
            Invoke("Explode", fuseTime);
        }

        public void Explode()
        {
            Debug.Log(gameObject.transform.position);

            var pos = gameObject.transform.position;
            DestroyTile(pos.x, pos.y + 1);
            DestroyTile(pos.x, pos.y + -1);
            DestroyTile(pos.x + 1, pos.y);
            DestroyTile(pos.x - 1, pos.y);

            Destroy(gameObject);
        }

        private void DestroyTile(float x, float y)
        {
            var tileGameObject = _generateLevel.GetTile(x, y);
            var tile = tileGameObject.GetComponent<Tile>();

            if (tile != null && tile.Destroyable)
            {
                Destroy(tileGameObject);
            }
        }
    }
}