using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

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
		public void AddFrame(Sprite sprite, float timeStamp)
		{
			_frames.Add(new SpriteAnimationFrame(sprite, timeStamp));
		}

		public void Update(GameTime gameTime)
		{

		}

		public void Play()
		{
			IsPlaying = true;
		}

		public void Stop()
		{
			IsPlaying = false;
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
