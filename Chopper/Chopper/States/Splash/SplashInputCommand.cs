using Chopper.Engine.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chopper.States.Splash
{
    public class SplashInputCommand : BaseInputCommand
    {
        // Out of Game Commands
        public class GameSelect : SplashInputCommand { }
    }
}
