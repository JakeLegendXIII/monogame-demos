using Chopper.Engine.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Chopper.Engine.Objects
{
    public class BaseGameObject
    {
        protected Texture2D _texture;

        protected Vector2 _position;
        public int zIndex;
        public event EventHandler<BaseGameStateEvent> OnObjectChanged;

        public int Width
        {
            get { return _texture.Width; }
        }   
        public int Height
        {
            get { return _texture.Height; }
        }   
        public virtual Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public virtual void OnNotify(BaseGameStateEvent eventType)
        {

        }

        public virtual void Render(SpriteBatch spriteBatch)
        {            
            spriteBatch.Draw(_texture, _position, Color.White);
        }

        public void SendEvent(BaseGameStateEvent e)
        {
            OnObjectChanged?.Invoke(this, e);
        }
    }
}
