using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NeonShooter.Core.Graphics;
using NeonShooter.Core.Input;
using NeonShooter.Core.Utils;
using System;

namespace NeonShooter.Core.Entities
{
	class PlayerShip : Entity
	{
		private static PlayerShip instance;

		public static PlayerShip Instance
		{
			get
			{
				if (instance == null)
					instance = new PlayerShip();
				return instance;
			}
		}

		const int cooldownFrames = 6;
		int cooldownRemaining = 0;

		int framesUntilRespawn = 0;
		public bool IsDead { get { return framesUntilRespawn > 0; } }

		static Random rand = new Random();

		private PlayerShip()
		{
			image = Art.Player;
			Position = MainGame.ScreenSize / 2;
			Radius = 10;
		}

		public override void Update()
		{
			if (IsDead)
			{
				if (--framesUntilRespawn == 0)
				{
					if (PlayerStatus.Lives == 0)
					{
						PlayerStatus.Reset();
						Position = MainGame.ScreenSize / 2;
					}
					// MainGame.Grid.ApplyDirectedForce(new Vector3(0, 0, 5000), new Vector3(Position, 0), 50);
				}

				return;
			}

			var aim = InputManager.GetAimDirection();
			if (aim.LengthSquared() > 0 && cooldownRemaining <= 0)
			{
				cooldownRemaining = cooldownFrames;
				float aimAngle = aim.ToAngle();
				Quaternion aimQuat = Quaternion.CreateFromYawPitchRoll(0, 0, aimAngle);

				float randomSpread = rand.NextFloat(-0.04f, 0.04f) + rand.NextFloat(-0.04f, 0.04f);
				Vector2 vel = MathUtil.FromPolar(aimAngle + randomSpread, 11f);

				Vector2 offset = Vector2.Transform(new Vector2(35, -8), aimQuat);
				EntityManager.Add(new Bullet(Position + offset, vel));

				offset = Vector2.Transform(new Vector2(35, 8), aimQuat);
				EntityManager.Add(new Bullet(Position + offset, vel));

				Sound.Sound.Shot.Play(0.2f, rand.NextFloat(-0.2f, 0.2f), 0);
			}

			if (cooldownRemaining > 0)
				cooldownRemaining--;


			const float speed = 8;
			Velocity = speed * InputManager.GetMovementDirection();
			Position += Velocity;
			Position = Vector2.Clamp(Position, Size / 2, MainGame.ScreenSize - Size / 2);

			if (Velocity.LengthSquared() > 0)
				Orientation = Velocity.ToAngle();

			Velocity = Vector2.Zero;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			if (!IsDead)
				base.Draw(spriteBatch);
		}

		public void Kill()
		{
			PlayerStatus.RemoveLife();
			framesUntilRespawn = PlayerStatus.IsGameOver ? 300 : 120;
		}
	}
}
