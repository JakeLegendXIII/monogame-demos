using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NeonShooter.Core.Graphics;
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
			throw new NotImplementedException();
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			// make the size of the black hole pulsate 
			float scale = 1 + 0.1f * (float)Math.Sin(10 * GameRoot.GameTime.TotalGameTime.TotalSeconds);
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
