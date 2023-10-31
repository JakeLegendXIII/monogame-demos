using Chopper.Engine.States;
using Chopper.Objects;
using Microsoft.Xna.Framework;
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

		public override void LoadContent()
		{
			var viewportAdapter = new DefaultViewportAdapter(_graphicsDevice);
			_camera = new OrthographicCamera(viewportAdapter);
		}

		public override void HandleInput(GameTime gameTime)
		{
			throw new System.NotImplementedException();
		}

		public override void UpdateGameState(GameTime gameTime)
		{
			throw new System.NotImplementedException();
		}

		protected override void SetInputManager()
		{
			throw new System.NotImplementedException();
		}
	}
}
