using Chopper.Engine.Objects.Collisions;
using Chopper.Engine.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace Chopper.Engine.Objects
{
    public class BaseGameObject
    {
        protected Texture2D _texture;
        protected Texture2D _boundingBoxTexture;

        protected Vector2 _position = Vector2.One;
        protected List<Collisions.BoundingBox> _boundingBoxes = new List<Collisions.BoundingBox>();
        public bool Active { get; protected set; }
        public float Angle { get; set; }
        public Vector2 Direction { get; set; }

        protected Vector2 CalculateDirection(float angleOffset = 0.0f)
        {
            Direction = new Vector2((float)Math.Cos(Angle - angleOffset), (float)Math.Sin(Angle - angleOffset));
            Direction.Normalize();

            return Direction;
        }

        public int zIndex;
        public event EventHandler<BaseGameStateEvent> OnObjectChanged;

        public bool Destroyed { get; private set; }

        public virtual int Width { get { return _texture.Width; } }
        public virtual int Height { get { return _texture.Height; } }
        public virtual Vector2 Position
        {
            get { return _position; }
            set
            {
                var deltaX = value.X - _position.X;
                var deltaY = value.Y - _position.Y;
                _position = value;

                foreach (var bb in _boundingBoxes)
                {
                    bb.Position = new Vector2(bb.Position.X + deltaX, bb.Position.Y + deltaY);
                }
            }
        }

        public BaseGameObject() { }

        public BaseGameObject(Texture2D texture)
        {
            _texture = texture;            
        }

        public List<Collisions.BoundingBox> BoundingBoxes
        {
            get
            {
                return _boundingBoxes;
            }
        }

        public virtual void OnNotify(BaseGameStateEvent eventType)
        {

        }

        public virtual void Render(SpriteBatch spriteBatch)
        {            
            spriteBatch.Draw(_texture, _position, Color.White);
        }

        public void RenderBoundingBoxes(SpriteBatch spriteBatch)
        {
            if (_boundingBoxTexture == null)
            {
                CreateBoundingBoxTexture(spriteBatch.GraphicsDevice);
            }

            foreach (var bb in _boundingBoxes)
            {
                spriteBatch.Draw(_boundingBoxTexture, bb.Rectangle, Color.Red);
            }
        }

        private void CreateBoundingBoxTexture(GraphicsDevice graphicsDevice)
        {
            _boundingBoxTexture = new Texture2D(graphicsDevice, 1, 1);
            _boundingBoxTexture.SetData<Color>(new Color[] { Color.White });
        }

        public void SendEvent(BaseGameStateEvent e)
        {
            OnObjectChanged?.Invoke(this, e);
        }

        public void AddBoundingBox(Collisions.BoundingBox bb)
        {
            _boundingBoxes.Add(bb);
        }

        public void Destroy()
        {
            Destroyed = true;
        }

        public virtual void Activate()
        {
            Active = true;
        }

        public virtual void Deactivate()
        {
            Active = false;
        }

        public virtual void Initialize()
        {
            Angle = 0.0f;
            Direction = new Vector2(0, 0);
            Position = Vector2.One;
        }
    }
}
