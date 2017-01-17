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
				p.Resources[ra.ResourceType].Amount += ra.Amount;
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
						d.player.Resources[Resource.Type.Wood].Amount++;
						break;
					case "Dog":
						break;
					case "Grain":
						d.player.Resources[Resource.Type.Grain].Amount++;
						break;
					case "Sheep":
						
						break;
					case "Stone":
						d.player.Resources[Resource.Type.Stone].Amount++;
						break;
					case "Donkey":
						
						break;
					case "Vegetable":
						d.player.Resources[Resource.Type.Vegetable].Amount++;
						break;
					case "Ores":
						d.player.Resources[Resource.Type.Ore].Amount += 2;
						break;
					case "Boar":
						
						break;
					case "Gold":
						d.player.Resources[Resource.Type.Gold].Amount += 2;
						break;
					case "FurnishCavern":
						
						break;
					case "Stable":
						
						break;
					case "Tunnel":
						
						break;
					case "Fence1":
						
						break;
					case "Cow":
						
						break;
					case "Meadow":
						d.player.town.AddTile(new Tile(Tile.Type.Meadow));
						break;
					case "FurnishDwelling":
						
						break;
					case "Field":
						d.player.town.AddTile(new Tile(Tile.Type.Field));
						break;
					case "Sow":
						
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
		}
							
		//----------------------------------------Action Cards------------------------------------------//
		
		public void Driftmining(Dwarf d)
		{
			ActionCardWindowContext acwc = new ActionCardWindowContext();
			acwc.Options.Add(new ActionCardCheckBox(){ Selected = true, Text = "Collect Resources"});
			acwc.Options.Add(new ActionCardCheckBox(){ Selected = true, Text = "Place Cave-Tunnel Tile"});
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
			acwc.Options.Add(new ActionCardCheckBox(){ Selected = true, Text = "Collect Resources"});
			acwc.Options.Add(new ActionCardCheckBox(){ Selected = true, Text = "Place Cave-Tunnel Tile"});
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
		
		public void Logging(Dwarf d)
		{
			ActionCardWindowContext acwc = new ActionCardWindowContext();
			acwc.Options.Add(new ActionCardCheckBox(){ Selected = true, Text = "Collect Resources", Able = true});
			bool expeditionAble = d.Level > 0;
			acwc.Options.Add(new ActionCardCheckBox(){ Selected = expeditionAble, Text = "Go on 1 Expedition", Able = expeditionAble});
			acwc.Control.ShowDialog();
			
			if((bool) acwc.Control.DialogResult)
			{
				if(acwc.Options[0].Selected)
				{
					foreach(ResourceAccumulator ra in Accumulators)
					{
						d.player.Resources[ra.ResourceType].Amount += ra.Amount;
						ra.Amount = 0;
					}
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
			acwc.Options.Add(new ActionCardCheckBox(){ Selected = true, Text = "Collect Resources"});
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
			acwc.Options.Add(new ActionCardCheckBox(){ Selected = true, Text = "Collect Resources"});
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
			acwc.Options.Add(new ActionCardCheckBox(){ Selected = true, Text = "Collect Resources"});
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
			acwc.Options.Add(new ActionCardCheckBox(){ Selected = true, Text = "Collect Resources and become the starting player"});
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
			acwc.Options.Add(new ActionCardCheckBox(){ Selected = true, Text = "Collect Resources"});
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
		
		public void Sustenance(Dwarf d)
		{
			ActionCardWindowContext acwc = new ActionCardWindowContext();
			acwc.Options.Add(new ActionCardCheckBox(){ Selected = true, Text = "Collect Resources"});
			acwc.Options.Add(new ActionCardCheckBox(){ Selected = true, Text = "Place Meadow-Field Tile"});
			acwc.Control.ShowDialog();
			
			if((bool) acwc.Control.DialogResult)
			{
				if(acwc.Options[0].Selected)
				{
					CollectAccumulatingResource(d.player);
					d.player.Resources[Resource.Type.Grain].Amount = 1;
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
			acwc.Options.Add(new ActionCardCheckBox(){ Selected = true, Text = "Collect Resources"});
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
			acwc.Options.Add(new ActionCardCheckBox(){ Selected = true, Text = "Collect Dog", Able = true});
			bool roomForCavern = d.player.town.HowManyBoardTilesOfType(BoardTile.Type.Cave) > 0;
			acwc.Options.Add(new ActionCardCheckBox(){ Selected = roomForCavern, Text = "Furnish a cavern", Able = roomForCavern});
			acwc.Control.ShowDialog();
			
			if((bool) acwc.Control.DialogResult)
			{
				if(acwc.Options[0].Selected)
				{
					d.player.town.AddTile(new Dog());
				}
				ActionBoardContext.Instance.readyForNextDwarf = true;
			}
		}
		
		public void Slashandburn(Dwarf d)
		{
			ActionCardWindowContext acwc = new ActionCardWindowContext();
			acwc.Options.Add(new ActionCardCheckBox(){ Selected = true, Text = "Place Meadow-Field Tile"});
			acwc.Options.Add(new ActionCardCheckBox(true){ Selected = true, Text = "... and sow crops"});
			acwc.Control.ShowDialog();
			
			if((bool) acwc.Control.DialogResult)
			{
				if(acwc.Options[0].Selected)
				{
					d.player.town.AddTile(new Tile(Tile.Type.MeadowField));
				}
				int numGrain = d.player.Resources[Resource.Type.Grain].Amount;
				int numVegetable = d.player.Resources[Resource.Type.Grain].Amount;
				d.player.town.AddTile(new Sowable(Sowable.Type.Grain));
				
				ActionBoardContext.Instance.readyForNextDwarf = true;
			}
		}
		
		public void Blacksmithing(Dwarf d)
		{
			ActionCardWindowContext acwc = new ActionCardWindowContext();
			if(d.Level == 0)
				acwc.Options.Add(new ActionCardCheckBox() {Selected = true, Text = "Forge a weapon and go on 3 expeditions"});
			acwc.Options.Add(new ActionCardCheckBox(true){Selected = true, Text = "Go on 3 expeditions"});
			//TODO: Warn player if he or she has no ores and his or her dwarf has no weapon
			acwc.Control.ShowDialog();
			
			if((bool) acwc.Control.DialogResult)
			{
				ActionBoardContext.Instance.readyForNextDwarf = true;
			}
		}
		
		public void Sheepfarming(Dwarf d)
		{
			ActionCardWindowContext acwc = new ActionCardWindowContext();
			int numWood = d.player.Resources[Resource.Type.Wood].Amount;
			bool hasMeadow = d.player.town.HowManyBoardTilesOfType(BoardTile.Type.Meadow) > 0;
			int numStone = d.player.Resources[Resource.Type.Stone].Amount;
			//TODO:must query boardtiles for adjacent meadows
			acwc.Options.Add(new ActionCardCheckBox() {Text = "Build small fenced area", Selected = numWood > 1 && hasMeadow, Able = numWood > 0});
			acwc.Options.Add(new ActionCardCheckBox() {Text = "Build large fenced area", Selected = numWood > 3, Able = numWood > 1});
			acwc.Options.Add(new ActionCardCheckBox() {Selected = true, Text = "Build a stable"});
			acwc.Options.Add(new ActionCardCheckBox(true){Selected = true, Text = "Collect sheep"});
			acwc.Control.ShowDialog();
			
			if((bool) acwc.Control.DialogResult)
			{
				if(acwc.Options[0].Selected)
				{
					d.player.Resources[Resource.Type.Wood].Amount-=2;
					d.player.town.AddTile(new Tile(Tile.Type.Fence));
				}
				if(acwc.Options[3].Selected)
				{
					d.player.town.AddTile(new FarmAnimal(FarmAnimal.Type.Sheep));
					d.player.town.AddTile(new FarmAnimal(FarmAnimal.Type.Sheep));
					d.player.town.AddTile(new FarmAnimal(FarmAnimal.Type.Sheep));
				}
				ActionBoardContext.Instance.readyForNextDwarf = true;
			}
		}
		
		public void Oremineconstruction(Dwarf d)
		{
			ActionCardWindowContext acwc = new ActionCardWindowContext();
			//TODO:must query boardtiles for adjacent tunnels
			acwc.Options.Add(new ActionCardCheckBox(){Text = "Build a ore mine"});
			acwc.Control.ShowDialog();
			
			if((bool) acwc.Control.DialogResult)
			{
				ActionBoardContext.Instance.readyForNextDwarf = true;
			}
		}
		
		public void Wishforchildren(Dwarf d)
		{
			ActionCardWindowContext acwc = new ActionCardWindowContext();
			acwc.Options.Add(new ActionCardRadioButton(new List<string>() {"Make a child", "Furnish a dwelling"}) {Selected = true});
			acwc.Control.ShowDialog();
			
			if((bool) acwc.Control.DialogResult)
			{
				if(acwc.Options[0].Selected)
				{
					Dwarf babyDwarf = new Dwarf() {X = d.X, Y = d.Y - 5, Locked = true }; babyDwarf.player = d.player;
					ActionBoardContext.Instance.Dwarfs.Add(babyDwarf);
				}
				ActionBoardContext.Instance.readyForNextDwarf = true;
			}
		}
		
		public void Donkeyfarming(Dwarf d)
		{
			ActionCardWindowContext acwc = new ActionCardWindowContext();
			int numWood = d.player.Resources[Resource.Type.Wood].Amount;
			bool hasMeadow = d.player.town.HowManyBoardTilesOfType(BoardTile.Type.Meadow) > 0;
			int numStone = d.player.Resources[Resource.Type.Stone].Amount;
			//TODO:must query boardtiles for adjacent meadows
			acwc.Options.Add(new ActionCardCheckBox() {Text = "Build small fenced area", Selected = numWood > 1 && hasMeadow, Able = numWood > 0});
			acwc.Options.Add(new ActionCardCheckBox() {Text = "Build large fenced area", Selected = numWood > 3, Able = numWood > 1});
			acwc.Options.Add(new ActionCardCheckBox() {Selected = numStone > 1, Able = numStone > 1, Text = "Build a stable"});
			acwc.Options.Add(new ActionCardCheckBox(true){Selected = true, Text = "Collect donkey"});
			acwc.Control.ShowDialog();
			
			if((bool) acwc.Control.DialogResult)
			{
				if(acwc.Options[0].Selected)
				{
					//d.player.Resources[Resource.Type.Wood].Amount-=2;
					d.player.town.AddTile(new Tile(Tile.Type.Fence));
				}
				if(acwc.Options[1].Selected)
				{
					//d.player.Resources[Resource.Type.Wood].Amount-= 4;
					d.player.town.AddTile(new Tile(Tile.Type.BigFence));
					d.player.town.AddTile(new Tile(Tile.Type.Meadow));
					d.player.town.AddTile(new Tile(Tile.Type.MeadowField));
					d.player.town.AddTile(new Tile(Tile.Type.MeadowField));
				}
				if(acwc.Options[3].Selected)
				{
					d.player.town.AddTile(new FarmAnimal(FarmAnimal.Type.Donkey));
					d.player.town.AddTile(new FarmAnimal(FarmAnimal.Type.Donkey));
					d.player.town.AddTile(new FarmAnimal(FarmAnimal.Type.Sheep));
					d.player.town.AddTile(new FarmAnimal(FarmAnimal.Type.Sheep));
					d.player.town.AddTile(new FarmAnimal(FarmAnimal.Type.Sheep));
					d.player.town.AddTile(new FarmAnimal(FarmAnimal.Type.Boar));
				}
				ActionBoardContext.Instance.readyForNextDwarf = true;
			}
		}
		
		public void Exploration(Dwarf d)
		{
			
		}
		
		public void Familylife(Dwarf d)
		{
			
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
