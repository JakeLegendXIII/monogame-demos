using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TRexRunner.Entities;

namespace TRexRunner.System
{
	public class InputController
	{
		private Trex _trex;
		private KeyboardState _previousKeyboardState;

		public InputController(Trex trex)
		{
			_trex = trex;
		}

		public void ProcessControls(GameTime gameTime)
		{
			KeyboardState keyboardState = Keyboard.GetState();

			if (!_previousKeyboardState.IsKeyDown(Keys.Up) && keyboardState.IsKeyDown(Keys.Up))
			{
				if (_trex.State != TrexState.Jumping)
				{
					_trex.BeginJump();
				}
				else
				{
					_trex.ContinueJump();
				}				
			}
			else if (_previousKeyboardState.IsKeyDown(Keys.Up) && !keyboardState.IsKeyDown(Keys.Up))
			{
				_trex.CancelJump();
			}
			
			_previousKeyboardState = keyboardState;
		}
	}
}
