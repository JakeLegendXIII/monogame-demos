namespace MonoGameLibrary.Graphics;

public class Tileset
{
	private readonly TextureRegion[] _tiles;

	/// <summary>
	/// Gets the width, in pixels, of each tile in this tileset.
	/// </summary>
	public int TileWidth { get; }

	/// <summary>
	/// Gets the height, in pixels, of each tile in this tileset.
	/// </summary>
	public int TileHeight { get; }

	/// <summary>
	/// Gets the total number of columns in this tileset.
	/// </summary>
	public int Columns { get; }

	/// <summary>
	/// Gets the total number of rows in this tileset.
	/// </summary>
	public int Rows { get; }

	/// <summary>
	/// Gets the total number of tiles in this tileset.
	/// </summary>
	public int Count { get; }

}