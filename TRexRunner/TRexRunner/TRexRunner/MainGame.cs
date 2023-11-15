using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TRexRunner.Entities;
using TRexRunner.Graphics;

namespace TRexRunner
{
	public class MainGame : Game
	{
		private const string SPRITE_SHEET = "Sprites/TrexSpritesheet";
		private const string SFX_HIT = "Audio/hit";
		private const string SFX_SCORE_REACHED = "Audio/score-reached";
		private const string SFX_BUTTON_PRESS = "Audio/button-press";
		public const int WINDOW_WIDTH = 600;
		public const int WINDOW_HEIGHT = 150;
		public const int TREX_START_POS_Y = WINDOW_HEIGHT - 16;
		public const int TREX_START_POS_X = 1;

		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;

		private SoundEffect _sfxHit;
		private SoundEffect _sfxScoreReached;
		private SoundEffect _sfxButtonPress;

		private Texture2D _spriteSheet;

		private Trex _trex;		

		public MainGame()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		protected override void Initialize()
		{			
			base.Initialize();

			_graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
			_graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
			_graphics.ApplyChanges();
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			_spriteSheet = Content.Load<Texture2D>(SPRITE_SHEET);

			_sfxHit = Content.Load<SoundEffect>(SFX_HIT);
			_sfxScoreReached = Content.Load<SoundEffect>(SFX_SCORE_REACHED);
			_sfxButtonPress = Content.Load<SoundEffect>(SFX_BUTTON_PRESS);

			_trex = new Trex(_spriteSheet, new Vector2(TREX_START_POS_X, TREX_START_POS_Y - Trex.TREX_DEFAULT_HEIGHT));			
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			// TODO: Add your update logic here

			base.Update(gameTime);

			_trex.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.White);

			_spriteBatch.Begin();

			_trex.Draw(_spriteBatch, gameTime);

			_spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}