using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TRexRunner.Graphics;

namespace TRexRunner
{
	public class MainGame : Game
	{
		private const string SPRITE_SHEET = "Sprites/TrexSpritesheet";
		private const string SFX_HIT = "Audio/hit";
		private const string SFX_SCORE_REACHED = "Audio/score-reached";
		private const string SFX_BUTTON_PRESS = "Audio/button-press";

		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;

		private SoundEffect _sfxHit;
		private SoundEffect _sfxScoreReached;
		private SoundEffect _sfxButtonPress;

		private Texture2D _spriteSheet;

		public MainGame()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		protected override void Initialize()
		{
			// TODO: Add your initialization logic here

			base.Initialize();
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			_spriteSheet = Content.Load<Texture2D>(SPRITE_SHEET);

			_sfxHit = Content.Load<SoundEffect>(SFX_HIT);
			_sfxScoreReached = Content.Load<SoundEffect>(SFX_SCORE_REACHED);
			_sfxButtonPress = Content.Load<SoundEffect>(SFX_BUTTON_PRESS);
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			// TODO: Add your update logic here

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			_spriteBatch.Begin();

			Sprite trexSprite = new Sprite(_spriteSheet, 848, 0, 44, 52);

			trexSprite.Draw(_spriteBatch, new Vector2(25, 25));

			_spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}