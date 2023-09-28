using Chopper.Engine.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chopper.States.Dev
{
    public class DevInputCommand : BaseInputCommand
    {
        // Out of Game Commands
        public class DevQuit : DevInputCommand { }
        public class DevShoot : DevInputCommand { }
    }
}
