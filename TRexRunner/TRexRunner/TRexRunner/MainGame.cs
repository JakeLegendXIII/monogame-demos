using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using TRexRunner.Entities;
using TRexRunner.Graphics;
using TRexRunner.System;

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
		private const float FADE_IN_ANIMATION_SPEED = 820f;
		private const int SCORE_BOARD_POS_X = WINDOW_WIDTH - 130;
		private const int SCORE_BOARD_POS_Y = 10;
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;

		private SoundEffect _sfxHit;
		private SoundEffect _sfxScoreReached;
		private SoundEffect _sfxButtonPress;

		private Texture2D _spriteSheet;
		private Texture2D _fadeIntexture;

		private float _fadeIntexturePositionX;

		private Trex _trex;
		private ScoreBoard _scoreBoard;

		private InputController _inputController;
		private EntityManager _entityManager;
		private GroundManager _groundManager;
		private ObstacleManager _obstacleManager;
		private GameOverScreen _gameOverScreen;

		private KeyboardState _previousKeyboardState;

		public GameState State { get; private set; }

		public MainGame()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
			_entityManager = new EntityManager();
			State = GameState.Initial;
			_fadeIntexturePositionX = Trex.TREX_DEFAULT_WIDTH;
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

			_fadeIntexture = new Texture2D(GraphicsDevice, 1, 1);
			_fadeIntexture.SetData(new Color[] { Color.White });

			_sfxHit = Content.Load<SoundEffect>(SFX_HIT);
			_sfxScoreReached = Content.Load<SoundEffect>(SFX_SCORE_REACHED);
			_sfxButtonPress = Content.Load<SoundEffect>(SFX_BUTTON_PRESS);

			_trex = new Trex(_spriteSheet, new Vector2(TREX_START_POS_X, TREX_START_POS_Y - Trex.TREX_DEFAULT_HEIGHT), _sfxButtonPress);			
			_trex.DrawOrder = 10;
			_trex.JumpComplete += trex_JumpComplete;
			_trex.Died += trex_Died;

			_scoreBoard = new ScoreBoard(_spriteSheet, new Vector2(SCORE_BOARD_POS_X, SCORE_BOARD_POS_Y), _trex);
			//_scoreBoard.Score = 498;
			//_scoreBoard.HighScore = 12345;

			_inputController = new InputController(_trex);

			_groundManager = new GroundManager(_spriteSheet, _entityManager, _trex);

			_obstacleManager = new ObstacleManager(_entityManager, _trex, _scoreBoard, _spriteSheet);

			_gameOverScreen = new GameOverScreen(_spriteSheet, this);
			_gameOverScreen.Position = new Vector2(WINDOW_WIDTH / 2 - GameOverScreen.GAME_OVER_SPRITE_WIDTH / 2, WINDOW_HEIGHT / 2 - 30);

			_entityManager.AddEntity(_trex);
			_entityManager.AddEntity(_groundManager);
			_entityManager.AddEntity(_scoreBoard);
			_entityManager.AddEntity(_obstacleManager);
			_entityManager.AddEntity(_gameOverScreen);

			_groundManager.Initialize();
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			KeyboardState currentKeyboardState = Keyboard.GetState();

			base.Update(gameTime);

			if (State == GameState.Playing)
			{
				_inputController.ProcessControls(gameTime);
			}
			else if (State == GameState.Transtion)
			{
				_fadeIntexturePositionX += (float)gameTime.ElapsedGameTime.TotalSeconds * FADE_IN_ANIMATION_SPEED;
			}
			else if (State == GameState.Initial)
			{
				bool isStartKeyPressed = currentKeyboardState.IsKeyDown(Keys.Up) || currentKeyboardState.IsKeyDown(Keys.Space);
				bool wasStartKeyPressed = _previousKeyboardState.IsKeyDown(Keys.Up) || _previousKeyboardState.IsKeyDown(Keys.Space);

				if (isStartKeyPressed && !wasStartKeyPressed)
				{
					StartGame();
				}
			}

			_entityManager.Update(gameTime);
			_previousKeyboardState = currentKeyboardState;
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.White);

			_spriteBatch.Begin();

			_entityManager.Draw(_spriteBatch, gameTime);	
			
			if (State == GameState.Initial || State == GameState.Transtion)
			{
				_spriteBatch.Draw(_fadeIntexture, new Rectangle((int)Math.Round(_fadeIntexturePositionX), 0, WINDOW_WIDTH, WINDOW_HEIGHT), Color.White);
			}

			_spriteBatch.End();

			base.Draw(gameTime);
		}

		private bool StartGame()
		{
			if (State != GameState.Initial)
			{
				return false;
			}

			State = GameState.Transtion;
			_trex.BeginJump();

			return true;
		}

		private void trex_JumpComplete(object sender, EventArgs e)
		{
			if (State == GameState.Transtion)
			{
				State = GameState.Playing;

				_trex.Initialize();

				_obstacleManager.IsEnabled = true;
			}
		}

		private void trex_Died(object sender, EventArgs e)
		{
			State = GameState.GameOver;
			_obstacleManager.IsEnabled = false;
			_gameOverScreen.IsEnabled = true;
			_sfxHit.Play();
		}

		public bool Reset()
		{
			if (State != GameState.GameOver)
			{
				return false;
			}
			State = GameState.Playing;
			_trex.Initialize();

			_obstacleManager.Reset();
			_obstacleManager.IsEnabled = true;

			_gameOverScreen.IsEnabled = false;
			_scoreBoard.Score = 0;

			_groundManager.Initialize();

			_inputController.BlockInputTemporarily();

			return true;
		}
	}
}