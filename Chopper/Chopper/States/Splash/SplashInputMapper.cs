﻿using Chopper.Engine.Input;
using Chopper.States.GamePlay;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chopper.States.Splash
{
    public class SplashInputMapper : BaseInputMapper
    {
        public override IEnumerable<BaseInputCommand> GetKeyboardState(KeyboardState state)
        {
            var commands = new List<SplashInputCommand>();

            if (state.IsKeyDown(Keys.Enter))
            {
                commands.Add(new SplashInputCommand.GameSelect());
            }

            return commands;
        }

        public override IEnumerable<BaseInputCommand> GetGamePadState(GamePadState state)
        {
            var commands = new List<SplashInputCommand>();

            if (state.IsButtonDown(Buttons.A))
            {
                commands.Add(new SplashInputCommand.GameSelect());
            }

            if (state.IsButtonDown(Buttons.Start))
            {
                commands.Add(new SplashInputCommand.GameSelect());
            }

            return commands;
        }
    }
}
