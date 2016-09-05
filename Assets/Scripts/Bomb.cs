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
        }

        void Update()
        {

            Invoke("Explode", DetonoteTime);
        }

        public void Explode()
        {
            Debug.Log(gameObject.transform.position);

            var pos = gameObject.transform.position;
            GameObject topTile = _generateLevel.GetTile(pos.x, pos.y + 1);
            if (topTile.GetComponent<Tile>().Destroyable)
            {
                Destroy(topTile);
            }

            Destroy(gameObject);
        }
    }
}