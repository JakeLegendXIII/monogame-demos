using Chopper.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Chopper.Objects.Base
{
    public class BaseGameObject
    {
        protected Texture2D _texture;

        private Microsoft.Xna.Framework.Vector2 _position;
        public int zIndex;

        public virtual void OnNotify(Events eventType)
        {

        }

        public void Render(SpriteBatch spriteBatch)
        {
            // TODO : Drawing call here
            spriteBatch.Draw(_texture, _position, Color.White);
        }
    }
}
