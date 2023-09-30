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


        public class ChopperHitBy : GameplayEvents
        {
            public IGameObjectWithDamage HitBy { get; private set; }
            public ChopperHitBy(IGameObjectWithDamage gameObject)
            {
                HitBy = gameObject;
            }
        }

        public class EnemyLostLife : GameplayEvents
        {
            public int CurrentLife { get; private set; }
            public EnemyLostLife(int currentLife)
            {
                CurrentLife = currentLife;
            }
        }
    }
}
