using Microsoft.Xna.Framework;
using NeonShooter.Core.Graphics;
using NeonShooter.Core.Utils;

namespace NeonShooter.Core.Entities
{
	class Bullet : Entity
	{
		public Bullet(Vector2 position, Vector2 velocity)
		{
			image = Art.Bullet;
			Position = position;
			Velocity = velocity;
			Orientation = Velocity.ToAngle();
			Radius = 8;
		}
		public override void Update()
		{
			if (Velocity.LengthSquared() > 0)
				Orientation = Velocity.ToAngle();
			Position += Velocity;
			// delete bullets that go off-screen 
			if (!MainGame.Viewport.Bounds.Contains(Position.ToPoint()))
				IsExpired = true;
		}
	}
}
