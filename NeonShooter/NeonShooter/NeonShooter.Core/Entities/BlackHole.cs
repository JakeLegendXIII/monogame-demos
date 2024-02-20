using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NeonShooter.Core.Graphics;
using NeonShooter.Core.Utils;
using System;

namespace NeonShooter.Core.Entities
{
	class BlackHole : Entity
	{
		private static Random rand = new Random();
		private int hitpoints = 10;

		public BlackHole(Vector2 position)
		{
			image = Art.BlackHole;
			Position = position;
			Radius = image.Width / 2f;
		}

		public override void Update()
		{
			var entities = EntityManager.GetNearbyEntities(Position, 250);

			foreach (Entity entity in entities)
			{
				if (entity is Enemy && !(entity as Enemy).IsActive)
					continue;
				// bullets are repelled by black holes and everything else is attracted 
				if (entity is Bullet)
					entity.Velocity += (entity.Position - Position).ScaleTo(0.3f);
				else
				{
					var dPos = Position - entity.Position;
					var length = dPos.Length();
					entity.Velocity += dPos.ScaleTo(MathHelper.Lerp(2, 0, length / 250f));
				}
			}
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			// make the size of the black hole pulsate 
			float scale = 1 + 0.1f * (float)Math.Sin(10 * MainGame.GameTime.TotalGameTime.TotalSeconds);
			spriteBatch.Draw(image, Position, null, color, Orientation, Size / 2f, scale, 0, 0);
		}

		public void WasShot()
		{
			hitpoints--;
			if (hitpoints <= 0)
				IsExpired = true;
		}

		public void Kill()
		{
			hitpoints = 0;
			WasShot();
		}		

	}
}
