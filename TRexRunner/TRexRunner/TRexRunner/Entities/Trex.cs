using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using TRexRunner.Graphics;

namespace TRexRunner.Entities
{
	public class Trex : IGameEntity
	{
		private const int TREX_IDLE_BACKGROUND_POS_X = 40;
		private const int TREX_IDLE_BACKGROUND_POS_Y = 0;

		public const int TREX_DEFAULT_POS_X = 848;
		public const int TREX_DEFAULT_POS_Y = 0;
		public const int TREX_DEFAULT_WIDTH = 44;
		public const int TREX_DEFAULT_HEIGHT = 52;
		private const float BLINK_ANIMATION_RANDOM_MIN = 2f;
		private const float BLINK_ANIMATION_RANDOM_MAX = 10f;
		private const float BLINK_ANIMATION_EYE_CLOSE_TIME = 0.5f;
		private Sprite _idleBackgroundSprite;

		private Sprite _idleSprite;
		private Sprite _idleBlinkSprite;
		private SpriteAnimation _blinkAnimation;

		private SoundEffect _jumpSound;

		public Vector2 Position { get; set; }
		public TrexState State { get; set; }
		public bool IsAlive { get; private set; }
		public float Speed { get; set; }
		public int DrawOrder { get; set; }

		private Random _random;

		public Trex(Texture2D spriteSheet, Vector2 position, SoundEffect jumpSound)
		{

			Position = position;
			_idleBackgroundSprite = new Sprite(spriteSheet, TREX_IDLE_BACKGROUND_POS_X, TREX_IDLE_BACKGROUND_POS_Y, TREX_DEFAULT_WIDTH, TREX_DEFAULT_HEIGHT);
			State = TrexState.Idle;

			_jumpSound = jumpSound;

			_random = new Random();

			_idleSprite = new Sprite(spriteSheet, TREX_DEFAULT_POS_X, TREX_DEFAULT_POS_Y, TREX_DEFAULT_WIDTH, TREX_DEFAULT_HEIGHT);
			_idleBlinkSprite = new Sprite(spriteSheet, TREX_DEFAULT_POS_X + TREX_DEFAULT_WIDTH, TREX_DEFAULT_POS_Y, TREX_DEFAULT_WIDTH, TREX_DEFAULT_HEIGHT);

			_blinkAnimation = new SpriteAnimation();
			CreateBlinkAnimation();
			_blinkAnimation.Play();

			
		}

		public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			if (State == TrexState.Idle)
			{
				_idleBackgroundSprite.Draw(spriteBatch, Position);
				_blinkAnimation.Draw(spriteBatch, Position);
			}
		}

		public void Update(GameTime gameTime)
		{
			if (State == TrexState.Idle)
			{
				if (!_blinkAnimation.IsPlaying)
				{
					CreateBlinkAnimation();
					_blinkAnimation.Play();
				}

				_blinkAnimation.Update(gameTime);
			}
		}

		private void CreateBlinkAnimation()
		{
			_blinkAnimation.Clear();
			_blinkAnimation.ShouldLoop = false;

			double blinkTimeStamp = BLINK_ANIMATION_RANDOM_MIN + _random.NextDouble() * (BLINK_ANIMATION_RANDOM_MAX - BLINK_ANIMATION_RANDOM_MIN);

			_blinkAnimation.AddFrame(_idleSprite, 0);
			_blinkAnimation.AddFrame(_idleBlinkSprite, (float)blinkTimeStamp);
			_blinkAnimation.AddFrame(_idleSprite, (float)blinkTimeStamp + BLINK_ANIMATION_EYE_CLOSE_TIME);
		}

		public bool BeginJump()
		{
			if (State == TrexState.Jumping || State == TrexState.Falling)
			{
				return false;
			}

			_jumpSound.Play();

			return true;
		}

		internal bool ContinueJump()
		{
			return true;
		}
	}
}
