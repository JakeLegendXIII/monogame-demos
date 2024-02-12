using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace NeonShooter.Core.Graphics
{
	static class Art
	{
		public static Texture2D Player { get; private set; }
		public static Texture2D Seeker { get; private set; }
		public static Texture2D Wanderer { get; private set; }
		public static Texture2D Bullet { get; private set; }
		public static Texture2D Pointer { get; private set; }

		public static Texture2D Pixel { get; private set; }     // a single white pixel

		public static void Load(ContentManager content)
		{
			Player = content.Load<Texture2D>("Player");
			Seeker = content.Load<Texture2D>("Seeker");
			Wanderer = content.Load<Texture2D>("Wanderer");
			Bullet = content.Load<Texture2D>("Bullet");
			Pointer = content.Load<Texture2D>("Pointer");

			Pixel = new Texture2D(Player.GraphicsDevice, 1, 1);
			Pixel.SetData(new[] { Color.White });
		}
	}
}
