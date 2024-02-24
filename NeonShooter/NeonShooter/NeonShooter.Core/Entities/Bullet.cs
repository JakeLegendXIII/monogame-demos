using Microsoft.Xna.Framework;
using NeonShooter.Core.Graphics;
using NeonShooter.Core.Utils;
using System;

namespace NeonShooter.Core.Entities
{
	class Bullet : Entity
	{
		private static Random rand = new Random();

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
			// MainGame.Grid.ApplyExplosiveForce(0.5f * Velocity.Length(), Position, 80);

			// delete bullets that go off-screen
			if (!MainGame.Viewport.Bounds.Contains(Position.ToPoint()))
			{
				IsExpired = true;

				for (int i = 0; i < 30; i++)
					MainGame.ParticleManager.CreateParticle(Art.LineParticle, Position, Color.LightBlue, 50, 1,
						new ParticleState() { Velocity = rand.NextVector2(0, 9), Type = ParticleType.Bullet, LengthMultiplier = 1 });

			}
		}
	}
}
