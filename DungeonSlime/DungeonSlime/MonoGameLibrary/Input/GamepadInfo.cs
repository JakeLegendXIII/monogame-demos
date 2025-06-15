using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGameLibrary.Input;

public class GamePadInfo
{
	private TimeSpan _vibrationTimeRemaining = TimeSpan.Zero;

	/// <summary>
	/// Gets the index of the player this gamepad is for.
	/// </summary>
	public PlayerIndex PlayerIndex { get; }

	/// <summary>
	/// Gets the state of input for this gamepad during the previous update cycle.
	/// </summary>
	public GamePadState PreviousState { get; private set; }

	/// <summary>
	/// Gets the state of input for this gamepad during the current update cycle.
	/// </summary>
	public GamePadState CurrentState { get; private set; }

	/// <summary>
	/// Gets a value that indicates if this gamepad is currently connected.
	/// </summary>
	public bool IsConnected => CurrentState.IsConnected;


}
