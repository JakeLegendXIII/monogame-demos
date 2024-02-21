using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NeonShooter.Core.Graphics
{
	public class ParticleManager<T>
	{
		public class Particle
		{
			public Texture2D Texture;
			public Vector2 Position;
			public float Orientation;
			public Vector2 Scale = Vector2.One;
			public Color Color;
			public float Duration;
			public float PercentLife = 1f;
			public T State;
		}
	}
}
