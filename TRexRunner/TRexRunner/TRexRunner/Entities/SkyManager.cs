using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TRexRunner.Entities
{
	public class SkyManager : IGameEntity
	{
		public int DrawOrder => 0;

		private EntityManager _entityManager;

		public SkyManager(EntityManager entityManager)
		{
			_entityManager = entityManager;
		}

		public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			
		}

		public void Update(GameTime gameTime)
		{
			
		}
	}
}
