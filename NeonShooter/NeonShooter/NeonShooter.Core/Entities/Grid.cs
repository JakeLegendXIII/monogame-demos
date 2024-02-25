using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace NeonShooter.Core.Entities
{
	public class Grid
	{
		private class PointMass
		{
			public Vector3 Position;
			public Vector3 Velocity;
			public float InverseMass;
			private Vector3 acceleration;
			private float damping = 0.98f;

			public PointMass(Vector3 position, float invMass)
			{
				Position = position;
				InverseMass = invMass;
			}

			public void ApplyForce(Vector3 force)
			{
				acceleration += force * InverseMass;
			}

			public void IncreaseDamping(float factor)
			{
				damping *= factor;
			}

			public void Update()
			{
				Velocity += acceleration;
				Position += Velocity;
				acceleration = Vector3.Zero;
				if (Velocity.LengthSquared() < 0.001f * 0.001f)
					Velocity = Vector3.Zero;
				Velocity *= damping;
				damping = 0.98f;
			}
		}

		private struct Spring
		{
			public PointMass End1;
			public PointMass End2;
			public float TargetLength;
			public float Stiffness;
			public float Damping;

			public Spring(PointMass end1, PointMass end2, float stiffness, float damping)
			{
				End1 = end1;
				End2 = end2;
				Stiffness = stiffness;
				Damping = damping;
				TargetLength = Vector3.Distance(end1.Position, end2.Position) * 0.95f;
			}

			public void Update()
			{
				var x = End1.Position - End2.Position;
				float length = x.Length();
				// these springs can only pull, not push 
				if (length <= TargetLength)
					return;
				x = (x / length) * (length - TargetLength);
				var dv = End2.Velocity - End1.Velocity;
				var force = Stiffness * x - dv * Damping;
				End1.ApplyForce(-force);
				End2.ApplyForce(force);
			}
		}

		Spring[] springs;
		PointMass[,] points;
		Vector2 screenSize;

		public Grid(Rectangle size, Vector2 spacing)
		{
			var springList = new List<Spring>();

			int numColumns = (int)(size.Width / spacing.X) + 1;
			int numRows = (int)(size.Height / spacing.Y) + 1;
			points = new PointMass[numColumns, numRows];

			// these fixed points will be used to anchor the grid to fixed positions on the screen
			PointMass[,] fixedPoints = new PointMass[numColumns, numRows];

			// create the point masses
			int column = 0, row = 0;
			for (float y = size.Top; y <= size.Bottom; y += spacing.Y)
			{
				for (float x = size.Left; x <= size.Right; x += spacing.X)
				{
					points[column, row] = new PointMass(new Vector3(x, y, 0), 1);
					fixedPoints[column, row] = new PointMass(new Vector3(x, y, 0), 0);
					column++;
				}
				row++;
				column = 0;
			}

			// link the point masses with springs
			for (int y = 0; y < numRows; y++)
				for (int x = 0; x < numColumns; x++)
				{
					if (x == 0 || y == 0 || x == numColumns - 1 || y == numRows - 1)    // anchor the border of the grid
						springList.Add(new Spring(fixedPoints[x, y], points[x, y], 0.1f, 0.1f));
					else if (x % 3 == 0 && y % 3 == 0)                                  // loosely anchor 1/9th of the point masses
						springList.Add(new Spring(fixedPoints[x, y], points[x, y], 0.002f, 0.02f));

					const float stiffness = 0.28f;
					const float damping = 0.06f;

					if (x > 0)
						springList.Add(new Spring(points[x - 1, y], points[x, y], stiffness, damping));
					if (y > 0)
						springList.Add(new Spring(points[x, y - 1], points[x, y], stiffness, damping));
				}

			springs = springList.ToArray();
		}

		public void Update()
		{
			foreach (var spring in springs)
				spring.Update();

			foreach (var mass in points)
				mass.Update();
		}

	}
}
