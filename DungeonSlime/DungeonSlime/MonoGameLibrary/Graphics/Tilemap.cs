using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameLibrary.Graphics;

public class Tilemap
{
	private readonly Tileset _tileset;
	private readonly int[] _tiles;

	/// <summary>
	/// Gets the total number of rows in this tilemap.
	/// </summary>
	public int Rows { get; }

	/// <summary>
	/// Gets the total number of columns in this tilemap.
	/// </summary>
	public int Columns { get; }

	/// <summary>
	/// Gets the total number of tiles in this tilemap.
	/// </summary>
	public int Count { get; }

	/// <summary>
	/// Gets or Sets the scale factor to draw each tile at.
	/// </summary>
	public Vector2 Scale { get; set; }

	/// <summary>
	/// Gets the width, in pixels, each tile is drawn at.
	/// </summary>
	public float TileWidth => _tileset.TileWidth * Scale.X;

	/// <summary>
	/// Gets the height, in pixels, each tile is drawn at.
	/// </summary>
	public float TileHeight => _tileset.TileHeight * Scale.Y;

	/// <summary>
	/// Creates a new tilemap.
	/// </summary>
	/// <param name="tileset">The tileset used by this tilemap.</param>
	/// <param name="columns">The total number of columns in this tilemap.</param>
	/// <param name="rows">The total number of rows in this tilemap.</param>
	public Tilemap(Tileset tileset, int columns, int rows)
	{
		_tileset = tileset;
		Rows = rows;
		Columns = columns;
		Count = Columns * Rows;
		Scale = Vector2.One;
		_tiles = new int[Count];
	}

	/// <summary>
	/// Sets the tile at the given index in this tilemap to use the tile from
	/// the tileset at the specified tileset id.
	/// </summary>
	/// <param name="index">The index of the tile in this tilemap.</param>
	/// <param name="tilesetID">The tileset id of the tile from the tileset to use.</param>
	public void SetTile(int index, int tilesetID)
	{
		_tiles[index] = tilesetID;
	}

	/// <summary>
	/// Sets the tile at the given column and row in this tilemap to use the tile
	/// from the tileset at the specified tileset id.
	/// </summary>
	/// <param name="column">The column of the tile in this tilemap.</param>
	/// <param name="row">The row of the tile in this tilemap.</param>
	/// <param name="tilesetID">The tileset id of the tile from the tileset to use.</param>
	public void SetTile(int column, int row, int tilesetID)
	{
		int index = row * Columns + column;
		SetTile(index, tilesetID);
	}

	/// <summary>
	/// Gets the texture region of the tile from this tilemap at the specified index.
	/// </summary>
	/// <param name="index">The index of the tile in this tilemap.</param>
	/// <returns>The texture region of the tile from this tilemap at the specified index.</returns>
	public TextureRegion GetTile(int index)
	{
		return _tileset.GetTile(_tiles[index]);
	}

	/// <summary>
	/// Gets the texture region of the tile frm this tilemap at the specified
	/// column and row.
	/// </summary>
	/// <param name="column">The column of the tile in this tilemap.</param>
	/// <param name="row">The row of hte tile in this tilemap.</param>
	/// <returns>The texture region of the tile from this tilemap at the specified column and row.</returns>
	public TextureRegion GetTile(int column, int row)
	{
		int index = row * Columns + column;
		return GetTile(index);
	}


}