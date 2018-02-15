using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Extensions;
using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts
{
	class BoardGenerator : MonoBehaviour
	{
		public GameObject Troll;

		public GameObject SpeedPowerUp;
		public GameObject BombRangePowerUp;
		public GameObject BombsHeldPowerUp;

		public GameObject Block;
		public GameObject Grass;
		public GameObject Crate;

		private GameObject[,] _board;
		public GameObject Camera;

		void Start()
		{
			var levelConfiguration = new LevelConfiguration
			{
				PlayableBoardHeight = 5,
				PlayableBoardWidth = 5,
				BombRangePowerUpCount = 2,
				BombsHeldPowerUpCount = 2,
				SpeedPowerUpCount = 2,
				CrateCount = 5,
				EnemyCount = 2
			};

			var clearTileVectors = PrepareBoard(levelConfiguration);
			RemovePathsInThePlayerStartZone(clearTileVectors);
			AddCratesToBoard(levelConfiguration, clearTileVectors);
			DistributeEnemies(levelConfiguration, clearTileVectors);
			SetCameraToCentreOfBoard(levelConfiguration);

		}

		private void SetCameraToCentreOfBoard(LevelConfiguration levelConfiguration)
		{
			var totalWidth = levelConfiguration.PlayableBoardWidth + 2;
			var totalHeight = levelConfiguration.PlayableBoardWidth + 2;
			Camera.transform.position = new Vector3(totalWidth / 2f, totalHeight / 2f);
		}

		public List<Vector2> PrepareBoard(LevelConfiguration configuration)
		{
			var clearTileVectors = new List<Vector2>();

			var length = configuration.PlayableBoardWidth;
			var height = configuration.PlayableBoardHeight;

			var board = new GameObject("Board");
			_board = new GameObject[length + 2, height + 2];
			for (var y = 0; y <= height + 1; y++)
			{
				for (var x = 0; x <= length + 1; x++)
				{
					var isEdge = x == 0 || y == 0 || x == length + 1 || y == height + 1;
					var isColumn = x % 2 == 0 && y % 2 == 0;

					var prefab = Block;
					if (!(isEdge || isColumn))
					{
						prefab = Grass;
						clearTileVectors.Add(new Vector2(x, y));
					}

					var tile = Instantiate(prefab, new Vector3(x, y, 100), Quaternion.identity, board.transform);
					_board[x, y] = tile;
				}
			}

			return clearTileVectors;
		}

		private void AddCratesToBoard(LevelConfiguration configuration, IList<Vector2> clearTileVectors)
		{
			var crateCount = configuration.CrateCount;
			var cratesParent = new GameObject("Crates");
			var powerUps = AddAllPowerUpsToStack(configuration);

			var clearTileStack = new Stack<Vector2>(clearTileVectors.KnuthShuffle());
			while (crateCount > 0 && clearTileStack.Count > 0)
			{
				var path = clearTileStack.Pop();
				var crate = Instantiate(Crate, new Vector3(path.x, path.y, 99), Quaternion.identity, cratesParent.transform);
				if (powerUps.Count > 0)
				{
					crate.GetComponent<Tile>().PowerUp = powerUps.Pop();
				}

				_board[(int)path.x, (int)path.y] = crate;
				crateCount--;
			}
		}

		private static void RemovePathsInThePlayerStartZone(IEnumerable<Vector2> clearTileVectors)
		{
			const int safeZone = 3;
			var clearTileList = clearTileVectors as List<Vector2>;
			if (clearTileList != null && clearTileList.Any())
			{
				clearTileList.RemoveAll(path => path.x < safeZone && path.y < safeZone);
			}
		}

		private void DistributeEnemies(LevelConfiguration configuration, ICollection<Vector2> clearTileStack)
		{
			if (clearTileStack.Count < configuration.EnemyCount)
			{
				throw new InvalidOperationException("There is not enough space to distribute the enemies.  Good maths!");
			}

		    var enemyManager = FindObjectOfType<EnemyManager>();
			var enemyCount = configuration.EnemyCount;
			foreach (var vector2 in clearTileStack)
			{
				if (enemyCount == 0)
				{
					break;
				}
				Instantiate(Troll, new Vector3(vector2.x, vector2.y, 10), Quaternion.identity);
				enemyCount--;
			    enemyManager.EnemyCount++;

			}
		}

		private Stack<GameObject> AddAllPowerUpsToStack(LevelConfiguration configuration)
		{
			var powerups = new Stack<GameObject>();

			powerups.Repeat(SpeedPowerUp, configuration.SpeedPowerUpCount);
			powerups.Repeat(BombRangePowerUp, configuration.BombRangePowerUpCount);
			powerups.Repeat(BombsHeldPowerUp, configuration.BombsHeldPowerUpCount);

			return powerups;
		}
	}
}
