using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using TRexRunner.Graphics;

namespace TRexRunner.Entities
{
	public class GroundManager : IGameEntity
	{
		private const float GROUND_TILE_POS_Y = 119;

		private const int GROUND_SPRITE_WIDTH = 600;
		private const int GROUND_SPRITE_HEIGHT = 14;
		private const int SPRITE_POS_X = 2;
		private const int SPRITE_POS_Y = 54;

		private Texture2D _spriteSheet;

		private readonly List<GroundTile> _groundTiles;

		private readonly EntityManager _entityManager;

		private Sprite _regularSprite;		
		private Sprite _bumpySprite;
		private Trex _trex;
		private Random _random;

		public int DrawOrder { get; set; }

		public GroundManager(Texture2D spriteSheet, EntityManager entityManager, Trex trex)
		{
			_spriteSheet = spriteSheet;
			_groundTiles = new List<GroundTile>();

			_entityManager = entityManager;

			_regularSprite = new Sprite(spriteSheet, SPRITE_POS_X, SPRITE_POS_Y, GROUND_SPRITE_WIDTH, GROUND_SPRITE_HEIGHT);
			_bumpySprite = new Sprite(spriteSheet, SPRITE_POS_X + GROUND_SPRITE_WIDTH, SPRITE_POS_Y, GROUND_SPRITE_WIDTH, GROUND_SPRITE_HEIGHT);
			_trex = trex;
			_random = new Random();
		}

		public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{

		}

		public void Update(GameTime gameTime)
		{
			if (_groundTiles.Any())
			{
				float maxPosX = _groundTiles.Max(t => t.PositionX);

				if (maxPosX < 0)
				{
					SpawnTile(maxPosX);
				}
			}

			List<GroundTile> tilesToRemove = new List<GroundTile>();

			foreach(GroundTile groundTile in _groundTiles)
			{
				groundTile.PositionX -= _trex.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (groundTile.PositionX < - GROUND_SPRITE_WIDTH)
                {
					_entityManager.RemoveEntity(groundTile);
					tilesToRemove.Add(groundTile);
                }
            }

			foreach(GroundTile groundTile in tilesToRemove)
			{
				_groundTiles.Remove(groundTile);
			}
		}

		public void Initialize()
		{
			GroundTile groundTile = CreateRegularTile(0);

			_groundTiles.Add(groundTile);

			_entityManager.AddEntity(groundTile);
		}

		private GroundTile CreateRegularTile(float positionX)
		{
			GroundTile groundTile = new GroundTile(positionX, GROUND_TILE_POS_Y, _regularSprite);

			return groundTile;
		}

		private GroundTile CreateBumpyTile(float positionX)
		{
			GroundTile groundTile = new GroundTile(positionX, GROUND_TILE_POS_Y, _bumpySprite);

			return groundTile;
		}

		private void SpawnTile(float maxPosX)
		{
			GroundTile groundTile;
			double randomNumber = _random.NextDouble();

			float positionX = maxPosX + GROUND_SPRITE_WIDTH;

			if (randomNumber > 0.5)
			{
				groundTile = CreateBumpyTile(positionX);
			}
			else
			{
				groundTile = CreateRegularTile(positionX);
			}

			_entityManager.AddEntity(groundTile);
			_groundTiles.Add(groundTile);			
		}
	}
}
