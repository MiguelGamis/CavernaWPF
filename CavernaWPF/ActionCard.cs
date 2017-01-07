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
using CavernaWPF.Layable;
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
		private string name;
		public Action<Dwarf> PlayerAction;
		public List<ResourceAccumulator> Accumulators = new List<ResourceAccumulator>();
		
		
		public ActionCard()
		{
		}
		
		public string Name
		{
			get { return name; }
			set { name = value; }
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
		
		//----------------------------------------------------------------------------------//
		
		public void DriftMining(Dwarf d)
		{
			ActionCardWindowContext acwc = new ActionCardWindowContext();
			acwc.Options.Add(new ActionCardOption(){ Selected = true, Text = "Collect Resources"});
			acwc.Options.Add(new ActionCardOption(){ Selected = true, Text = "Place Cave-Tunnel Tile"});
			acwc.Control.ShowDialog();
			
			if((bool) acwc.Control.DialogResult)
			{
				if(acwc.Options[0].Selected)
				{
					foreach(ResourceAccumulator ra in Accumulators)
					{
						d.player.town.Resources[ra.ResourceType].Amount += ra.Amount;
						ra.Amount = 0;
					}
				}
				
				if(acwc.Options[1].Selected)
				{
					d.player.town.Tiles.Add(new Tile(Tile.Type.CaveTunnel));
					
					ActionBoardContext.Instance.readyForNextDwarf = true;
				}
			}
		}
		
		public void Excavation(Dwarf d)
		{
			ActionCardWindowContext acwc = new ActionCardWindowContext();
			acwc.Options.Add(new ActionCardOption(){ Selected = true, Text = "Collect Resources"});
			acwc.Options.Add(new ActionCardOption(){ Selected = true, Text = "Place Cave-Tunnel Tile"});
			acwc.Control.ShowDialog();
			
			if((bool) acwc.Control.DialogResult)
			{
				if(acwc.Options[0].Selected)
				{
					foreach(ResourceAccumulator ra in Accumulators)
					{
						d.player.town.Resources[ra.ResourceType].Amount += ra.Amount;
						ra.Amount = 0;
					}
				}
				
				if(acwc.Options[1].Selected)
				{
					d.player.town.Tiles.Add(new Tile(Tile.Type.CaveTunnel));
					
					ActionBoardContext.Instance.readyForNextDwarf = true;
				}
			}
		}
		
		public void Logging(Dwarf d)
		{
			ActionCardWindowContext acwc = new ActionCardWindowContext();
			acwc.Options.Add(new ActionCardOption(){ Selected = true, Text = "Collect Resources"});
			acwc.Options.Add(new ActionCardOption(){ Selected = true, Text = "Go on 1 Expedition"});
			acwc.Control.ShowDialog();
			
			if((bool) acwc.Control.DialogResult)
			{
				if(acwc.Options[0].Selected)
				{
					foreach(ResourceAccumulator ra in Accumulators)
					{
						d.player.town.Resources[ra.ResourceType].Amount += ra.Amount;
						ra.Amount = 0;
					}
				}
				
				if(acwc.Options[1].Selected)
				{
					Expedition exp = new Expedition(5, 1);
					exp.Control.ShowDialog();
					List<String> loot = exp.Loot;
					
					foreach(String str in loot)
					{
						switch(str)
						{
							case "Wood":
								d.player.town.Resources[Resource.Type.Wood].Amount++;
								break;
							case "Dog":
								break;
							case "Grain":
								d.player.town.Resources[Resource.Type.Grain].Amount++;
								break;
							case "Sheep":
								
								break;
							case "Stone":
								d.player.town.Resources[Resource.Type.Stone].Amount++;
								break;
							case "Donkey":
								
								break;
							case "Vegetable":
								d.player.town.Resources[Resource.Type.Vegetable].Amount++;
								break;
							case "Ores":
								d.player.town.Resources[Resource.Type.Ore].Amount += 2;
								break;
							case "Boar":
								
								break;
							case "Gold":
								d.player.town.Resources[Resource.Type.Gold].Amount += 2;
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
								d.player.town.Tiles.Add(new Tile(Tile.Type.Meadow));
								break;
							case "FurnishDwelling":
								
								break;
							case "Field":
								d.player.town.Tiles.Add(new Tile(Tile.Type.Field));
								break;
							case "Sow":
								
								break;
							case "Cavern":
								d.player.town.Tiles.Add(new Tile(Tile.Type.Cave));
								break;
							case "Breed":
								
								break;
							default:
								break;
						}
					}
				}
			}
		}
	}
}
