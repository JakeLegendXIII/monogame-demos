using Microsoft.Xna.Framework;
using NeonShooter.Core.Graphics;
using NeonShooter.Core.Input;

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
		private PlayerShip()
		{
			image = Art.Player;
			Position = MainGame.ScreenSize / 2;
			Radius = 10;
		}
		public override void Update()
		{
			const float speed = 8;
			Velocity = speed * InputManager.GetMovementDirection();
			Position += Velocity;
			Position = Vector2.Clamp(Position, Size / 2, MainGame.ScreenSize - Size / 2);

			if (Velocity.LengthSquared() > 0)
				Orientation = Velocity.ToAngle();
		}
	}
}
