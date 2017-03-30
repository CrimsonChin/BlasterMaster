using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Extensions;
using Assets.Scripts.Models;
using UnityEngine;
using Random = UnityEngine.Random;

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
				PlayableBoardHeight = 15,
				PlayableBoardWidth = 15,
				BombRangePowerUpCount = 2,
				BombsHeldPowerUpCount = 2,
				SpeedPowerUpCount = 2,
				CrateCount = 50,
				EnemyCount = 5
			};

			PrepareBoard(levelConfiguration);
			//AddCratesToBoard(levelConfiguration);
			SetCameraToCentreOfBoard(levelConfiguration);
			// TODO populate enemies or return the baord
		}

		private void SetCameraToCentreOfBoard(LevelConfiguration levelConfiguration)
		{
			Camera.transform.position = new Vector3((levelConfiguration.PlayableBoardWidth + 2) / 2f,
				(levelConfiguration.PlayableBoardHeight + 2) / 2f);
		}

		public void PrepareBoard(LevelConfiguration configuration)
		{
			List<Vector2> paths = new List<Vector2>();

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

					//var gameobject = (isEdge || isColumn) ? Block : Grass;
					GameObject prefab = Block;
					if (!(isEdge || isColumn))
					{
						prefab = Grass;
						paths.Add(new Vector2(x, y));
					}
					var tile = Instantiate(prefab, new Vector3(x, y, 100), Quaternion.identity, board.transform);
					_board[x, y] = tile;
				}
			}

			AddCratesToBoard(configuration, paths);
		}

		public void AddCratesToBoard(LevelConfiguration configuration, IList<Vector2> paths)
		{
			int crateCount = configuration.CrateCount;
			var cratesParent = new GameObject("Crates");
			var powerUps = AddAllPowerUpsToStack(configuration);

			// remove the safe zones
			for (int i = paths.Count-1; i >= 0; i--)
			{
				var path = paths[i];

				const int safeZone = 3;
				if (path.x < safeZone && path.y < safeZone)
				{
					paths.RemoveAt(i);
				}
			}

			// distribute crates
			var pathStack = new Stack<Vector2>(paths.KnuthShuffle());
			while (crateCount > 0 && pathStack.Count > 0)
			{
				var path = pathStack.Pop();
				var crate = Instantiate(Crate, new Vector3(path.x, path.y, 99), Quaternion.identity, cratesParent.transform);
				if (powerUps.Count > 0)
				{
					crate.GetComponent<Tile>().PowerUp = powerUps.Pop();
				}

				_board[(int)path.x, (int)path.y] = crate;
				crateCount--;
			}

			if (pathStack.Count < configuration.EnemyCount)
			{
				throw  new InvalidOperationException("Theres not enough space to distribute the enemies");
			}

			var enemyCount = configuration.EnemyCount;
			foreach (var vector2 in pathStack)
			{
				if (enemyCount == 0)
				{
					break;
				}
				Debug.Log("creating an enemy");
				Instantiate(Troll, new Vector3(vector2.x, vector2.y, 10), Quaternion.identity);
				enemyCount--;
			}
		}

		public void AddCratesToBoard(LevelConfiguration configuration)
		{
			var shuffledCrates = PrepareCratePrefabs(configuration).KnuthShuffle();
			var crateObjects = new Stack<GameObject>(shuffledCrates);

			while (crateObjects.Count > 0)
			{
				for (var y = 1; y < _board.GetLength(0); y++)
				{
					for (var x = 1; x < _board.GetLength(1); x++)
					{
						const int safeZone = 3;
						var isPlayerStartZone = x < safeZone && y < safeZone;

						if (isPlayerStartZone || crateObjects.Count <= 0 || Random.Range(0, 100) % 7 != 0)
						{
							continue;
						}

						var tile = _board[x, y].GetComponent<Tile>();
						if (tile.IsPath)
						{
							var crate = crateObjects.Pop();
							crate.transform.position = new Vector3(x, y, 90); // The crates or on top of the board 

							// push the crate into the main board so we can just interegate one board
							_board[x, y] = crate;
						}
					}
				}
			}
		}

		private IList<GameObject> PrepareCratePrefabs(LevelConfiguration configuration)
		{
			var cratesParent = new GameObject("Crates");

			var powerUps = AddAllPowerUpsToStack(configuration);
			var crates = new List<GameObject>();

			for (var i = 0; i < configuration.CrateCount; i++)
			{
				// hide them all under the board
				var crate = Instantiate(Crate, new Vector3(0, 0, 120), Quaternion.identity, cratesParent.transform); 
				crates.Add(crate);
				if (powerUps.Count > 0)
				{
					crate.GetComponent<Tile>().PowerUp = powerUps.Pop();
				}
			}

			return crates;
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
