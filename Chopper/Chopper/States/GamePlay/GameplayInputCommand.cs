﻿using Chopper.Engine.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chopper.States.GamePlay
{
    public class GameplayInputCommand : BaseInputCommand
    {
        public class GameExit : GameplayInputCommand { }
        public class PlayerMoveLeft : GameplayInputCommand { }
        public class PlayerMoveRight : GameplayInputCommand { }
        public class PlayerShoots : GameplayInputCommand { }
    }
}