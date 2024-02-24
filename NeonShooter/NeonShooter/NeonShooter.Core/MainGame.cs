using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using NeonShooter.Core.Entities;
using NeonShooter.Core.Graphics;
using NeonShooter.Core.Input;
using NeonShooter.Core.Sound;
using System;
using BloomPostprocess;

namespace NeonShooter.Core
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class MainGame : Game
	{
		public static MainGame Instance { get; private set; }
		public static Viewport Viewport { get { return Instance.GraphicsDevice.Viewport; } }
		public static Vector2 ScreenSize { get { return new Vector2(Viewport.Width, Viewport.Height); } }
		public static GameTime GameTime { get; private set; }
		public static ParticleManager<ParticleState> ParticleManager { get; private set; }

		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
		BloomComponent bloom;

		bool useBloom = true;

		public MainGame()
		{
			Instance = this;

			_graphics = new GraphicsDeviceManager(this);
			_graphics.PreferredBackBufferWidth = 1920;
			_graphics.PreferredBackBufferHeight = 1080;

			IsMouseVisible = true;

			bloom = new BloomComponent(this);
					Components.Add(bloom);
					bloom.Settings = new BloomSettings(null, 0.25f, 4, 2, 1, 1.5f, 1);
			bloom.Visible = false;
		}

		protected override void Initialize()
		{
			this.Content.RootDirectory = "Content";

			ParticleManager = new ParticleManager<ParticleState>(1024 * 20, ParticleState.UpdateParticle);

			base.Initialize();

			EntityManager.Add(PlayerShip.Instance);

			MediaPlayer.IsRepeating = true;
			MediaPlayer.Play(Sound.SoundManager.Music);
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			Art.Load(Content);
			Sound.SoundManager.Load(Content);

			EntityManager.Add(PlayerShip.Instance);
		}

		protected override void Update(GameTime gameTime)
		{		
			GameTime = gameTime;

			InputManager.Update();

			if (InputManager.WasButtonPressed(Buttons.Back) || InputManager.WasKeyPressed(Keys.Escape))
				this.Exit();

			PlayerStatus.Update();
			EntityManager.Update();
			EnemySpawner.Update();
			ParticleManager.Update();

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			bloom.BeginDraw();
			if (!useBloom)
				base.Draw(gameTime);

			GraphicsDevice.Clear(Color.Black);

			_spriteBatch.Begin(SpriteSortMode.Texture, BlendState.Additive);
			EntityManager.Draw(_spriteBatch);
			_spriteBatch.End();

			_spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive);
			// Grid.Draw(_spriteBatch);
			ParticleManager.Draw(_spriteBatch);
			_spriteBatch.End();

			if (useBloom)
				base.Draw(gameTime);

			// Draw the user interface without bloom
			_spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive);

			DrawTitleSafeAlignedString("Lives: " + PlayerStatus.Lives, 5);
			DrawTitleSafeRightAlignedString("Score: " + PlayerStatus.Score, 5);
			DrawTitleSafeRightAlignedString("Multiplier: " + PlayerStatus.Multiplier, 35);
			// draw the custom mouse cursor
			_spriteBatch.Draw(Art.Pointer, InputManager.MousePosition, Color.White);

			if (PlayerStatus.IsGameOver)
			{
				string text = "Game Over\n" +
					"Your Score: " + PlayerStatus.Score + "\n" +
					"High Score: " + PlayerStatus.HighScore;

				Vector2 textSize = Art.Font.MeasureString(text);
				_spriteBatch.DrawString(Art.Font, text, ScreenSize / 2 - textSize / 2, Color.White);
			}

			_spriteBatch.End();
		}

		private void DrawRightAlignedString(string text, float y)
		{
			var textWidth = Art.Font.MeasureString(text).X;
			_spriteBatch.DrawString(Art.Font, text, new Vector2(ScreenSize.X - textWidth - 5, y), Color.White);
		}

		private void DrawTitleSafeAlignedString(string text, int pos)
		{
			_spriteBatch.DrawString(Art.Font, text, new Vector2(Viewport.TitleSafeArea.X + pos), Color.White);
		}

		private void DrawTitleSafeRightAlignedString(string text, float y)
		{
			var textWidth = Art.Font.MeasureString(text).X;
			_spriteBatch.DrawString(Art.Font, text, new Vector2(ScreenSize.X - textWidth - 5 - Viewport.TitleSafeArea.X, Viewport.TitleSafeArea.Y + y), Color.White);
		}
	}
}
