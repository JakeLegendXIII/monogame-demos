using Chopper.Enums;
using Chopper.Input.Base;
using Chopper.Objects.Base;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chopper.States.Base
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

        public abstract void HandleInput();

        public event EventHandler<BaseGameState> OnStateSwitched;

        public event EventHandler<Events> OnEventNotification;

        protected Texture2D LoadTexture(string textureName)
        {
            var texture = _contentManager.Load<Texture2D>(textureName);

            return texture ?? _contentManager.Load<Texture2D>(FallbackTexture);
        }

        protected void NotifyEvent(Events eventType, object argument = null)
        {
            OnEventNotification?.Invoke(this, eventType);

            foreach(var gameObject in _gameObjects)
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

        public void Render(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            foreach(var gameObject in _gameObjects.OrderBy(x => x.zIndex))
            {
                gameObject.Render(spriteBatch);
            }
        }

    }
}
