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
using System.Linq;
using CavernaWPF.Layables;
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
		
		public List<ResourceTab> Cost = new List<ResourceTab>();
		
		public Action<Player> Effect;
		
		public int roundsOfEffect;
		
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
		
		public void Carpenter(Player p)
		{
		}
		
		public void Stonecarver(Player p)
		{
			p.Resources[Resource.Type.Stone].Increment(2);
		}
		
		public void Blacksmith(Player p)
		{
			p.Resources[Resource.Type.Ore].Increment(2);
		}
		
		public void Miner(Player p)
		{
			ActionBoardContext.Instance.NewRound += delegate 
			{
				int numDonkey = p.town.Tiles.OfType<FarmAnimal>().Count(fa => fa.type == FarmAnimal.Type.Donkey);
				p.Resources[Resource.Type.Ore].Amount += numDonkey;
			};
		}
		
		public void Builder(Player p)
		{
		}
		
		public void Trader(Player p)
		{
		}
		
		public void Slaughteringcave(Player p)
		{
			ActionBoardContext.Instance.tradeManager.AnimalSlaughtered += delegate 
			{
				p.Resources[Resource.Type.Food].Amount += 1;
			};
		}
		
		public void Cookingcave(Player p)
		{
		}
		
		public void Workingcave(Player p)
		{
		}
		
		public void Miningcave(Player p)
		{
		}
		
		public void Breedingcave(Player p)
		{
		}
		
		public void Peacefulcave(Player p)
		{
		}
		
		public void Stonestorage(Player p)
		{
		}
		
		public void Orestorage(Player p)
		{
		}
		
		public void Sparepartstorage(Player p)
		{
		}
		
		public void Mainstorage(Player p)
		{
		}
		
		public void Weaponstorage(Player p)
		{
		}
		
		public void Suppliesstorage(Player p)
		{
		}
		
		public void Cuddleroom(Player p)
		{
			ActionBoardContext.Instance.NewRound += delegate(object sender, EventArgs args) 
			{
			};
		}
		
		public void Breakfastroom(Player p)
		{
			ActionBoardContext.Instance.NewRound += delegate(object sender, EventArgs args) 
			{
			};
		}
				
		public void Stubbleroom(Player p)
		{
			ActionBoardContext.Instance.NewRound += delegate(object sender, EventArgs args) 
			{
			};
		}
		
		public void Workroom(Player p)
		{
			ActionBoardContext.Instance.NewRound += delegate(object sender, EventArgs args) 
			{
			};
		}
		
		public void Guestroom(Player p)
		{
			//No effect
		}
		
		public void Officeroom(Player p)
		{
			//No effect
		}
		
		public void Woodsupplier(Player p)
		{
			ActionBoardContext.Instance.NewRound += delegate(object sender, EventArgs args) 
			{
				if(roundsOfEffect > 0)
				{
					p.Resources[Resource.Type.Wood].Increment(1);
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
			ActionBoardContext.Instance.NewRound += Stonesupplier;
		}
		
		private void Stonesupplier(object sender, EventArgs args) 
		{
			if(roundsOfEffect > 0)
			{
				if(player != null)
				{
					player.Resources[Resource.Type.Stone].Increment(1);
					roundsOfEffect--;
				}
			}
			else
			{
				//Unhook
			}
		}
		
		public void Rubysupplier(Player p)
		{
			ActionBoardContext.Instance.NewRound += delegate(object sender, EventArgs args) 
			{ 
				if(roundsOfEffect > 0)
				{
					p.Resources[Resource.Type.Ruby].Increment(1);
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
		
		public void Weavingparlor(Player p)
		{
			var numSheep = p.town.Tiles.OfType<FarmAnimal>().Count(fa => fa.type == FarmAnimal.Type.Sheep);
			p.Resources[Resource.Type.Food].Amount += numSheep;
		}
		
		public void Milkingparlor(Player p)
		{
			var numCow = p.town.Tiles.OfType<FarmAnimal>().Count(fa => fa.type == FarmAnimal.Type.Cow);
			p.Resources[Resource.Type.Food].Amount += numCow;
		}
		
		public void Stateparlor(Player p)
		{
			p.Resources[Resource.Type.Food].Amount += 0;
		}
		
		public void Huntingparlor(Player p)
		{
			//No effect
		}
		
		public void Beerparlor(Player p)
		{
			//No effect
		}
		
		public void Blacksmithingparlor(Player p)
		{
			//No effect
		}
		
		public void Broomchamber(Player p)
		{
			//No effect
		}
		
		public void Treasurechamber(Player p)
		{
			//No effect
		}
		
		public void Foodchamber(Player p)
		{
			//No effect
		}
		
		public void Prayerchamber(Player p)
		{
			//No effect
		}
		
		public void Writingchamber(Player p)
		{
			//No effect
		}
		
		public void Fodderchamber(Player p)
		{
			//No effect
		}
	}
}
