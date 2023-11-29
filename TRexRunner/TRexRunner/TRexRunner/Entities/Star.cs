using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TRexRunner.Graphics;

namespace TRexRunner.Entities
{
	public class Star : SkyObject
	{		
		private const int STAR_SOURCE_X = 644;
		private const int STAR_SOURCE_Y = 2;
		private const int STAR_WIDTH = 9;
		private const int STAR_HEIGHT = 9;

		private const float ANIMATION_FRAME_LENGTH = 0.4f;

		private SpriteAnimation _animation;
		private IDayNightCycle _dayNightCycle;

		public override float Speed => _trex.Speed * 0.2f;

		public Star(IDayNightCycle dayNightCycle, Trex trex, Vector2 position, Texture2D spriteSheet) : base(trex, position)
		{
			_dayNightCycle = dayNightCycle;

			_animation = SpriteAnimation.CreateSimpleAnimation(spriteSheet, new Point(STAR_SOURCE_X, STAR_SOURCE_Y), STAR_WIDTH, STAR_HEIGHT,
				new Point(0, STAR_HEIGHT), 3, ANIMATION_FRAME_LENGTH);

			_animation.ShouldLoop = true;
			_animation.Play();
		}

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
            if (_dayNightCycle.IsNight)
            {
				_animation.Draw(spriteBatch, Position);
			}            
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			if (_trex.IsAlive)
			{
				_animation.Update(gameTime);
			}
			
		}
	}
}
