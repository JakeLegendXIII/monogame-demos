using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace TRexRunner.Entities
{
	public class ObstacleManager : IGameEntity
	{
		private const float MIN_SPAWN_DISTANCE = 10f;

		private const int MIN_OBSTACLE_DISTANCE = 6;
		private const int MAX_OBSTACLE_DISTANCE = 28;

		private double _lastSpawnScore = -1;
		private double _currentTargetDistance;

		private readonly EntityManager _entityManager;
		private readonly Trex _trex;
		private readonly ScoreBoard _scoreBoard;

		public bool IsEnabled { get; set; }
		public bool CanSpawnObstacles => IsEnabled && _scoreBoard.Score >= MIN_SPAWN_DISTANCE;
		public int DrawOrder => 0;

		private Random _random;

		private Texture2D _spriteSheet;

		public ObstacleManager(EntityManager entityManager, Trex trex, ScoreBoard scoreBoard, Texture2D spriteSheet)
		{
			_entityManager = entityManager;
			_trex = trex;
			_scoreBoard = scoreBoard;
			_random = new Random();
			_spriteSheet = spriteSheet;
		}

		public void Draw(SpriteBatch spriteBatch, GameTime gameTime) { }

		public void Update(GameTime gameTime)
		{
			if (!IsEnabled)
			{
				return;
			}

			if (CanSpawnObstacles && 
				(_lastSpawnScore <= 0 || ( _scoreBoard.Score - _lastSpawnScore >= _currentTargetDistance)))
			{
				_currentTargetDistance = _random.NextDouble() * (MAX_OBSTACLE_DISTANCE + MIN_OBSTACLE_DISTANCE) + MIN_SPAWN_DISTANCE;


				_lastSpawnScore = _scoreBoard.Score;
				
				SpawnRandomObstacle();

			}

			foreach(Obstacle obstacle in _entityManager.GetEntitiesOfType<Obstacle>())
			{
				if (obstacle.Position.X < -200)
				{
					_entityManager.RemoveEntity(obstacle);
				}
			}

		}

		private void SpawnRandomObstacle()
		{
			// TODO: Create instance of obstacle and add to entity manager
			
			Obstacle obstacle = null;

			CactusGroup.GroupSize randomGroupSize = (CactusGroup.GroupSize)_random.Next((int)CactusGroup.GroupSize.Small, (int)CactusGroup.GroupSize.Large + 1);

			obstacle = new CactusGroup(_spriteSheet, false, randomGroupSize, _trex, new Vector2(MainGame.WINDOW_WIDTH, 100));

			_entityManager.AddEntity(obstacle);
		}
	}
}
