using Chopper.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Chopper.Objects.Base
{
    public class BaseGameObject
    {
        protected Texture2D Texture;

        private Microsoft.Xna.Framework.Vector2 _position;
        public int zIndex;

        public virtual void OnNotify(Events eventType)
        {

        }

        public void Render(SpriteBatch spriteBatch)
        {
            // TODO : Drawing call here
            spriteBatch.Draw(Texture, _position, Color.White);
        }
    }
}
