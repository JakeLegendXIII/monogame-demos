using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TRexRunner.Graphics;

namespace TRexRunner.Entities
{
	public class Cloud : SkyObject
	{
		private const int TEXTURE_COORDS_X = 67;
		private const int TEXTURE_COORDS_Y = 0;

		private const int TEXTURE_WIDTH = 46;
		private const int TEXTURE_HEIGHT = 17;

		private Sprite _sprite;

		public override float Speed => _trex.Speed * 0.5f;

		public Cloud(Trex trex, Vector2 position, Texture2D spriteSheet) : base(trex, position)
		{
			_sprite = new Sprite(spriteSheet, TEXTURE_COORDS_X, TEXTURE_COORDS_Y, TEXTURE_WIDTH, TEXTURE_HEIGHT);
		}		

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			_sprite.Draw(spriteBatch, Position);
		}
	}
}
