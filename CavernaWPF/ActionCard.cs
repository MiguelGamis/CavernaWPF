/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 12/23/2016
 * Time: 12:38
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using CavernaWPF.Layables;
using CavernaWPF.Resources;
using CavernaWPF.ActionCardControls;
using System.Windows;
using System.Windows.Controls;

namespace CavernaWPF
{
	/// <summary>
	/// Description of ActionCard.
	/// </summary>
	public class ActionCard
	{
		public Action<Dwarf> PlayerAction;
		
		//TODO: Consider using a Dictionary to be able to see how many of a specific resource
		public List<ResourceAccumulator> Accumulators = new List<ResourceAccumulator>();
		
		public ActionCard()
		{
		}
		
		public string Name
		{
			get;
			set;
		}
		
		public int Stage
		{
			get;
			set;
		}
		
		public void Accumulate()
		{
			foreach(ResourceAccumulator ra in Accumulators)
			{
				if(ra.Amount > 0)
					ra.Amount += ra.Accumulation;
				else
					ra.Amount = ra.StartingAmount;
			}
		}
		
		//----------------------------------------Action Card Helpers------------------------------------------//
		
		private void CollectAccumulatingResource(Player p)
		{
			foreach(ResourceAccumulator ra in Accumulators)
			{
				p.Resources[ra.ResourceType].Increment(ra.Amount);
				ra.Amount = 0;
			}
		}
		
		private void Expedition(Dwarf d, int count)
		{
			Expedition exp = new Expedition(d.Level, count);
			exp.Control.ShowDialog();
			List<String> loot = exp.Loot;
			
			foreach(String str in loot)
			{
				switch(str)
				{
					case "Wood":
						d.player.Resources[Resource.Type.Wood].Increment(1);
						break;
					case "Dog":
						d.player.town.AddTile(new Dog());
						break;
					case "Grain":
						d.player.Resources[Resource.Type.Grain].Increment(1);
						break;
					case "Sheep":
						d.player.town.AddTile(new FarmAnimal(FarmAnimal.Type.Sheep));
						break;
					case "Stone":
						d.player.Resources[Resource.Type.Stone].Increment(1);
						break;
					case "Donkey":
						d.player.town.AddTile(new FarmAnimal(FarmAnimal.Type.Donkey));
						break;
					case "Vegetable":
						d.player.Resources[Resource.Type.Vegetable].Amount++;
						break;
					case "Ores":
						d.player.Resources[Resource.Type.Ore].Increment(2);
						break;
					case "Boar":
						d.player.town.AddTile(new FarmAnimal(FarmAnimal.Type.Boar));
						break;
					case "Gold":
						d.player.Resources[Resource.Type.Gold].Increment(2);
						break;
					case "FurnishCavern":
						ActionBoardContext.Instance.FurnishingCavern = true;
						break;
					case "Stable":
						d.player.town.AddTile(new Stable(d.player.Color));
						break;
					case "Tunnel":
						d.player.town.AddTile(new Tile(Tile.Type.Tunnel));
						break;
					case "Fence":
						d.player.town.AddTile(new Tile(Tile.Type.Fence));
						break;
					case "BigFence":
						d.player.town.AddTile(new Tile(Tile.Type.BigFence));
						break;
					case "Cow":
						d.player.town.AddTile(new FarmAnimal(FarmAnimal.Type.Cow));
						break;
					case "Meadow":
						d.player.town.AddTile(new Tile(Tile.Type.Meadow));
						break;
					case "FurnishDwelling":
						ActionBoardContext.Instance.FurnishingDwelling = true;
						break;
					case "Field":
						d.player.town.AddTile(new Tile(Tile.Type.Field));
						break;
					case "Sow":
						Sow(d);
						break;
					case "Cavern":
						d.player.town.AddTile(new Tile(Tile.Type.Cave));
						break;
					case "Breed":
						
						break;
					default:
						break;
				}
			}
			d.Level = Math.Min(d.Level + 1, 14);
		}
		
		private void Sow(Dwarf d)
		{
			ActionBoardContext.Instance.Sow = true;
			
			int numGrain = d.player.Resources[Resource.Type.Grain].Amount;
			int numVegetable = d.player.Resources[Resource.Type.Grain].Amount;
			
			//hardcode
			for(int i = 0; i < Math.Min(numGrain, 2); i++)
				d.player.town.AddTile(new Sowable(Sowable.Type.Grain){X=220+35*6, Y=420, row=0, column=0});
			//hardcode
			for(int i = 0; i < Math.Min(numVegetable, 2); i++)
				d.player.town.AddTile(new Sowable(Sowable.Type.Vegetable){X=220+35*7, Y=420, row=0, column=0});
		}
		
		private void MakeBaby(Dwarf d)
		{
			Dwarf babyDwarf = new Dwarf() {X = d.X, Y = d.Y - 5, Locked = true }; babyDwarf.player = d.player;
			ActionBoardContext.Instance.Dwarfs.Add(babyDwarf);
		}
		
		private void FurnishDwelling(Dwarf d)
		{
			
		}
		//----------------------------------------Action Cards------------------------------------------//
		
		public void Driftmining(Dwarf d)
		{
			ActionCardWindowContext acwc = new ActionCardWindowContext();
			
			acwc.Options.Add(new ActionCardCheckBox(){ Selected = true, Able = true, Text = GetResourcesOption()});
			
			acwc.Options.Add(new ActionCardCheckBox(){ Selected = true, Able = true, Text = "Place Cave-Tunnel Tile"});
			
			acwc.Control.ShowDialog();
			
			if((bool) acwc.Control.DialogResult)
			{
				if(acwc.Options[0].Selected)
				{
					CollectAccumulatingResource(d.player);
				}
				
				if(acwc.Options[1].Selected)
				{
					d.player.town.AddTile(new Tile(Tile.Type.CaveTunnel));
				}
				ActionBoardContext.Instance.readyForNextDwarf = true;
			}
		}
		
		public void Excavation(Dwarf d)
		{
			ActionCardWindowContext acwc = new ActionCardWindowContext();
			
			acwc.Options.Add(new ActionCardCheckBox(){ Selected = true, Able = true, Text = GetResourcesOption()});
			
			acwc.Options.Add(new ActionCardRadioButton(true){ Selected = true, Able = true, Text = "Place a Cave-Tunnel Tile"});
			
			acwc.Options.Add(new ActionCardRadioButton(true){ Able = true, Text = "Place a Cave-Cave Tile"});
			
			acwc.Control.ShowDialog();
			
			if((bool) acwc.Control.DialogResult)
			{
				if(acwc.Options[0].Selected)
				{
					CollectAccumulatingResource(d.player);
				}
				
				if(acwc.Options[1].Selected)
				{
					d.player.town.AddTile(new Tile(Tile.Type.CaveTunnel));
				}
				
				if(acwc.Options[2].Selected)
				{
					d.player.town.AddTile(new Tile(Tile.Type.CaveCave));
				}
				
				ActionBoardContext.Instance.readyForNextDwarf = true;
			}
		}
		
		public void Logging(Dwarf d)
		{
			ActionCardWindowContext acwc = new ActionCardWindowContext();
			
			acwc.Options.Add(new ActionCardCheckBox(){ Selected = true, Able = true, Text = GetResourcesOption() });
			
			bool expeditionAble = d.Level > 0;
			acwc.Options.Add(new ActionCardCheckBox(){ Selected = expeditionAble, Able = expeditionAble, Text = "Go on 1 Expedition"});
			
			acwc.Control.ShowDialog();
			
			if((bool) acwc.Control.DialogResult)
			{
				if(acwc.Options[0].Selected)
				{
					CollectAccumulatingResource(d.player);
				}
				
				if(acwc.Options[1].Selected)
				{
					Expedition(d, 1);
				}
				ActionBoardContext.Instance.readyForNextDwarf = true;
			}
		}
		
		//DONE
		public void Woodgathering(Dwarf d)
		{
			ActionCardWindowContext acwc = new ActionCardWindowContext();
			acwc.Options.Add(new ActionCardCheckBox(){ Selected = true, Text = GetResourcesOption()});
			acwc.Control.ShowDialog();
			
			if((bool) acwc.Control.DialogResult)
			{
				if(acwc.Options[0].Selected)
				{
					CollectAccumulatingResource(d.player);
				}
				ActionBoardContext.Instance.readyForNextDwarf = true;
			}
		}
		
		//DONE
		public void Supplies(Dwarf d)
		{
			ActionCardWindowContext acwc = new ActionCardWindowContext();
			acwc.Options.Add(new ActionCardCheckBox(){ Selected = true, Text = "Collect 1 Wood, 1 Stone, 1 Ore, 1 Food, and 2 Gold"});
			acwc.Control.ShowDialog();
			
			if((bool) acwc.Control.DialogResult)
			{
				if(acwc.Options[0].Selected)
				{
					d.player.Resources[Resource.Type.Wood].Amount++;
					d.player.Resources[Resource.Type.Stone].Amount++;
					d.player.Resources[Resource.Type.Ore].Amount++;
					d.player.Resources[Resource.Type.Food].Amount++;
					d.player.Resources[Resource.Type.Gold].Amount+=2;
				}
				ActionBoardContext.Instance.readyForNextDwarf = true;
			}
		}
		
		public bool Clearing(Dwarf d)
		{
			ActionCardWindowContext acwc = new ActionCardWindowContext();
			acwc.Options.Add(new ActionCardCheckBox(){ Selected = true, Text = GetResourcesOption()});
			acwc.Options.Add(new ActionCardCheckBox(){ Selected = true, Text = "Place Meadow-Field Tile"});
			acwc.Control.ShowDialog();
			
			if((bool) acwc.Control.DialogResult)
			{
				if(acwc.Options[0].Selected)
				{
					CollectAccumulatingResource(d.player);
				}
				
				if(acwc.Options[1].Selected)
				{
					d.player.town.AddTile(new Tile(Tile.Type.MeadowField));
				}
				ActionBoardContext.Instance.readyForNextDwarf = true;
			}
			return false;
		}
		
		//DONE
		public void Startingplayer(Dwarf d)
		{
			ActionCardWindowContext acwc = new ActionCardWindowContext();
			acwc.Options.Add(new ActionCardCheckBox(){ Selected = true, Text =  GetResourcesOption() + " and become the starting player"});
			acwc.Control.ShowDialog();
			
			if((bool) acwc.Control.DialogResult)
			{
				if(acwc.Options[0].Selected)
				{
					CollectAccumulatingResource(d.player);
					ActionBoardContext.Instance.StartingPlayer = d.player;
					d.player.Resources[Resource.Type.Ore].Amount+=2;
				}
				ActionBoardContext.Instance.readyForNextDwarf = true;
			}
		}
		
		public void Oremining(Dwarf d)
		{
			ActionCardWindowContext acwc = new ActionCardWindowContext();
			acwc.Options.Add(new ActionCardCheckBox(){ Selected = true, Text = GetResourcesOption()});
			acwc.Control.ShowDialog();
			
			if((bool) acwc.Control.DialogResult)
			{
				if(acwc.Options[0].Selected)
				{
					CollectAccumulatingResource(d.player);
				}
				ActionBoardContext.Instance.readyForNextDwarf = true;
			}
		}
		
		private string GetResourcesOption(bool more = false)
		{
			string option = "Collect ";
			int numTypes = Accumulators.Count;
			int i = 0;
			foreach(ResourceAccumulator ra in Accumulators)
			{
				option += ra.Amount + " " + ra.ResourceType;
				i++;
				if(i == numTypes)
					break;
				else if(i == numTypes - 1 && !more)
					option += " and ";
				else
					option += ", ";
			}
			return option;
		}
		
		public void Sustenance(Dwarf d)
		{
			ActionCardWindowContext acwc = new ActionCardWindowContext();
			acwc.Options.Add(new ActionCardCheckBox(){ Selected = true, Text = GetResourcesOption(true) + " and 1 Grain"});
			acwc.Options.Add(new ActionCardCheckBox(){ Selected = true, Text = "Place Meadow-Field Tile"});
			acwc.Control.ShowDialog();
			
			if((bool) acwc.Control.DialogResult)
			{
				if(acwc.Options[0].Selected)
				{
					CollectAccumulatingResource(d.player);
					d.player.Resources[Resource.Type.Grain].Amount++;
				}
				if(acwc.Options[1].Selected)
				{
					d.player.town.AddTile(new Tile(Tile.Type.MeadowField));
				}
				ActionBoardContext.Instance.readyForNextDwarf = true;
			}
		}
		
		public void Rubymining(Dwarf d)
		{
			ActionCardWindowContext acwc = new ActionCardWindowContext();
			acwc.Options.Add(new ActionCardCheckBox(){ Selected = true, Text = GetResourcesOption()});
			acwc.Control.ShowDialog();
			
			if((bool) acwc.Control.DialogResult)
			{
				if(acwc.Options[0].Selected)
				{
					CollectAccumulatingResource(d.player);
				}
				ActionBoardContext.Instance.readyForNextDwarf = true;
			}
		}
		
		public void Housework(Dwarf d)
		{
			ActionCardWindowContext acwc = new ActionCardWindowContext();
			acwc.Options.Add(new ActionCardCheckBox(){ Selected = true, Text = "Collect 1 Dog", Able = true});
			bool roomForCavern = true;
			acwc.Options.Add(new ActionCardCheckBox(){ Selected = roomForCavern, Text = "Furnish a cavern", Able = roomForCavern});
			acwc.Control.ShowDialog();
			
			if((bool) acwc.Control.DialogResult)
			{
				if(acwc.Options[0].Selected)
				{
					d.player.town.AddTile(new Dog());
				}
				
				if(acwc.Options[1].Selected)
				{
					ActionBoardContext.Instance.FurnishingCavern = true;
				}
				
				ActionBoardContext.Instance.readyForNextDwarf = true;
			}
		}
		
		public void Slashandburn(Dwarf d)
		{
			ActionCardWindowContext acwc = new ActionCardWindowContext();
			acwc.Options.Add(new ActionCardCheckBox(){ Selected = true, Text = "Place Meadow-Field Tile"});
			int numGrain = d.player.Resources[Resource.Type.Grain].Amount;
			int numVegetable = d.player.Resources[Resource.Type.Grain].Amount;
			bool ableToSow = numGrain + numVegetable > 0;
			acwc.Options.Add(new ActionCardCheckBox(true){ Selected = ableToSow, Able = ableToSow, Text = "... and sow crops"});
			acwc.Control.ShowDialog();
			
			if((bool) acwc.Control.DialogResult)
			{
				if(acwc.Options[0].Selected)
				{
					d.player.town.AddTile(new Tile(Tile.Type.MeadowField));
				}
				if(acwc.Options[1].Selected)
				{
					Sow(d);
				}
				
				ActionBoardContext.Instance.readyForNextDwarf = true;
			}
		}
		
		public void Blacksmithing(Dwarf d)
		{
			ActionCardWindowContext acwc = new ActionCardWindowContext();
			int numOre = d.player.Resources[Resource.Type.Ore].Amount;
			bool isWarrior = d.Level > 0;
			if(isWarrior)
			{
				acwc.Options.Add(new ActionCardCheckBox(true){Selected = true, Able = true, Text = "Go on 3 expeditions"});
			}
			else
			{
				acwc.Options.Add(new ActionCardTicker(0, Math.Min(numOre, 8) ) {Selected = !isWarrior && numOre > 0, Able = !isWarrior && numOre > 0, Text = "Forge weapon and go on 3 expeditions"});
			}
			//TODO: Warn player if he or she has no ores and his or her dwarf has no weapon
			acwc.Control.ShowDialog();
			
			if((bool) acwc.Control.DialogResult)
			{
				if(acwc.Options[0].Selected)
				{
					if(acwc.Options[0] is ActionCardCheckBox)
					{
						Expedition(d,3);
					}
					else if(acwc.Options[0] is ActionCardTicker)
					{
						ActionCardTicker act = acwc.Options[0] as ActionCardTicker;
						int gainedLevel = act.Value;
						d.player.Resources[Resource.Type.Ore].Amount-=gainedLevel;
						d.Level = gainedLevel;
						Expedition(d,3);
					}
				}
				ActionBoardContext.Instance.readyForNextDwarf = true;
			}
		}
		
		public void Sheepfarming(Dwarf d)
		{
			ActionCardWindowContext acwc = new ActionCardWindowContext();
			int numWood = d.player.Resources[Resource.Type.Wood].Amount;
			bool hasMeadow = true;
			int numStone = d.player.Resources[Resource.Type.Stone].Amount;
			//TODO:must query boardtiles for adjacent meadows
			acwc.Options.Add(new ActionCardCheckBox() {Text = "Build small fenced area", Selected = numWood > 1 && hasMeadow, Able = numWood > 0});
			acwc.Options.Add(new ActionCardCheckBox() {Text = "Build large fenced area", Selected = numWood > 3, Able = numWood > 1});
			acwc.Options.Add(new ActionCardCheckBox() {Selected = numStone > 1, Able = numStone > 1, Text = "Build a stable"});
			acwc.Options.Add(new ActionCardCheckBox(true){Selected = true, Text = GetResourcesOption()});
			acwc.Control.ShowDialog();
			
			if((bool) acwc.Control.DialogResult)
			{
				if(acwc.Options[0].Selected)
				{
					d.player.Resources[Resource.Type.Wood].Amount-=2;
					d.player.town.AddTile(new Tile(Tile.Type.Fence));
				}
				if(acwc.Options[1].Selected)
				{
					d.player.Resources[Resource.Type.Wood].Amount-= 4;
					d.player.town.AddTile(new Tile(Tile.Type.BigFence));
				}
				if(acwc.Options[2].Selected)
				{
					d.player.Resources[Resource.Type.Stone].Amount-=1;
					d.player.town.AddTile(new Stable(d.player.Color));
				}
				if(acwc.Options[3].Selected)
				{
					//TODO: Make more robust
					foreach(ResourceAccumulator ra in Accumulators)
					{
						for(int i = 0; i < ra.Amount; i++)
						{
							d.player.town.AddTile(new FarmAnimal(FarmAnimal.Type.Sheep));
						}
						ra.Amount = 0;
					}
				}
				ActionBoardContext.Instance.readyForNextDwarf = true;
			}
		}
		
		public void Oremineconstruction(Dwarf d)
		{
			ActionCardWindowContext acwc = new ActionCardWindowContext();
			//TODO:must query boardtiles for adjacent tunnels
			acwc.Options.Add(new ActionCardCheckBox(){Selected = true, Able = true, Text = "Build an ore mine and collect 3 Ore"});
			bool expeditionAble = d.Level > 0;
			acwc.Options.Add(new ActionCardCheckBox(){Selected = expeditionAble, Able = expeditionAble, Text = "Go on 2 expeditions"});
			acwc.Control.ShowDialog();
			
			if((bool) acwc.Control.DialogResult)
			{
				if(acwc.Options[0].Selected)
				{
					d.player.town.AddTile(new Tile(Tile.Type.OreMine));
					d.player.Resources[Resource.Type.Ore].Increment(3);
				}
				if(acwc.Options[1].Selected)
				{
					Expedition(d,2);
				}
				ActionBoardContext.Instance.readyForNextDwarf = true;
			}
		}
		
		public void Wishforchildren(Dwarf d)
		{
			ActionCardWindowContext acwc = new ActionCardWindowContext();
			if(ActionBoardContext.Instance.FurnishingTiles["Guest room"].player == d.player)
			{
				acwc.Options.Add(new ActionCardCheckBox() {Selected = true, Able = true, Text = "Furnish a dwelling"} );
			
				acwc.Options.Add(new ActionCardCheckBox() {Able = true, Text = "Make a baby"} );
			}
			else
			{
				acwc.Options.Add(new ActionCardRadioButton() {Selected = true, Able = true, Text = "Furnish a dwelling"} );
			
				acwc.Options.Add(new ActionCardRadioButton() {Able = true, Text = "Make a baby"} );
			}
			
			acwc.Control.ShowDialog();
			
			if((bool) acwc.Control.DialogResult)
			{
				if(acwc.Options[0].Selected)
				{
					FurnishDwelling(d);
				}
				if(acwc.Options[1].Selected)
				{
					MakeBaby(d);
				}
				ActionBoardContext.Instance.readyForNextDwarf = true;
			}
		}
		
		public void Urgentwishforchildren(Dwarf d)
		{
			ActionCardWindowContext acwc = new ActionCardWindowContext();
			if(ActionBoardContext.Instance.FurnishingTiles["Guest room"].player == d.player)
			{
				acwc.Options.Add(new ActionCardCheckBox() {Selected = true, Able = true, Text = "Furnish a dwelling and then make a baby"} );
			
				acwc.Options.Add(new ActionCardCheckBox() {Able = true, Text = "Collect 3 Gold"} );
			}
			else
			{
				acwc.Options.Add(new ActionCardRadioButton() {Selected = true, Able = true, Text = "Furnish a dwelling and then make a baby"} );
			
				acwc.Options.Add(new ActionCardRadioButton() {Able = true, Text = "Collect 3 Gold"} );
			}
			
			acwc.Control.ShowDialog();
			
			if((bool) acwc.Control.DialogResult)
			{
				if(acwc.Options[0].Selected)
				{
					FurnishDwelling(d);
					MakeBaby(d);
				}
				if(acwc.Options[1].Selected)
				{
					d.player.Resources[Resource.Type.Gold].Increment(3);
				}
				ActionBoardContext.Instance.readyForNextDwarf = true;
			}
		}
		
		public void Donkeyfarming(Dwarf d)
		{
			ActionCardWindowContext acwc = new ActionCardWindowContext();
			int numWood = d.player.Resources[Resource.Type.Wood].Amount;
			bool hasMeadow = true;
			int numStone = d.player.Resources[Resource.Type.Stone].Amount;
			//TODO:must query boardtiles for adjacent meadows
			acwc.Options.Add(new ActionCardCheckBox() {Text = "Build small fenced area", Selected = numWood > 1 && hasMeadow, Able = numWood > 0});
			acwc.Options.Add(new ActionCardCheckBox() {Text = "Build large fenced area", Selected = numWood > 3, Able = numWood > 1});
			acwc.Options.Add(new ActionCardCheckBox() {Selected = numStone > 1, Able = numStone > 1, Text = "Build a stable"});
			acwc.Options.Add(new ActionCardCheckBox(true){Selected = true, Text = GetResourcesOption()});
			acwc.Control.ShowDialog();
			
			if((bool) acwc.Control.DialogResult)
			{
				if(acwc.Options[0].Selected)
				{
					d.player.Resources[Resource.Type.Wood].Amount-=2;
					d.player.town.AddTile(new Tile(Tile.Type.Fence));
				}
				if(acwc.Options[1].Selected)
				{
					d.player.Resources[Resource.Type.Wood].Amount-= 4;
					d.player.town.AddTile(new Tile(Tile.Type.BigFence));
				}
				if(acwc.Options[2].Selected)
				{
					d.player.Resources[Resource.Type.Stone].Amount-=1;
					d.player.town.AddTile(new Stable(d.player.Color));
				}
				if(acwc.Options[3].Selected)
				{
					//TODO: Make more robust
					foreach(ResourceAccumulator ra in Accumulators)
					{
						for(int i = 0; i < ra.Amount; i++)
						{
							d.player.town.AddTile(new FarmAnimal(FarmAnimal.Type.Sheep));
						}
						ra.Amount = 0;
					}
				}
				ActionBoardContext.Instance.readyForNextDwarf = true;
			}
		}
		
		public void Exploration(Dwarf d)
		{
			ActionCardWindowContext acwc = new ActionCardWindowContext();
			bool expeditionAble = d.Level > 0;
			acwc.Options.Add(new ActionCardCheckBox(){ Selected = expeditionAble, Able = expeditionAble, Text = "Go on 4 expeditions"});
			if((bool) acwc.Control.DialogResult)
			{
				if(acwc.Options[0].Selected)
				{
					Expedition(d, 4);
				}
				ActionBoardContext.Instance.readyForNextDwarf = true;
			}
		}
		
		public void Familylife(Dwarf d)
		{
			ActionCardWindowContext acwc = new ActionCardWindowContext();
			
			acwc.Options.Add(new ActionCardCheckBox(){ Selected = true, Able = true, Text = "Make a baby"});
			
			acwc.Options.Add(new ActionCardCheckBox(true){ Selected = true, Able = true, Text = "... and sow crops"});
			
			if((bool) acwc.Control.DialogResult)
			{
				if(acwc.Options[0].Selected)
				{
					
				}
				ActionBoardContext.Instance.readyForNextDwarf = true;
			}
		}
		
		public void Oredelivery(Dwarf d)
		{
			
		}
		
		public void Rubymineconstruction(Dwarf d)
		{
			
		}
		
		public void Adventure(Dwarf d)
		{
			
		}
		
		public void Oretrading(Dwarf d)
		{
			
		}
		
		public void Rubydelivery(Dwarf d)
		{
			
		}
	}
}
