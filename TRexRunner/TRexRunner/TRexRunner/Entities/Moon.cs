﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TRexRunner.Graphics;

namespace TRexRunner.Entities
{
	public class Moon : SkyObject
	{
		private const int RIGHTMOST_SPRITE_COORDS_X = 624;
		private const int RIGHTMOST_SPRITE_COORDS_Y = 2;

		private const int MOON_WIDTH = 20;
		private const int MOON_HEIGHT = 40;

		private const int SPRITE_COUNT = 7;

		private readonly IDayNightCycle _dayNightCycle;
		private Sprite _sprite;

		public override float Speed => _trex.Speed * 0.1f;

		public Moon(IDayNightCycle dayNightCycle, Texture2D spriteSheet, Trex trex, Vector2 position) : base(trex, position)
		{
			_dayNightCycle = dayNightCycle;

			_sprite = new Sprite(spriteSheet, RIGHTMOST_SPRITE_COORDS_X, RIGHTMOST_SPRITE_COORDS_Y, MOON_WIDTH, MOON_HEIGHT);
		}

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			UpdateSprite();

			if (_dayNightCycle.IsNight)
			{
				_sprite.Draw(spriteBatch, Position);
			}
		}

		public void UpdateSprite()
		{
			int spriteIndex = _dayNightCycle.NightCount % SPRITE_COUNT;

			int spriteWidth = MOON_WIDTH;
			int spriteHeight = MOON_HEIGHT;

			if (spriteIndex == 3)
				spriteWidth *= 2;

			if (spriteIndex >= 3)
				spriteIndex++;

			_sprite.Height = spriteHeight;
			_sprite.Width = spriteWidth;

			_sprite.X = RIGHTMOST_SPRITE_COORDS_X - spriteIndex * MOON_WIDTH;
			_sprite.Y = RIGHTMOST_SPRITE_COORDS_Y;
		}
	}
}
