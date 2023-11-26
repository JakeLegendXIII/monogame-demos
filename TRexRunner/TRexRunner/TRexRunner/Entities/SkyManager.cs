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

		public int DrawOrder => 0;

		private EntityManager _entityManager;
		private readonly ScoreBoard _scoreBoard;

		private double _lastCloudSpawnScore = -1;

		private int _targetCloudDistance = -1;
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
			IEnumerable<Cloud> clouds = _entityManager.GetEntitiesOfType<Cloud>();

			if (clouds.Count() <= 0 || (MainGame.WINDOW_WIDTH - clouds.Max(c => c.Position.X) >= _targetCloudDistance))
			{
				_targetCloudDistance = _random.Next(CLOUD_MIN_DISTANCE, CLOUD_MAX_DISTANCE + 1);
				int posY = _random.Next(CLOUD_MIN_POS_Y, CLOUD_MAX_POS_Y + 1);

				Cloud cloud = new Cloud(_trex, new Vector2(MainGame.WINDOW_WIDTH, posY), _spriteSheet);

				_entityManager.AddEntity(cloud);
			}

			foreach (var cl in clouds.Where(c => c.Position.X < -200))
			{
				_entityManager.RemoveEntity(cl);
			}
		}
	}
}
