using Chopper.Engine.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chopper.States.GamePlay
{
    public class GameplayEvents : BaseGameStateEvent
    {
        public class PlayerShootsBullets : GameplayEvents { }
        public class PlayerShootsMissile : GameplayEvents { }
    }
}
