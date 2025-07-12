using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;

namespace DungeonSlime.GameObjects;

public class Slime
{
	// A constant value that represents the amount of time to wait between
	// movement updates.
	private static readonly TimeSpan s_movementTime = TimeSpan.FromMilliseconds(200);

	// The amount of time that has elapsed since the last movement update.
	private TimeSpan _movementTimer;

	// Normalized value (0-1) representing progress between movement ticks for visual interpolation
	private float _movementProgress;

	// The next direction to apply to the head of the slime chain during the
	// next movement update.
	private Vector2 _nextDirection;

	// The number of pixels to move the head segment during the movement cycle.
	private float _stride;

	// Tracks the segments of the slime chain.
	private List<SlimeSegment> _segments;

	// The AnimatedSprite used when drawing each slime segment
	private AnimatedSprite _sprite;

	/// <summary>
	/// Event that is raised if it is detected that the head segment of the slime
	/// has collided with a body segment.
	/// </summary>
	public event EventHandler BodyCollision;

	/// <summary>
	/// Creates a new Slime using the specified animated sprite.
	/// </summary>
	/// <param name="sprite">The AnimatedSprite to use when drawing the slime.</param>
	public Slime(AnimatedSprite sprite)
	{
		_sprite = sprite;
	}

	/// <summary>
	/// Initializes the slime, can be used to reset it back to an initial state.
	/// </summary>
	/// <param name="startingPosition">The position the slime should start at.</param>
	/// <param name="stride">The total number of pixels to move the head segment during each movement cycle.</param>
	public void Initialize(Vector2 startingPosition, float stride)
	{
		// Initialize the segment collection.
		_segments = new List<SlimeSegment>();

		// Set the stride
		_stride = stride;

		// Create the initial head of the slime chain.
		SlimeSegment head = new SlimeSegment();
		head.At = startingPosition;
		head.To = startingPosition + new Vector2(_stride, 0);
		head.Direction = Vector2.UnitX;

		// Add it to the segment collection.
		_segments.Add(head);

		// Set the initial next direction as the same direction the head is
		// moving.
		_nextDirection = head.Direction;

		// Zero out the movement timer.
		_movementTimer = TimeSpan.Zero;
	}

	private void HandleInput()
	{
		Vector2 potentialNextDirection = _nextDirection;

		if (GameController.MoveUp())
		{
			potentialNextDirection = -Vector2.UnitY;
		}
		else if (GameController.MoveDown())
		{
			potentialNextDirection = Vector2.UnitY;
		}
		else if (GameController.MoveLeft())
		{
			potentialNextDirection = -Vector2.UnitX;
		}
		else if (GameController.MoveRight())
		{
			potentialNextDirection = Vector2.UnitX;
		}

		// Only allow direction change if it is not reversing the current
		// direction.  This prevents the slime from backing into itself.
		float dot = Vector2.Dot(potentialNextDirection, _segments[0].Direction);
		if (dot >= 0)
		{
			_nextDirection = potentialNextDirection;
		}
	}

	private void Move()
	{
		// Capture the value of the head segment
		SlimeSegment head = _segments[0];

		// Update the direction the head is supposed to move in to the
		// next direction cached.
		head.Direction = _nextDirection;

		// Update the head's "at" position to be where it was moving "to"
		head.At = head.To;

		// Update the head's "to" position to the next tile in the direction
		// it is moving.
		head.To = head.At + head.Direction * _stride;

		// Insert the new adjusted value for the head at the front of the
		// segments and remove the tail segment. This effectively moves
		// the entire chain forward without needing to loop through every
		// segment and update its "at" and "to" positions.
		_segments.Insert(0, head);
		_segments.RemoveAt(_segments.Count - 1);

		// Iterate through all of the segments except the head and check
		// if they are at the same position as the head. If they are, then
		// the head is colliding with a body segment and a body collision
		// has occurred.
		for (int i = 1; i < _segments.Count; i++)
		{
			SlimeSegment segment = _segments[i];

			if (head.At == segment.At)
			{
				if (BodyCollision != null)
				{
					BodyCollision.Invoke(this, EventArgs.Empty);
				}

				return;
			}
		}
	}

}
