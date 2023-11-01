using Chopper.Engine;
using Chopper.Engine.Input;
using Chopper.Engine.Objects;
using Chopper.Engine.States;
using Chopper.Objects;
using Chopper.Particles;
using Microsoft.Xna.Framework;
using System;

namespace Chopper.States.Dev
{
    /// <summary>
    /// Used to test out new things, like particle engines and shooting missiles
    /// </summary>
    public class DevState : BaseGameState
    {
        private const string CloudTexture = "Sprites/explosion";
        private const string ChopperTexture = "Sprites/chopper";
        private const string FighterSpriteSheet = "Sprites/Animations/FighterSpriteSheet";
        private const string StatsFont = "Fonts/Stats";
        private PlayerSprite _player;

        private ChopperSprite _chopper;
        private ExplosionEmitter _explosion;
        private TimeSpan _explodeAt;

        private StatsObject _statsText;

        public override void LoadContent()
        {
            //_player = new PlayerSprite(LoadTexture(FighterSpriteSheet));
            //_player.Position = new Vector2(200, 400);
            //AddGameObject(_player);

            _chopper = new ChopperSprite(LoadTexture(ChopperTexture));
            _chopper.Position = new Vector2(600, 100);
            AddGameObject(_chopper);

            _statsText = new StatsObject(LoadFont(StatsFont));
            _statsText.Position = new Vector2(10, 10);

            if (Debug.Instance.IsDebugMode)
            {
                AddGameObject(_statsText);
            }
        }

        public override void HandleInput(GameTime gameTime)
        {
            InputManager.GetCommands(cmd =>
            {
                if (cmd is DevInputCommand.DevQuit)
                {
                    NotifyEvent(new BaseGameStateEvent.GameQuit());
                }

                if (cmd is DevInputCommand.DevCamLeft)
                {
                    _player.MoveLeft();
                }

                if (cmd is DevInputCommand.DevCamRight)
                {
                    _player.MoveRight();
                }

                if (cmd is DevInputCommand.DevNotMoving)
                {
                    _player.StopMoving();
                }
            });
        }

        public override void UpdateGameState(GameTime gameTime)
        {
            _player.Update(gameTime);

            if (_explosion == null && gameTime.TotalGameTime > TimeSpan.FromSeconds(2))
            {
                _explosion = new ExplosionEmitter(LoadTexture(CloudTexture));
                AddGameObject(_explosion);
                _explodeAt = gameTime.TotalGameTime;
            }

            if (_explosion != null && gameTime.TotalGameTime - _explodeAt > TimeSpan.FromSeconds(1.2))
            {
                _explosion.Deactivate();
            }

            if (_explosion != null && gameTime.TotalGameTime - _explodeAt > TimeSpan.FromSeconds(0.5))
            {
                RemoveGameObject(_chopper);
            }

            if (_explosion != null && gameTime.TotalGameTime > TimeSpan.FromSeconds(10))
            {
                RemoveGameObject(_explosion);
            }

            if (_explosion != null)
            {
                _explosion.Update(gameTime);
            }

            if (Debug.Instance.IsDebugMode)
            {
                _statsText.Update(gameTime);
            }
        }

        protected override void SetInputManager()
        {
            InputManager = new InputManager(new DevInputMapper());
        }
    }
}
