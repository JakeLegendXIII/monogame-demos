using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TRexRunner.Graphics;

namespace TRexRunner.Entities
{
	public class GameOverScreen : IGameEntity
	{
		private const int GAME_OVER_TEXTURE_POS_X = 655;
		private const int GAME_OVER_TEXTURE_POS_Y = 14;
		public const int GAME_OVER_SPRITE_WIDTH = 192;
		public const int GAME_OVER_SPRITE_HEIGHT = 14;

		private const int BUTTON_TEXTURE_POS_X = 1;
		private const int BUTTON_TEXTURE_POS_Y = 1;
		private const int BUTTON_SPRITE_WIDTH = 34;
		private const int BUTTON_SPRITE_HEIGHT = 38;

		private Sprite _textSprite;
		private Sprite _buttonSprite;
		private MainGame _mainGame;

		private KeyboardState _previousKeyboardState;

		public int DrawOrder => 100;

		public Vector2 Position { get; set; }

		public bool IsEnabled { get; set; }

		private Vector2 _buttonPosition => Position + new Vector2(GAME_OVER_SPRITE_WIDTH / 2 - BUTTON_SPRITE_WIDTH,
			GAME_OVER_SPRITE_HEIGHT + 20);

		private Rectangle ButtonBounds => new Rectangle(_buttonPosition.ToPoint(), new Point(BUTTON_SPRITE_WIDTH, BUTTON_SPRITE_HEIGHT));

		public GameOverScreen(Texture2D spriteSheet, MainGame mainGame)
		{
			_textSprite = new Sprite(spriteSheet, GAME_OVER_TEXTURE_POS_X, GAME_OVER_TEXTURE_POS_Y, GAME_OVER_SPRITE_WIDTH, GAME_OVER_SPRITE_HEIGHT);
			_buttonSprite = new Sprite(spriteSheet, BUTTON_TEXTURE_POS_X, BUTTON_TEXTURE_POS_Y, BUTTON_SPRITE_WIDTH, BUTTON_SPRITE_HEIGHT);
			_mainGame = mainGame;
		}

		public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			if (!IsEnabled) return;

			_textSprite.Draw(spriteBatch, Position);
			_buttonSprite.Draw(spriteBatch, _buttonPosition);
		}

		public void Update(GameTime gameTime)
		{
			if (!IsEnabled) return;

			MouseState mouseState = Mouse.GetState();
			KeyboardState keyboardState = Keyboard.GetState();

			bool isKeyPressed = keyboardState.IsKeyDown(Keys.Space) || keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.Enter);
			bool wasKeyPressed = _previousKeyboardState.IsKeyDown(Keys.Space) || _previousKeyboardState.IsKeyDown(Keys.Up) || _previousKeyboardState.IsKeyDown(Keys.Enter);

			if ((ButtonBounds.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed)
				|| (isKeyPressed && !wasKeyPressed))
			{
				_mainGame.Reset();
			}

			_previousKeyboardState = keyboardState;
		}
	}
}
