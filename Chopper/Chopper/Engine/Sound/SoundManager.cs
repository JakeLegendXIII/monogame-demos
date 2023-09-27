using Chopper.Engine.States;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chopper.Engine.Sound
{
    public class SoundManager
    {
        private int _soundTrackIndex = -1;
        private List<SoundEffectInstance> _soundTracks = new List<SoundEffectInstance>();
        private Dictionary<Type, SoundEffect> _soundBank = new Dictionary<Type, SoundEffect>();

        public void SetSoundTrack(List<SoundEffectInstance> tracks)
        {
            _soundTracks = tracks;
            _soundTrackIndex = _soundTracks.Count - 1;
        }

        public void PlaySoundTrack()
        {
            var nbTracks = _soundTracks.Count;

            if (nbTracks <= 0)
            {
                return;
            }

            var currentTrack = _soundTracks[_soundTrackIndex];
            var nextTrack = _soundTracks[(_soundTrackIndex + 1) % nbTracks];

            if (currentTrack.State == SoundState.Stopped)
            {
                nextTrack.Play();
                _soundTrackIndex++;

                if (_soundTrackIndex >= _soundTracks.Count)
                {
                    _soundTrackIndex = 0;
                }
            }
        }

        public void RegisterSound(BaseGameStateEvent gameStateEvent, SoundEffect sound)
        {
            _soundBank[gameStateEvent.GetType()] = sound;
        }

        public void OnNotify(BaseGameStateEvent gameEvent)
        {
            if (_soundBank.ContainsKey(gameEvent.GetType()))
            {
                _soundBank[gameEvent.GetType()].Play();
            }
        }
    }
}
