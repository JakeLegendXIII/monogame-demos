using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TRexRunner.Graphics;

namespace TRexRunner.Entities
{
	public class Trex : IGameEntity
	{
		private const int TREX_DEFAULT_POS_X = 848;
		private const int TREX_DEFAULT_POS_Y = 0;
		private const int TREX_DEFAULT_WIDTH = 44;
		private const int TREX_DEFAULT_HEIGHT = 52;

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
		}

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			Sprite.Draw(spriteBatch, Position);
		}

		public void Update(GameTime gameTime)
		{
			
		}
	}
}
