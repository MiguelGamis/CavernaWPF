/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 01/14/2017
 * Time: 19:45
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using CavernaWPF.Layables;

namespace CavernaWPF
{
	/// <summary>
	/// Description of Helpers.
	/// </summary>
	public static class Helpers
	{
		public static BoardTile.Type convertFirst(Tile t)
		{
			switch(t.type)
			{
				case Tile.Type.Meadow:
					return BoardTile.Type.Meadow;
					break;
				case Tile.Type.Field:
					return BoardTile.Type.Field;
					break;
				case Tile.Type.Cave:
					return BoardTile.Type.Cave;
					break;
				case Tile.Type.Tunnel:
					return BoardTile.Type.Tunnel;
					break;
				case Tile.Type.RubyMine:
					return BoardTile.Type.RubyMine;
					break;
				case Tile.Type.MeadowField:
					return BoardTile.Type.Meadow;
					break;
				case Tile.Type.CaveCave:
					return BoardTile.Type.Cave;
					break;
				case Tile.Type.CaveTunnel:
					return BoardTile.Type.Cave;
					break;
				case Tile.Type.OreMine:
					return BoardTile.Type.OreMine;
					break;
				case Tile.Type.Fence:
					return BoardTile.Type.Fenced;
					break;
				case Tile.Type.BigFence:
					return BoardTile.Type.Fenced;
					break;
				default:
					return BoardTile.Type.Forest;
					break;
			}
		}
		
		public static BoardTile.Type convertSecond(Tile t)
		{
			switch(t.type)
			{
				case Tile.Type.Meadow:
					return BoardTile.Type.Meadow;
					break;
				case Tile.Type.Field:
					return BoardTile.Type.Field;
					break;
				case Tile.Type.Cave:
					return BoardTile.Type.Cave;
					break;
				case Tile.Type.Tunnel:
					return BoardTile.Type.Tunnel;
					break;
				case Tile.Type.RubyMine:
					return BoardTile.Type.RubyMine;
					break;
				case Tile.Type.MeadowField:
					return BoardTile.Type.Field;
					break;
				case Tile.Type.CaveCave:
					return BoardTile.Type.Cave;
					break;
				case Tile.Type.CaveTunnel:
					return BoardTile.Type.Tunnel;
					break;
				case Tile.Type.OreMine:
					return BoardTile.Type.DeepTunnel;
					break;
				case Tile.Type.Fence:
					return BoardTile.Type.Fenced;
					break;
				case Tile.Type.BigFence:
					return BoardTile.Type.Fenced;
					break;
				default:
					return BoardTile.Type.Forest;
					break;
			}
		}
	}
}
