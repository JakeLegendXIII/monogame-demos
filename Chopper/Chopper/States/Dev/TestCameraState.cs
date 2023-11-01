using Chopper.Engine.States;
using Chopper.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

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
			throw new System.NotImplementedException();
		}

		public override void UpdateGameState(GameTime gameTime)
		{
			_playerSprite.Update(gameTime);
			_camera.Position += new Vector2(0, -CAMERA_SPEED);
		}

		protected override void SetInputManager()
		{
			throw new System.NotImplementedException();
		}
	}
}
