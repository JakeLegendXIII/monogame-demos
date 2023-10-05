using Chopper.Engine.Objects;
using Microsoft.Xna.Framework.Graphics;

namespace Chopper.Objects.Text
{
    public class GameOverText : BaseTextObject
    {
        public GameOverText(SpriteFont font)
        {
            _font = font;
            Text = "Game Over";           
        }
    }
}
