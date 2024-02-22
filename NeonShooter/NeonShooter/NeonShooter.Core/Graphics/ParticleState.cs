using Microsoft.Xna.Framework;

namespace NeonShooter.Core.Graphics
{
	public enum ParticleType { None, Enemy, Bullet, IgnoreGravity }
	public struct ParticleState
	{
		public Vector2 Velocity;
		public ParticleType Type;
		public float LengthMultiplier;
	}
}
