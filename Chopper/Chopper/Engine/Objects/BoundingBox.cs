using Microsoft.Xna.Framework;

namespace Chopper.Engine.Objects
{
    public class BoundingBox
    {
        public Vector2 Position { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, (int)Width, (int)Height);
            }
        }

        public BoundingBox(Vector2 position, float width, float height)
        {
            Position = position;
            Width = width;
            Height = height;
        }

        public bool CollidesWith(BoundingBox otherBoundingBox)
        {
            if (Position.X < otherBoundingBox.Position.X + otherBoundingBox.Width &&
                               Position.X + Width > otherBoundingBox.Position.X &&
                                              Position.Y < otherBoundingBox.Position.Y + otherBoundingBox.Height &&
                                                             Position.Y + Height > otherBoundingBox.Position.Y)
            {
                return true;
            }
            else
            {
                return false;
            }            
        }
    }
}
