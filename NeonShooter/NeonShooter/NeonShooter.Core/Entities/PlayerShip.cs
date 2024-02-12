using NeonShooter.Core.Graphics;

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
			// ship logic goes here 
		}
	}
}
