using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TRexRunner.Entities
{
	public class SkyManager : IGameEntity
	{

		private const int CLOUD_MIN_POS_Y = 20;
		private const int CLOUD_MAX_POS_Y = 70;
		private const int CLOUD_MIN_DISTANCE = 150;
		private const int CLOUD_MAX_DISTANCE = 400;

		private const int STAR_MIN_POS_X = 10;
		private const int STAR_MAX_POS_Y = 60;

		private const int STAR_MIN_DISTANCE = 120;
		private const int STAR_MAX_DISTANCE = 380;

		public int DrawOrder => 0;

		private EntityManager _entityManager;
		private readonly ScoreBoard _scoreBoard;

		private double _lastCloudSpawnScore = -1;

		private int _targetCloudDistance = -1;
		private int _targetStarDistance = -1;
		private Random _random;
		private Texture2D _spriteSheet;
		private Trex _trex;

		public SkyManager(Trex trex, Texture2D spriteSheet, EntityManager entityManager, ScoreBoard scoreBoard)
		{
			_trex = trex;
			_spriteSheet = spriteSheet;
			_entityManager = entityManager;
			_scoreBoard = scoreBoard;
			_random = new Random();
		}

		public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{

		}

		public void Update(GameTime gameTime)
		{
			HandleCloudSpawning();

			HandleStarSpawning();

			foreach (var so in _entityManager.GetEntitiesOfType<SkyObject>().Where(c => c.Position.X < -200))
			{
				_entityManager.RemoveEntity(so);
			}
		}

		private void HandleStarSpawning()
		{
			IEnumerable<Star> stars = _entityManager.GetEntitiesOfType<Star>();

			if (stars.Count() <= 0 || (MainGame.WINDOW_WIDTH - stars.Max(c => c.Position.X) >= _targetStarDistance))
			{
				_targetStarDistance = _random.Next(STAR_MIN_DISTANCE, STAR_MAX_DISTANCE + 1);
				int posY = _random.Next(STAR_MIN_POS_X, STAR_MAX_POS_Y + 1);

				Star star = new Star(_trex, new Vector2(MainGame.WINDOW_WIDTH, posY), _spriteSheet);

				_entityManager.AddEntity(star);
			}
		}

		private void HandleCloudSpawning()
		{
			IEnumerable<Cloud> clouds = _entityManager.GetEntitiesOfType<Cloud>();

			if (clouds.Count() <= 0 || (MainGame.WINDOW_WIDTH - clouds.Max(c => c.Position.X) >= _targetCloudDistance))
			{
				_targetCloudDistance = _random.Next(CLOUD_MIN_DISTANCE, CLOUD_MAX_DISTANCE + 1);
				int posY = _random.Next(CLOUD_MIN_POS_Y, CLOUD_MAX_POS_Y + 1);

				Cloud cloud = new Cloud(_trex, new Vector2(MainGame.WINDOW_WIDTH, posY), _spriteSheet);

				_entityManager.AddEntity(cloud);
			}
		}
	}
}
