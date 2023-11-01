using Chopper.Engine.Input;
using Chopper.Engine.States;
using Chopper.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using System;

namespace Chopper.States.Dev
{
	public class TestCameraState : BaseGameState
	{
		private const string PlayerFighter = "Sprites/Animations/FighterSpriteSheet";
		private const string PlayerAnimationTurnLeft = "Sprites/Animations/turn_left";
		private const string PlayerAnimationTurnRight = "Sprites/Animations/turn_right";
		private PlayerSprite _playerSprite;

		private OrthographicCamera _camera;

		private const float CAMERA_SPEED = 10.0f;

		public override void LoadContent()
		{
			var viewportAdapter = new DefaultViewportAdapter(_graphicsDevice);
			_camera = new OrthographicCamera(viewportAdapter);
			_playerSprite = new PlayerSprite(LoadTexture(PlayerFighter), 
				LoadAnimation(PlayerAnimationTurnLeft), LoadAnimation(PlayerAnimationTurnRight));
			_playerSprite.Position = new Vector2(0, 0);
		}

		public override void Render(SpriteBatch spriteBatch)
		{
			var transformMatrix = _camera.GetViewMatrix();

			spriteBatch.Begin(transformMatrix: transformMatrix);
				_playerSprite.Render(spriteBatch);
			spriteBatch.End();			
		}

		public override void HandleInput(GameTime gameTime)
		{
			InputManager.GetCommands(cmd =>
			{
				if (cmd is DevInputCommand.DevQuit)
				{
					NotifyEvent(new BaseGameStateEvent.GameQuit());
				}

				if (cmd is DevInputCommand.DevCamLeft)
				{
					_camera.Position += new Vector2(-10.0f, 0);
				}

				if (cmd is DevInputCommand.DevCamRight)
				{
					_camera.Position += new Vector2(10.0f, 0);
				}

				if (cmd is DevInputCommand.DevCamUp)
				{
					_camera.Position += new Vector2(0, -10.0f);
				}

				if (cmd is DevInputCommand.DevCamDown)
				{
					_camera.Position += new Vector2(0, 10.0f);
				}

				if (cmd is DevInputCommand.DevCamRotateLeft)
				{
					_camera.Rotate(-0.05f);
				}

				if (cmd is DevInputCommand.DevCamRotateRight)
				{
					_camera.Rotate(0.05f);
				}

				if (cmd is DevInputCommand.DevPlayerUp)
				{
					_playerSprite.MoveUp();
				}

				if (cmd is DevInputCommand.DevPlayerDown)
				{
					_playerSprite.MoveDown();
				}

				if (cmd is DevInputCommand.DevPlayerRight)
				{
					_playerSprite.MoveRight();
				}

				if (cmd is DevInputCommand.DevPlayerLeft)
				{
					_playerSprite.MoveLeft();
				}

				if (cmd is DevInputCommand.DevPlayerStopsMovingHorizontal)
				{
					_playerSprite.StopMoving();
				}

				if (cmd is DevInputCommand.DevPlayerStopsMovingVertical)
				{
					_playerSprite.StopVerticalMoving();
				}

				KeepPlayerInBounds();

				if (cmd is DevInputCommand.DevShoot)
				{
				}
			});
		}

		private void KeepPlayerInBounds()
		{
			if (_playerSprite.Position.X < _camera.BoundingRectangle.Left)
			{
				_playerSprite.Position = new Vector2(0, _playerSprite.Position.Y);
			}

			if (_playerSprite.Position.X + _playerSprite.Width > _camera.BoundingRectangle.Right)
			{
				_playerSprite.Position = new Vector2(_camera.BoundingRectangle.Right - _playerSprite.Width, _playerSprite.Position.Y);
			}

			if (_playerSprite.Position.Y < _camera.BoundingRectangle.Top)
			{
				_playerSprite.Position = new Vector2(_playerSprite.Position.X, _camera.BoundingRectangle.Top);
			}

			if (_playerSprite.Position.Y + _playerSprite.Height > _camera.BoundingRectangle.Bottom)
			{
				_playerSprite.Position = new Vector2(_playerSprite.Position.X, _camera.BoundingRectangle.Bottom - _playerSprite.Height);
			}
		}

		public override void UpdateGameState(GameTime gameTime)
		{
			_playerSprite.Update(gameTime);
			_camera.Position += new Vector2(0, -CAMERA_SPEED);
		}

		protected override void SetInputManager()
		{
			InputManager = new InputManager(new DevInputMapper());
		}
	}
}
