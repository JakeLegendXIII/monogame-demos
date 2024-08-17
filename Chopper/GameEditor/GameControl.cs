using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Forms.NET.Controls;
using MonoGame.Extended.ViewportAdapters;
using ChopperPipelineExtensions;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MonoGame.Extended.Graphics;

namespace GameEditor
{
	public class GameControl : MonoGameControl
	{
		public const string GROUND = "ground";
		public const string BUILDINGS = "buildings";
		public const string OBJECTS = "objects";
		public const string EVENTS = "events";

		public const int TILE_SIZE = 128;

		private int _mouseX;
		private int _mouseY;

		private OrthographicCamera _camera;
		private Texture2D _groundTexture;
		private Texture2D _buildingTexture;
		private Texture2D _backgroundRectangle;
		private Texture2D _eventRectangle;
		private bool _cameraDrag;


		public Dictionary<string, Texture2DAtlas> Atlas { get; private set; }

		public event EventHandler<EventArgs> OnInitialized;

		protected override void Initialize()
		{
			_backgroundRectangle = new Texture2D(Editor.GraphicsDevice, 1, 1);
			_backgroundRectangle.SetData(new[] { Microsoft.Xna.Framework.Color.CadetBlue });
			var viewportAdapter = new DefaultViewportAdapter(Editor.GraphicsDevice);
			_camera = new OrthographicCamera(viewportAdapter);
			ResetCameraPostion();

			// Load Atlas
			Atlas = new Dictionary<string, Texture2DAtlas>();
			var groundTiles = GetGroundTiles();
			//var buildingTiles = GetBuildingTiles();
			//var objectTiles = GetObjectTiles();			
			//var groundAtlas = new TextureAtlas(GROUND, _groundTexture, groundTiles);

			//Atlas.Add(GROUND, groundAtlas);

			OnInitialized(this, EventArgs.Empty);
		}	

		protected override void Update(GameTime gameTime)
		{
		}

		protected override void Draw()
		{
			var tower = Editor.Content.Load<Texture2D>(@"Sprites\Tower");
			Editor.spriteBatch.Begin();
			//Editor.spriteBatch.Draw(tower, new Microsoft.Xna.Framework.Rectangle(0, 0, tower.Width, tower.Height),
			//	Microsoft.Xna.Framework.Color.White);
			Editor.spriteBatch.Draw(_backgroundRectangle, new Microsoft.Xna.Framework.Rectangle(0, 0, ClientSize.Width, ClientSize.Height),
								Microsoft.Xna.Framework.Color.White);
			Editor.spriteBatch.End();
		}

		private void ResetCameraPostion()
		{
			_camera.Position = new Vector2(
			 0,
			 Level.LEVEL_LENGTH * TILE_SIZE - ClientSize.Height
		 );
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);

			if (e.Button == MouseButtons.Middle)
			{
				_cameraDrag = false;
			}
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			if (_cameraDrag)
			{
				_camera.Move(new Vector2(_mouseX - e.X, _mouseY - e.Y));
			}

			_mouseX = e.X;
			_mouseY = e.Y;
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

			if (e.Button == MouseButtons.Middle)
			{
				_cameraDrag = true;
			}
		}

		private object GetObjectTiles()
		{
			throw new NotImplementedException();
		}

		private object GetBuildingTiles()
		{
			throw new NotImplementedException();
		}

		private Dictionary<string, Microsoft.Xna.Framework.Rectangle> GetGroundTiles()
		{
			return new Dictionary<string, Microsoft.Xna.Framework.Rectangle>
			{
				{ "sand", new Microsoft.Xna.Framework.Rectangle(0, 0, 128, 128) },
				{ "beach_tm_02_grass", new Microsoft.Xna.Framework.Rectangle(128, 0, 128, 128) },
				{ "beach_tm_02", new Microsoft.Xna.Framework.Rectangle(256, 0, 128, 128) },
				{ "beach_tm_01_grass", new Microsoft.Xna.Framework.Rectangle(384, 0, 128, 128) },
				{ "beach_tm_01", new Microsoft.Xna.Framework.Rectangle(512, 0, 128, 128) },
				{ "beach_tl_grass", new Microsoft.Xna.Framework.Rectangle(640, 0, 128, 128) },
				{ "beach_tl", new Microsoft.Xna.Framework.Rectangle(768, 0, 128, 128) },
				{ "beach_rm_05_grass", new Microsoft.Xna.Framework.Rectangle(896, 0, 128, 128) },
			};
		}
	}
}
