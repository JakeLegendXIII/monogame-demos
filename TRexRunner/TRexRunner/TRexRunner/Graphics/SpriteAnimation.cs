﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TRexRunner.Graphics
{
	public class SpriteAnimation
	{
		private List<SpriteAnimationFrame> _frames = new List<SpriteAnimationFrame>();

		public SpriteAnimationFrame this[int index]
		{
			get => GetFrame(index);
		}

		public bool IsPlaying { get; private set; }
		public float PlaybackProgress { get; private set; }

		public SpriteAnimationFrame CurrentFrame 
		{ 
			get
			{
				return _frames
					.Where(f => f.TimeStamp <= PlaybackProgress)
					.OrderBy(f => f.TimeStamp).LastOrDefault();
			}
		}

		public float Duration => _frames.Count > 0 ? _frames.Max(f => f.TimeStamp) : 0;

		public void AddFrame(Sprite sprite, float timeStamp)
		{
			_frames.Add(new SpriteAnimationFrame(sprite, timeStamp));
		}

		public void Update(GameTime gameTime)
		{
			if (IsPlaying)
			{
				PlaybackProgress += (float)gameTime.ElapsedGameTime.TotalSeconds;

				if (PlaybackProgress > Duration)
				{
					PlaybackProgress -= Duration;
				}
			}
		}

		public void Draw(SpriteBatch spriteBatch, Vector2 position)
		{
			SpriteAnimationFrame frame = CurrentFrame;

			if (frame != null)
			{
				frame.Sprite.Draw(spriteBatch, position);
			}

			// Cool but uses new C# features
			// CurrentFrame?.Sprite.Draw(spriteBatch, position);
		}

		public void Play()
		{
			IsPlaying = true;
		}

		public void Stop()
		{
			IsPlaying = false;
			PlaybackProgress = 0;
		}

		public SpriteAnimationFrame GetFrame(int index)
		{
			if(index < 0 || index >= _frames.Count)
			{
				throw new ArgumentOutOfRangeException(nameof(index), "The index must be within the bounds of the frames list.");
			}
			
			return _frames[index];
		}
	}
}