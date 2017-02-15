/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 2/14/2017
 * Time: 4:44 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace CavernaWPF.Layables
{
	/// <summary>
	/// Description of FurnishingTileLayable.
	/// </summary>
	public class FurnishingTileLayable : Tile
	{
		public FurnishingTileLayable(FurnishingTile ft) : base(Tile.Type.FurnishingTile)
		{
			furnishingtile = ft;
			Img = ft.Img;
		}
		
		public FurnishingTile furnishingtile;
	}
}
