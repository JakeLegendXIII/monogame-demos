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

		// Represents a circular array with an arbitrary starting point. It's useful for efficiently overwriting
		// the oldest particles when the array gets full. Simply overwrite particleList[0] and advance Start.
		private class CircularParticleArray
		{
			private int start;
			public int Start
			{
				get { return start; }
				set { start = value % list.Length; }
			}

			public int Count { get; set; }
			public int Capacity { get { return list.Length; } }
			private Particle[] list;

			public CircularParticleArray() { }  // for serialization

			public CircularParticleArray(int capacity)
			{
				list = new Particle[capacity];
			}

			public Particle this[int i]
			{
				get { return list[(start + i) % list.Length]; }
				set { list[(start + i) % list.Length] = value; }
			}
		}
	}
}
