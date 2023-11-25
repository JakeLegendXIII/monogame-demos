﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using TRexRunner.Graphics;

namespace TRexRunner.Entities
{
	public class Trex : IGameEntity
	{
		private const float GRAVITY = 1600f;
		private const float JUMP_START_VELOCITY = -480f;
		private const float JUMP_CANCEL_VELOCITY = -100f;
		private const float MIN_JUMP_HEIGHT = 40f;

		private const int TREX_IDLE_BACKGROUND_POS_X = 40;
		private const int TREX_IDLE_BACKGROUND_POS_Y = 0;

		public const int TREX_DEFAULT_POS_X = 848;
		public const int TREX_DEFAULT_POS_Y = 0;
		public const int TREX_DEFAULT_WIDTH = 44;
		public const int TREX_DEFAULT_HEIGHT = 52;
		private const float BLINK_ANIMATION_RANDOM_MIN = 2f;
		private const float BLINK_ANIMATION_RANDOM_MAX = 10f;
		private const float BLINK_ANIMATION_EYE_CLOSE_TIME = 0.5f;

		private const int TREX_RUNNING_SPRITE_ONE_POS_X = TREX_DEFAULT_POS_X + TREX_DEFAULT_WIDTH * 2;
		private const int TREX_RUNNING_SPRITE_ONE_POS_Y = 0;
		private const float RUN_ANIMATION_FRAME_LENGTH = 0.1f;

		private const int TREX_DUCKING_SPRITE_WIDTH = 59;
		private const int TREX_DUCKING_SPRITE_ONE_POS_X = TREX_DEFAULT_POS_X + TREX_DEFAULT_WIDTH * 6;
		private const int TREX_DUCKING_SPRITE_ONE_POS_Y = 0;
		private const int TREX_DEAD_SPRITE_POS_X = 1068;
		private const int TREX_DEAD_SPRITE_POS_Y = 0;

		private const float DROP_VELOCITY = 600f;
		public const float START_SPEED = 280f;
		public const float MAX_SPEED = 900f;

		private const float ACCELERATION_PPS_PER_SECOND = 3f;
		private const int COLLISION_BOX_INSET = 3;
		private const int DUCK_COLLISION_REDUCTION = 20;

		private Sprite _idleBackgroundSprite;

		private Sprite _idleSprite;
		private Sprite _idleBlinkSprite;
		private Sprite _deadSprite;
		private SpriteAnimation _blinkAnimation;
		private SpriteAnimation _runAnimation;
		private SpriteAnimation _duckAnimation;

		private SoundEffect _jumpSound;

		private Random _random;
		private float _startPosY;
		private float _dropVelocity;
		private float _verticalVelocity;

		public event EventHandler JumpComplete;
		public event EventHandler Died;

		public Vector2 Position { get; set; }
		public TrexState State { get; set; }
		public bool IsAlive { get; private set; }
		public float Speed { get; private set; }
		public int DrawOrder { get; set; }

		public Rectangle CollisionBox
		{
			get
			{
				Rectangle box = new Rectangle(
					(int)Math.Round(Position.X),
					(int)Math.Round(Position.Y),
					TREX_DEFAULT_WIDTH,
					TREX_DEFAULT_HEIGHT
				);
				box.Inflate(-COLLISION_BOX_INSET, -COLLISION_BOX_INSET);

				if (State == TrexState.Ducking)
				{
					box.Y += DUCK_COLLISION_REDUCTION;
					box.Height -= DUCK_COLLISION_REDUCTION;

				}

				return box;
			}
		}


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

			_startPosY = position.Y;

			_runAnimation = new SpriteAnimation();
			_runAnimation.AddFrame(new Sprite(spriteSheet, TREX_RUNNING_SPRITE_ONE_POS_X, TREX_RUNNING_SPRITE_ONE_POS_Y, TREX_DEFAULT_WIDTH, TREX_DEFAULT_HEIGHT), 0);
			_runAnimation.AddFrame(new Sprite(spriteSheet, TREX_RUNNING_SPRITE_ONE_POS_X + TREX_DEFAULT_WIDTH, TREX_RUNNING_SPRITE_ONE_POS_Y, TREX_DEFAULT_WIDTH, TREX_DEFAULT_HEIGHT), RUN_ANIMATION_FRAME_LENGTH);
			_runAnimation.AddFrame(_runAnimation[0].Sprite, RUN_ANIMATION_FRAME_LENGTH * 2);
			_runAnimation.Play();

			_duckAnimation = new SpriteAnimation();
			_duckAnimation.AddFrame(new Sprite(spriteSheet, TREX_DUCKING_SPRITE_ONE_POS_X, TREX_DUCKING_SPRITE_ONE_POS_Y, TREX_DUCKING_SPRITE_WIDTH, TREX_DEFAULT_HEIGHT), 0);
			_duckAnimation.AddFrame(new Sprite(spriteSheet, TREX_DUCKING_SPRITE_ONE_POS_X + TREX_DUCKING_SPRITE_WIDTH, TREX_DUCKING_SPRITE_ONE_POS_Y, TREX_DUCKING_SPRITE_WIDTH, TREX_DEFAULT_HEIGHT), RUN_ANIMATION_FRAME_LENGTH);
			_duckAnimation.AddFrame(_duckAnimation[0].Sprite, RUN_ANIMATION_FRAME_LENGTH * 2);
			_duckAnimation.Play();

			_deadSprite = new Sprite(spriteSheet, TREX_DEAD_SPRITE_POS_X, TREX_DEAD_SPRITE_POS_Y, TREX_DEFAULT_WIDTH, TREX_DEFAULT_HEIGHT);

			IsAlive = true;
		}

		public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			if (IsAlive)
			{
				if (State == TrexState.Idle)
				{
					_idleBackgroundSprite.Draw(spriteBatch, Position);
					_blinkAnimation.Draw(spriteBatch, Position);
				}
				else if (State == TrexState.Jumping || State == TrexState.Falling)
				{
					_idleSprite.Draw(spriteBatch, Position);
				}
				else if (State == TrexState.Running)
				{
					_runAnimation.Draw(spriteBatch, Position);
				}
				else if (State == TrexState.Ducking)
				{
					_duckAnimation.Draw(spriteBatch, Position);
				}
			}
			else
			{
				_deadSprite.Draw(spriteBatch, Position);
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
			else if (State == TrexState.Jumping || State == TrexState.Falling)
			{				
				Position = new Vector2(Position.X, Position.Y + _verticalVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds + _dropVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
				_verticalVelocity += GRAVITY * (float)gameTime.ElapsedGameTime.TotalSeconds;

				if (_verticalVelocity >= 0)
				{
					State = TrexState.Falling;
				}					

				if (Position.Y >= _startPosY)
				{

					Position = new Vector2(Position.X, _startPosY);
					_verticalVelocity = 0;
					State = TrexState.Running;

					OnJumpComplete();
				}
			}
			else if (State == TrexState.Running)
			{
				_runAnimation.Update(gameTime);
			}
			else if (State == TrexState.Ducking)
			{
				_duckAnimation.Update(gameTime);
			}

			if (State != TrexState.Idle)
			{
				Speed += ACCELERATION_PPS_PER_SECOND * (float)gameTime.ElapsedGameTime.TotalSeconds;
			}				

			if (Speed > MAX_SPEED)
			{
				Speed = MAX_SPEED;
			}				

			_dropVelocity = 0;
		}

		public void Initialize()
		{

			IsAlive = true;
			State = TrexState.Running;
			Speed = START_SPEED;
			Position = new Vector2(Position.X, _startPosY);
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

			State = TrexState.Jumping;

			_verticalVelocity = JUMP_START_VELOCITY;

			return true;
		}

		public bool CancelJump()
		{
			if (State != TrexState.Jumping || (Position.Y - _startPosY) < MIN_JUMP_HEIGHT)
			{
				return false;
			}

			_verticalVelocity = _verticalVelocity < JUMP_CANCEL_VELOCITY ? JUMP_CANCEL_VELOCITY : 0;

			return true;
		}

		public bool Duck()
		{
			if (State == TrexState.Jumping || State == TrexState.Falling)
				return false;

			State = TrexState.Ducking;

			return true;

		}

		public bool GetUp()
		{
			if (State != TrexState.Ducking)
				return false;

			State = TrexState.Running;

			return true;
		}

		public bool Drop()
		{
			if (State != TrexState.Falling && State != TrexState.Jumping)
			{
				return false;
			}				

			State = TrexState.Falling;
			_dropVelocity = DROP_VELOCITY;

			return true;
		}

		internal bool ContinueJump()
		{
			return true;
		}

		protected virtual void OnJumpComplete()
		{
			JumpComplete?.Invoke(this, EventArgs.Empty);
		}

		protected virtual void OnDied()
		{			
			Died?.Invoke(this, EventArgs.Empty);
		}

		public bool Die()
		{
			if (!IsAlive)
				return false;

			State = TrexState.Idle;
			Speed = 0;

			IsAlive = false;

			OnDied();

			return true;
		}
	}
}
