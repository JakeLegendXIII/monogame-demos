using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TRexRunner.Graphics;

namespace TRexRunner.Entities
{
	public abstract class Obstacle : IGameEntity
	{
		private Trex _trex;
		protected Sprite _sprite;

		public Vector2 Position { get; private set; }

		public int DrawOrder { get; set; }

		public abstract Rectangle CollisionBox { get; }

		protected Obstacle(Trex trex, Vector2 position, Sprite sprite)
		{
			_trex = trex;
			Position = position;
			_sprite = sprite;
		}

		public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{

		}

		public void Update(GameTime gameTime)
		{			
			Position = new Vector2(Position.X - _trex.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds, Position.Y);
		}
	}
}
