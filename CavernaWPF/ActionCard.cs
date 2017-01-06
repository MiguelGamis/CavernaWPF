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
		public Action<Player> PlayerAction;
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
		
		public void DriftMining(Player p)
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
						p.town.Resources[ra.ResourceType].Amount += ra.Amount;
						ra.Amount = 0;
					}
				}
				
				if(acwc.Options[1].Selected)
				{
					p.town.Tiles.Add(new Tile(Tile.Type.CaveTunnel));
					
					ActionBoardContext.Instance.readyForNextDwarf = true;
				}
			}
		}
		
		public void Excavation(Player p)
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
						p.town.Resources[ra.ResourceType].Amount += ra.Amount;
						ra.Amount = 0;
					}
				}
				
				if(acwc.Options[1].Selected)
				{
					p.town.Tiles.Add(new Tile(Tile.Type.CaveTunnel));
					
					ActionBoardContext.Instance.readyForNextDwarf = true;
				}
			}
		}
		
		public void Logging(Player p)
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
						p.town.Resources[ra.ResourceType].Amount += ra.Amount;
						ra.Amount = 0;
					}
				}
				
				if(acwc.Options[1].Selected)
				{
					Expedition exp = new Expedition();
					exp.Control.ShowDialog();
					List<String> test = exp.Loot;
				}
			}
		}
	}
}
