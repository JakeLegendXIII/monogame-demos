using Microsoft.Xna.Framework;
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

		private Texture2D _texture;

		public double Score { get; set; }
		public int DisplayScore => (int)Math.Floor(Score);
		public int HighScore { get; set; }
		public bool HasHighScore => HighScore > 0;

		public int DrawOrder => 100;

		public Vector2 Position { get; set; }

		public ScoreBoard(Texture2D texture, Vector2 position)
		{
			_texture = texture;
			Position = position;
		}

		public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{						
			if (HasHighScore)
			{
				DrawScore(spriteBatch, HighScore, Position.X);
			}

			DrawScore(spriteBatch, DisplayScore, Position.X + 70);
		}		

		public void Update(GameTime gameTime)
		{
			
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
