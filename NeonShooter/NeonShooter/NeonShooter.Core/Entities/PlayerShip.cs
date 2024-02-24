﻿using Microsoft.Xna.Framework;
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

			MakeExhaustFire();
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

			Color explosionColor = new Color(0.8f, 0.8f, 0.4f); // yellow

			for (int i = 0; i < 1200; i++)
			{
				float speed = 18f * (1f - 1 / rand.NextFloat(1f, 10f));
				Color color = Color.Lerp(Color.White, explosionColor, rand.NextFloat(0, 1));
				var state = new ParticleState()
				{
					Velocity = rand.NextVector2(speed, speed),
					Type = ParticleType.None,
					LengthMultiplier = 1
				};

				MainGame.ParticleManager.CreateParticle(Art.LineParticle, Position, color, 190, 1.5f, state);
			}
		}

		private void MakeExhaustFire()
		{
			if (Velocity.LengthSquared() > 0.1f)
			{
				// set up some variables
				Orientation = Velocity.ToAngle();
				Quaternion rot = Quaternion.CreateFromYawPitchRoll(0f, 0f, Orientation);

				double t = MainGame.GameTime.TotalGameTime.TotalSeconds;
				// The primary velocity of the particles is 3 pixels/frame in the direction opposite to which the ship is travelling.
				Vector2 baseVel = Velocity.ScaleTo(-3);
				// Calculate the sideways velocity for the two side streams. The direction is perpendicular to the ship's velocity and the
				// magnitude varies sinusoidally.
				Vector2 perpVel = new Vector2(baseVel.Y, -baseVel.X) * (0.6f * (float)Math.Sin(t * 10));
				Color sideColor = new Color(200, 38, 9);    // deep red
				Color midColor = new Color(255, 187, 30);   // orange-yellow
				Vector2 pos = Position + Vector2.Transform(new Vector2(-25, 0), rot);   // position of the ship's exhaust pipe.
				const float alpha = 0.7f;

				// middle particle stream
				Vector2 velMid = baseVel + rand.NextVector2(0, 1);
				MainGame.ParticleManager.CreateParticle(Art.LineParticle, pos, Color.White * alpha, 60f, new Vector2(0.5f, 1),
					new ParticleState(velMid, ParticleType.Enemy));
				MainGame.ParticleManager.CreateParticle(Art.Glow, pos, midColor * alpha, 60f, new Vector2(0.5f, 1),
					new ParticleState(velMid, ParticleType.Enemy));

				// side particle streams
				Vector2 vel1 = baseVel + perpVel + rand.NextVector2(0, 0.3f);
				Vector2 vel2 = baseVel - perpVel + rand.NextVector2(0, 0.3f);
				MainGame.ParticleManager.CreateParticle(Art.LineParticle, pos, Color.White * alpha, 60f, new Vector2(0.5f, 1),
					new ParticleState(vel1, ParticleType.Enemy));
				MainGame.ParticleManager.CreateParticle(Art.LineParticle, pos, Color.White * alpha, 60f, new Vector2(0.5f, 1),
					new ParticleState(vel2, ParticleType.Enemy));

				MainGame.ParticleManager.CreateParticle(Art.Glow, pos, sideColor * alpha, 60f, new Vector2(0.5f, 1),
					new ParticleState(vel1, ParticleType.Enemy));
				MainGame.ParticleManager.CreateParticle(Art.Glow, pos, sideColor * alpha, 60f, new Vector2(0.5f, 1),
					new ParticleState(vel2, ParticleType.Enemy));
			}
		}
	}
}
