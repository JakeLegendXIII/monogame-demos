using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Forms.NET.Controls;
using MonoGame.Extended.TextureAtlases;
using MonoGame.Extended.ViewportAdapters;
using ChopperPipelineExtensions;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GameEditor
{
	public class GameControl : MonoGameControl
	{
		public const int TILE_SIZE = 128;

		private int _mouseX;
		private int _mouseY;

		private Texture2D _backgroundRectangle;
		private OrthographicCamera _camera;
		private bool _cameraDrag;

		public event EventHandler<EventArgs> OnInitialized;

		protected override void Initialize()
		{
			_backgroundRectangle = new Texture2D(Editor.GraphicsDevice, 1, 1);
			_backgroundRectangle.SetData(new[] { Microsoft.Xna.Framework.Color.CadetBlue });
			var viewportAdapter = new DefaultViewportAdapter(Editor.GraphicsDevice);
			_camera = new OrthographicCamera(viewportAdapter);
			ResetCameraPostion();

			//OnInitialized(this, EventArgs.Empty);
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
	}
}
