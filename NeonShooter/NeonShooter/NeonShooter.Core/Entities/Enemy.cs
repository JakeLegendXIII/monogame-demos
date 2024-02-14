using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NeonShooter.Core.Graphics;
using NeonShooter.Core.Utils;
using System.Collections.Generic;

namespace NeonShooter.Core.Entities
{
	class Enemy : Entity
	{
		private List<IEnumerator<int>> behaviours = new List<IEnumerator<int>>();
		private int timeUntilStart = 60;
		public bool IsActive { get { return timeUntilStart <= 0; } }
		public int PointValue { get; private set; }

		public Enemy(Texture2D image, Vector2 position)
		{
			this.image = image;
			Position = position;
			Radius = image.Width / 2f;
			color = Color.Transparent;
		}

		public override void Update()
		{
			if (timeUntilStart <= 0)
			{
				ApplyBehaviours();
			}
			else
			{
				timeUntilStart--;
				color = Color.White * (1 - timeUntilStart / 60f);
			}
			Position += Velocity;
			Position = Vector2.Clamp(Position, Size / 2, MainGame.ScreenSize - Size / 2);
			Velocity *= 0.8f;
		}

		public static Enemy CreateSeeker(Vector2 position)
		{
			var enemy = new Enemy(Art.Seeker, position);
			enemy.AddBehaviour(enemy.FollowPlayer(0.9f));
			enemy.PointValue = 2;

			return enemy;
		}

		private void AddBehaviour(IEnumerable<int> behaviour)
		{
			behaviours.Add(behaviour.GetEnumerator());
		}

		private void ApplyBehaviours()
		{
			for (int i = 0; i < behaviours.Count; i++)
			{
				if (!behaviours[i].MoveNext())
					behaviours.RemoveAt(i--);
			}
		}

		public void WasShot()
		{
			IsExpired = true;
		}

		#region Enemy behaviours
		IEnumerable<int> FollowPlayer(float acceleration = 1f)
		{
			while (true)
			{
				Velocity += (PlayerShip.Instance.Position - Position).ScaleTo(acceleration);
				if (Velocity != Vector2.Zero)
					Orientation = Velocity.ToAngle();
				yield return 0;
			}
		}

		// Square pattern example
		IEnumerable<int> MoveInASquare()
		{
			const int framesPerSide = 30;
			while (true)
			{
				// move right for 30 frames 
				for (int i = 0; i < framesPerSide; i++)
				{
					Velocity = Vector2.UnitX;
					yield return 0;
				}
				// move down 
				for (int i = 0; i < framesPerSide; i++)
				{
					Velocity = Vector2.UnitY;
					yield return 0;
				}
				// move left 
				for (int i = 0; i < framesPerSide; i++)
				{
					Velocity = -Vector2.UnitX;
					yield return 0;
				}
				// move up 
				for (int i = 0; i < framesPerSide; i++)
				{
					Velocity = -Vector2.UnitY;
					yield return 0;
				}
			}
		}

		#endregion
	}
}
