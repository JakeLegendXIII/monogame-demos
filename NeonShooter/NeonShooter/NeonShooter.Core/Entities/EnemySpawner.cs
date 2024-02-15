using Microsoft.Xna.Framework;
using System;

namespace NeonShooter.Core.Entities
{
	static class EnemySpawner
	{
		static Random rand = new Random();
		static float inverseSpawnChance = 60;

		public static void Update()
		{
			if (!PlayerShip.Instance.IsDead && EntityManager.Count < 200)
			{
				if (rand.Next((int)inverseSpawnChance) == 0)
					EntityManager.Add(Enemy.CreateSeeker(GetSpawnPosition()));
				if (rand.Next((int)inverseSpawnChance) == 0)
					EntityManager.Add(Enemy.CreateWanderer(GetSpawnPosition()));
			}

			// slowly increase the spawn rate as time progresses 
			if (inverseSpawnChance > 20)
				inverseSpawnChance -= 0.005f;
		}

		private static Vector2 GetSpawnPosition()
		{
			Vector2 pos;
			do
			{
				pos = new Vector2(rand.Next((int)MainGame.ScreenSize.X), rand.Next((int)MainGame.ScreenSize.Y));
			}
			while (Vector2.DistanceSquared(pos, PlayerShip.Instance.Position) < 250 * 250);

			return pos;
		}

		public static void Reset()
		{
			inverseSpawnChance = 60;
		}
	}
}
