using Chopper.Engine.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Chopper.Objects
{
    public class TurretBulletSprite : BaseGameObject
    {
        private const float BULLET_SPEED = 18.0f;
        private Vector2 _bulletCenterPosition;

        public Segment CollisionSegment
        {
            get
            {
                var segment = Direction * _texture.Height;
                return new Segment(_position, Vector2.Add(_position, segment));
            }
        }

        public TurretBulletSprite(Texture2D texture, Vector2 direction, float angle)
        {
            _texture = texture;
            Direction = direction;
            Direction.Normalize();

            _bulletCenterPosition = new Vector2(_texture.Width / 2, _texture.Height / 2);
            Angle = angle;
        }

        public void Update()
        {
            Position += Direction * BULLET_SPEED;
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, _texture.Bounds, Color.White, Angle, _bulletCenterPosition, 1f, SpriteEffects.None, 0f);
        }
    }
}
