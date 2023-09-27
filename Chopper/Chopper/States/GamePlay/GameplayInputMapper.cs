using Chopper.Engine.Input;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Chopper.States.GamePlay
{
    public class GameplayInputMapper : BaseInputMapper
    {
        public override IEnumerable<BaseInputCommand> GetKeyboardState(KeyboardState state)
        {
            var commands = new List<GameplayInputCommand>();

            if (state.IsKeyDown(Keys.Escape))
            {
                commands.Add(new GameplayInputCommand.GameExit());
            }

            if (state.IsKeyDown(Keys.Left) || state.IsKeyDown(Keys.A))
            {
                commands.Add(new GameplayInputCommand.PlayerMoveLeft());
            }

            if (state.IsKeyDown(Keys.Right) || state.IsKeyDown(Keys.D))
            {
                commands.Add(new GameplayInputCommand.PlayerMoveRight());
            }

            if (state.IsKeyDown(Keys.Space))
            {
                commands.Add(new GameplayInputCommand.PlayerShoots());
            }

            return commands;
        }

        public override IEnumerable<BaseInputCommand> GetGamePadState(GamePadState state)
        {
            var commands = new List<GameplayInputCommand>();

            if (state.IsButtonDown(Buttons.Back))
            {
                commands.Add(new GameplayInputCommand.GameExit());
            }

            if (state.IsButtonDown(Buttons.RightTrigger))
            {
                commands.Add(new GameplayInputCommand.PlayerShoots());
            }

            if (state.DPad.Left == ButtonState.Pressed)
            {
                commands.Add(new GameplayInputCommand.PlayerMoveLeft());
            }

            if (state.DPad.Right == ButtonState.Pressed)
            {
                commands.Add(new GameplayInputCommand.PlayerMoveRight());
            }

            if (state.ThumbSticks.Left.X < 0)
            {
                commands.Add(new GameplayInputCommand.PlayerMoveLeft());
            }

            if (state.ThumbSticks.Left.X > 0)
            {
                commands.Add(new GameplayInputCommand.PlayerMoveRight());
            }

            return commands;
        }
    }
}
