using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chopper.Engine.Sound
{
    public class SoundBankItem
    {
        public SoundEffect Sound { get; private set; }
        public SoundAttributes Attributes { get; private set; }

        public SoundBankItem(SoundEffect sound, SoundAttributes attributes)
        {
            Sound = sound;
            Attributes = attributes;
        }
    }
}
