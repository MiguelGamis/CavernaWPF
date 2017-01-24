/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 1/22/2017
 * Time: 6:41 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using CavernaWPF.Resources;

namespace CavernaWPF
{
	/// <summary>
	/// Description of FurnishingTile.
	/// </summary>
	public class FurnishingTile
	{
		public string Name
		{
			get;
			set;
		}
		
		public Player player;
		
		public List<ResourcesTab> Cost = new List<ResourcesTab>();
		
		public Action<Player> Effect;
		
		public Action VictoryPoints;
		
		public int Row
		{
			get;
			set;
		}
		
		public int Column
		{
			get;
			set;
		}
		
		public string Img
		{
			get;
			set;
		}
		
		public FurnishingTile()
		{
		}
		
		public void Woodsupplier(Player p)
		{
			p.Resources[Resource.Type.Stone].ResourceIncremented += delegate(object sender, EventArgs args) { p.Resources[Resource.Type.Ore].Amount++; };
		}
		
		public void Dogschool(Player p)
		{
			p.Resources[Resource.Type.Stone].ResourceIncremented += delegate(object sender, EventArgs args) { p.Resources[Resource.Type.Ore].Amount++; };
		}
		
		public void Seam(Player p)
		{
			p.Resources[Resource.Type.Stone].ResourceIncremented += delegate(object sender, EventArgs args) { p.Resources[Resource.Type.Ore].Amount++; };
		}
		
		public void Quarry(Player p)
		{
		}
	}
}
