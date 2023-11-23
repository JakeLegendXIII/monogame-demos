using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TRexRunner.Entities
{
	public abstract class Obstacle : IGameEntity
	{
		private Trex _trex;

		public Vector2 Position { get; private set; }

		public int DrawOrder { get; set; }

		public abstract Rectangle CollisionBox { get; }

		protected Obstacle(Trex trex, Vector2 position)
		{
			_trex = trex;
			Position = position;
		}

		public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);

		public void Update(GameTime gameTime)
		{
			Position = new Vector2(Position.X - _trex.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds, Position.Y);
		}
	}
}
