using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.NET.Controls;

namespace GameEditor
{
	public class GameControl : MonoGameControl
	{		
		protected override void Initialize()
		{
			
		}

		protected override void Update(GameTime gameTime)
		{			
		}

		protected override void Draw()
		{
			var tower = Editor.Content.Load<Texture2D>(@"Sprites\Tower");
			Editor.spriteBatch.Begin();
			Editor.spriteBatch.Draw(tower, new Microsoft.Xna.Framework.Rectangle(0, 0, tower.Width, tower.Height), 
				Microsoft.Xna.Framework.Color.White);
			Editor.spriteBatch.End();
		}
	}
}
