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
		
		private int roundsOfEffect;
		
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
			ActionBoardContext.Instance.NewRound += delegate(object sender, EventArgs args) 
			{
				if(roundsOfEffect > 0)
				{
					p.Resources[Resource.Type.Wood].Amount++;
					roundsOfEffect--;
				}
				else
				{
					//Unhook
				}
			};
		}
		
		public void Stonesupplier(Player p)
		{
			ActionBoardContext.Instance.NewRound += delegate(object sender, EventArgs args) 
			{ 
				if(roundsOfEffect > 0)
				{
					p.Resources[Resource.Type.Stone].Amount++;
					roundsOfEffect--;
				}
				else
				{
					//Unhook
				}
			};
		}
		
		public void Rubysupplier(Player p)
		{
			ActionBoardContext.Instance.NewRound += delegate(object sender, EventArgs args) 
			{ 
				if(roundsOfEffect > 0)
				{
					p.Resources[Resource.Type.Ruby].Amount++;
					roundsOfEffect--;
				}
				else
				{
					//Unhook
				}
			};
		}
		
		public void Dogschool(Player p)
		{
			p.town.DogAdded += delegate(object sender, EventArgs args) { p.Resources[Resource.Type.Wood].Amount++; };
		}
		
		public void Seam(Player p)
		{
			p.Resources[Resource.Type.Stone].ResourceIncremented += delegate(object sender, EventArgs args) { p.Resources[Resource.Type.Ore].Amount++; };
		}
		
		public void Quarry(Player p)
		{
			p.town.BabyDonkeyAdded += delegate(object sender, EventArgs args) { p.Resources[Resource.Type.Ore].Amount++; };
		}
	}
}
