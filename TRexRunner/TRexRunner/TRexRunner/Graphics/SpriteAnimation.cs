using Microsoft.Xna.Framework;
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
		public bool ShouldLoop { get; set; } = true;

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
					if (ShouldLoop)
					{
						PlaybackProgress -= Duration;
					}
					else
					{
						Stop();
					}
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

		public void Clear()
		{

			Stop();
			_frames.Clear();
		}

		public static SpriteAnimation CreateSimpleAnimation(Texture2D texture, Point startPos, int width, int height, Point offset, int frameCount, float frameLength)
		{
			if (texture == null)
			{
				throw new ArgumentNullException(nameof(texture), "The texture must not be null.");
			}

			SpriteAnimation anim = new SpriteAnimation();

			for(var i = 0; i < frameCount; i++)
			{
				Sprite sprite = new Sprite(texture, startPos.X + i * offset.X, startPos.Y + i * offset.Y, width, height);
				anim.AddFrame(sprite, frameLength * i);

				if (i == frameCount - 1)
				{
					anim.AddFrame(sprite, frameLength * (i + 1));
				}
			}

			return anim;
		}
	}
}
