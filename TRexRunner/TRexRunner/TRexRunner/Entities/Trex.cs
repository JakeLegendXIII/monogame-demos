using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TRexRunner.Graphics;

namespace TRexRunner.Entities
{
	public class Trex : IGameEntity
	{
		private const int TREX_IDLE_BACKGROUND_POS_X = 40;
		private const int TREX_IDLE_BACKGROUND_POS_Y = 0;

		public const int TREX_DEFAULT_POS_X = 848;
		public const int TREX_DEFAULT_POS_Y = 0;
		public const int TREX_DEFAULT_WIDTH = 44;
		public const int TREX_DEFAULT_HEIGHT = 52;

		private Sprite _idleBackgroundSprite;

        public Sprite Sprite { get; private set; }
        public Vector2 Position { get; set; }
		public TrexState State { get; set; }
        public bool IsAlive { get; private set; }
		public float Speed { get; set; }
        public int DrawOrder { get; set; }

        public Trex(Texture2D spriteSheet, Vector2 position)
        {
            Sprite = new Sprite(spriteSheet, TREX_DEFAULT_POS_X,TREX_DEFAULT_POS_Y, TREX_DEFAULT_WIDTH, TREX_DEFAULT_HEIGHT);
			Position = position;
			_idleBackgroundSprite = new Sprite(spriteSheet, TREX_IDLE_BACKGROUND_POS_X, TREX_IDLE_BACKGROUND_POS_Y, TREX_DEFAULT_WIDTH, TREX_DEFAULT_HEIGHT);
			State = TrexState.Idle;
		}

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			if (State == TrexState.Idle)
			{
				_idleBackgroundSprite.Draw(spriteBatch, Position);
			}				

			Sprite.Draw(spriteBatch, Position);
		}

		public void Update(GameTime gameTime)
		{
			
		}
	}
}
