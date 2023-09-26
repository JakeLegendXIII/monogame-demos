using Chopper.Engine.Input;
using Chopper.Engine.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chopper.Engine.States
{
    public abstract class BaseGameState
    {
        private const string FallbackTexture = "Empty";

        private ContentManager _contentManager;
        protected int _viewportHeight;
        protected int _viewportWidth;

        protected InputManager InputManager { get; set; }

        private readonly List<BaseGameObject> _gameObjects = new List<BaseGameObject>();

        public abstract void LoadContent();

        public void Initialize(ContentManager contentManager, int viewportWidth, int viewportHeight)
        {
            _contentManager = contentManager;
            _viewportHeight = viewportHeight;
            _viewportWidth = viewportWidth;

            SetInputManager();
        }

        protected abstract void SetInputManager();

        public void UnloadContent(ContentManager contentManager)
        {
            _contentManager.Unload();
        }

        public virtual void Update(GameTime gameTime) { }

        public abstract void HandleInput(GameTime gameTime);

        public event EventHandler<BaseGameState> OnStateSwitched;

        public event EventHandler<BaseGameStateEvent> OnEventNotification;

        protected Texture2D LoadTexture(string textureName)
        {
            var texture = _contentManager.Load<Texture2D>(textureName);

            return texture ?? _contentManager.Load<Texture2D>(FallbackTexture);
        }

        protected void NotifyEvent(BaseGameStateEvent eventType, object argument = null)
        {
            OnEventNotification?.Invoke(this, eventType);

            foreach (var gameObject in _gameObjects)
            {
                gameObject.OnNotify(eventType);
            }
        }

        protected void SwitchState(BaseGameState newState)
        {
            OnStateSwitched?.Invoke(this, newState);
        }

        protected void AddGameObject(BaseGameObject gameObject)
        {
            _gameObjects.Add(gameObject);
        }

        protected void RemoveGameObject(BaseGameObject gameObject)
        {
            _gameObjects.Remove(gameObject);
        }

        public void Render(SpriteBatch spriteBatch)
        {
            foreach (var gameObject in _gameObjects.OrderBy(x => x.zIndex))
            {
                gameObject.Render(spriteBatch);
            }
        }

    }
}
