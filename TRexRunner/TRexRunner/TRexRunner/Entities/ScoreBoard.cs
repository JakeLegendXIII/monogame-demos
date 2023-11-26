using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TRexRunner.Entities
{
	public class ScoreBoard : IGameEntity
	{
		private const int TEXTURE_SPRITE_POS_X = 655;
		private const int TEXTURE_SPRITE_POS_Y = 0;
		private const int TEXTURE_SPRITE_WIDTH = 10;
		private const int TEXTURE_SPRITE_HEIGHT = 13;

		private const byte NUMBER_OF_DIGITS = 5;
		private const int SCORE_MARGIN = 70;

		private const int TEXTURE_COORDS_HI_X = 755;
		private const int TEXTURE_COORDS_HI_Y = 0;
		private const int TEXTURE_COORDS_HI_WIDTH = 20;
		private const int TEXTURE_COORDS_HI_HEIGHT = 13;
		private const int HI_TEXT_MARGIN = 28;

		private const float SCORE_INCREMENT_MULTIPLIER = 0.025f;

		private const float FLASH_ANIMATION_FRAME_LENGTH = 0.333f;
		private const int FLASH_ANIMATION_FLASH_COUNT = 4;

		private bool _isFlashing;
		private float _flashTimer;

		private Texture2D _texture;
		private Trex _trex;
		private SoundEffect _scoreSfx;

		public double Score { get; set; }
		public int DisplayScore => (int)Math.Floor(Score);
		public int HighScore { get; set; }
		public bool HasHighScore => HighScore > 0;

		public int DrawOrder => 100;

		public Vector2 Position { get; set; }

		public ScoreBoard(Texture2D texture, Vector2 position, Trex  trex, SoundEffect scoreSfx)
		{
			_texture = texture;
			Position = position;
			_trex = trex;
			_scoreSfx = scoreSfx;
		}

		public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			
			if (HasHighScore)
			{
				spriteBatch.Draw(_texture, new Vector2(Position.X - HI_TEXT_MARGIN, Position.Y), new Rectangle(TEXTURE_COORDS_HI_X, TEXTURE_COORDS_HI_Y, TEXTURE_COORDS_HI_WIDTH, TEXTURE_COORDS_HI_HEIGHT), Color.White);

				DrawScore(spriteBatch, HighScore, Position.X);
			}

			if (!_isFlashing || ((int)(_flashTimer / FLASH_ANIMATION_FRAME_LENGTH) % 2 != 0))
			{
				DrawScore(spriteBatch, DisplayScore, Position.X + SCORE_MARGIN);
			}			
		}		

		public void Update(GameTime gameTime)
		{
			int oldScore = DisplayScore;

			Score += _trex.Speed * SCORE_INCREMENT_MULTIPLIER * gameTime.ElapsedGameTime.TotalSeconds;

            if (!_isFlashing && (DisplayScore / 100 != oldScore / 100))
            {
                _isFlashing = true;
				_flashTimer = 0;
				_scoreSfx.Play(0.5f, 0, 0);
            }

            if (_isFlashing)
            {
				_flashTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (_flashTimer >= FLASH_ANIMATION_FRAME_LENGTH * FLASH_ANIMATION_FLASH_COUNT)
                {
					_isFlashing = false;
                }
            }
        }

		private void DrawScore(SpriteBatch spriteBatch, int score, float startPosX)
		{
			int[] digits = SplitDigits(score);
			float posX = startPosX;

			foreach (var digit in digits)
			{
				Rectangle textureCoordinates = GetDigitTextureCoords(digit);

				Vector2 screenPos = new Vector2(posX, Position.Y);

				spriteBatch.Draw(_texture, screenPos, textureCoordinates, Color.White);

				posX += TEXTURE_SPRITE_WIDTH;
			}
		}

		private int[] SplitDigits(int input)
		{
			string inputString = input.ToString().PadLeft(NUMBER_OF_DIGITS, '0');

			int[] digits = new int[inputString.Length];

			for(var i = 0; i < inputString.Length; i++)
			{
				int digit = int.Parse(inputString[i].ToString());
				digits[i] = digit;
			}

			return digits;
		}

		private Rectangle GetDigitTextureCoords(int digit)
		{
			if (digit <0 || digit > 9)
			{
				throw new ArgumentOutOfRangeException(nameof(digit), "Digit must be between 0 and 9");
			}

			int x = TEXTURE_SPRITE_POS_X + (digit * TEXTURE_SPRITE_WIDTH);
			int y = TEXTURE_SPRITE_POS_Y;

			return new Rectangle(x, y, TEXTURE_SPRITE_WIDTH, TEXTURE_SPRITE_HEIGHT);
		}
	}
}
