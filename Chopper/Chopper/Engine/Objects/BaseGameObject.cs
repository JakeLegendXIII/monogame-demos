using Chopper.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Chopper.Engine.Objects
{
    public class BaseGameObject
    {
        protected Texture2D _texture;

        protected Vector2 _position;
        public int zIndex;


        public int Width
        {
            get { return _texture.Width; }
        }   
        public int Height
        {
            get { return _texture.Height; }
        }   
        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public virtual void OnNotify(Events eventType)
        {

        }

        public virtual void Render(SpriteBatch spriteBatch)
        {            
            spriteBatch.Draw(_texture, _position, Color.White);
        }
    }
}
