using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NeonShooter.Core.Entities;
using NeonShooter.Core.Graphics;
using NeonShooter.Core.Input;

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

		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;

		public MainGame()
		{
			Instance = this;

			_graphics = new GraphicsDeviceManager(this);
			_graphics.PreferredBackBufferWidth = 1920;
			_graphics.PreferredBackBufferHeight = 1080;

			IsMouseVisible = true;
		}

		protected override void Initialize()
		{
			this.Content.RootDirectory = "Content";

			base.Initialize();

			EntityManager.Add(PlayerShip.Instance);
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			Art.Load(Content);

			EntityManager.Add(PlayerShip.Instance);

			// TODO: use this.Content to load your game content here
		}

		protected override void Update(GameTime gameTime)
		{			
			InputManager.Update();

			EntityManager.Update();
			EnemySpawner.Update();

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

			_spriteBatch.Begin(SpriteSortMode.Texture, BlendState.Additive);
			EntityManager.Draw(_spriteBatch);
			_spriteBatch.Draw(Art.Pointer, InputManager.MousePosition, Color.White);
			_spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
