using Microsoft.Xna.Framework;
using System;

namespace NeonShooter.Core.Utils
{
	static class Extensions
	{
		public static float ToAngle(this Vector2 vector)
		{
			return (float)Math.Atan2(vector.Y, vector.X);
		}
	}
}
