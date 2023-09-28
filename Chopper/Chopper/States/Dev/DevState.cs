using Chopper.Engine.Input;
using Chopper.Engine.States;
using Chopper.Particles;
using Microsoft.Xna.Framework;
using System;

namespace Chopper.States.Dev
{
    public class DevState : BaseGameState
    {
        private const string ExhaustTexture = "Cloud001";
        private ExhaustEmitter _exhaustEmitter;

        public override void HandleInput(GameTime gameTime)
        {
            InputManager.GetCommands(cmd =>
            {
                if (cmd is DevInputCommand.DevQuit)
                {
                    NotifyEvent(new BaseGameStateEvent.GameQuit());
                }
            });
        }

        public override void LoadContent()
        {
            var exhaustPosition = new Vector2(_viewportWidth / 2, _viewportHeight / 2);
            _exhaustEmitter = new ExhaustEmitter(LoadTexture(ExhaustTexture), exhaustPosition);
            AddGameObject(_exhaustEmitter);
        }

        public override void UpdateGameState(GameTime gameTime)
        {
            _exhaustEmitter.Position = new Vector2(_exhaustEmitter.Position.X, _exhaustEmitter.Position.Y - 3f);
            _exhaustEmitter.Update(gameTime);

            if (_exhaustEmitter.Position.Y < -200)
            {
                RemoveGameObject(_exhaustEmitter);
            }
        }

        protected override void SetInputManager()
        {
            InputManager = new InputManager(new DevInputMapper());
        }
    }
}
