using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NeonShooter.Core.Utils;
using System.Collections.Generic;

namespace NeonShooter.Core.Entities
{
	class Enemy : Entity
	{
		private int timeUntilStart = 60;
		public bool IsActive { get { return timeUntilStart <= 0; } }

		public Enemy(Texture2D image, Vector2 position)
		{
			this.image = image;
			Position = position;
			Radius = image.Width / 2f;
			color = Color.Transparent;
		}

		public override void Update()
		{
			if (timeUntilStart <= 0)
			{
				// enemy behaviour logic goes here. 
			}
			else
			{
				timeUntilStart--;
				color = Color.White * (1 - timeUntilStart / 60f);
			}
			Position += Velocity;
			Position = Vector2.Clamp(Position, Size / 2, MainGame.ScreenSize - Size / 2);
			Velocity *= 0.8f;
		}

		public void WasShot()
		{
			IsExpired = true;
		}

		#region Enemy behaviours
		IEnumerable<int> FollowPlayer(float acceleration = 1f)
		{
			while (true)
			{
				Velocity += (PlayerShip.Instance.Position - Position).ScaleTo(acceleration);
				if (Velocity != Vector2.Zero)
					Orientation = Velocity.ToAngle();
				yield return 0;
			}
		}

		#endregion
	}
}
