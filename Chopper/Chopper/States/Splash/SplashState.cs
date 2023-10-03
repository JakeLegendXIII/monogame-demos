using Chopper.Objects;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Chopper.Engine.Input;
using Chopper.States.Splash;
using Chopper.Engine.States;
using Chopper.States.GamePlay;

namespace Chopper.States.Splash
{
    public class SplashState : BaseGameState
    {
        public override void UpdateGameState(GameTime _) { }

        public override void LoadContent()
        {
            AddGameObject(new SplashImage(LoadTexture("Images/splash")));
        }

        public override void HandleInput(GameTime gameTime)
        {
            InputManager.GetCommands(cmd =>
            {
                if (cmd is SplashInputCommand.GameSelect)
                {
                    SwitchState(new GameplayState());
                }
            });
        }

        protected override void SetInputManager()
        {
            InputManager = new InputManager(new SplashInputMapper());
        }
    }
}
