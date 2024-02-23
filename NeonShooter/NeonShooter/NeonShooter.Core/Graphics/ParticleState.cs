using Microsoft.Xna.Framework;
using NeonShooter.Core.Utils;
using System;

namespace NeonShooter.Core.Graphics
{
	public enum ParticleType { None, Enemy, Bullet, IgnoreGravity }

	public struct ParticleState
	{
		public Vector2 Velocity;
		public ParticleType Type;
		public float LengthMultiplier;

		private static Random rand = new Random();

		public ParticleState(Vector2 velocity, ParticleType type, float lengthMultiplier = 1f)
		{
			Velocity = velocity;
			Type = type;
			LengthMultiplier = lengthMultiplier;
		}

		public static ParticleState GetRandom(float minVel, float maxVel)
		{
			var state = new ParticleState();
			state.Velocity = rand.NextVector2(minVel, maxVel);
			state.Type = ParticleType.None;
			state.LengthMultiplier = 1;

			return state;
		}
	}
}
