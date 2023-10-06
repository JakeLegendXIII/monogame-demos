
using Chopper.Engine.States;

namespace Chopper.Levels
{
    public class LevelEvents : BaseGameStateEvent
    {
        public class GenerateEnemies : LevelEvents
        {
            public int NumberOfEnemies { get; private set; }
            public GenerateEnemies(int numberOfEnemies)
            {
                NumberOfEnemies = numberOfEnemies;
            }
        }

        public class GenerateTurret : LevelEvents
        {
            public float XPosition { get; private set; }
            public GenerateTurret(float xPosition)
            {
                this.XPosition = xPosition;
            }
        }

        public class StartLevel : LevelEvents { }
        public class EndLevel : LevelEvents { }
        public class NoRowEvent : LevelEvents { }

    }
}
