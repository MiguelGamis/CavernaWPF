/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 1/29/2017
 * Time: 3:20 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace CavernaWPF
{
	/// <summary>
	/// Description of PrepareGameContext.
	/// </summary>
	public class PrepareGameContext : INotifyPropertyChanged
	{	
		public PrepareGameWindow Control;
		
		private List<PlayerSlot> playerSlots;
		
		public List<PlayerSlot> PlayerSlots
		{
			get {return playerSlots;}
			set {
				playerSlots = value;
				if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("PlayerSlots"));
			}
		}
		
		public PrepareGameContext()
		{
			Control = new PrepareGameWindow();
			Control.DataContext = this;
			
			PlayerSlots = new List<PlayerSlot>()
			{
				new PlayerSlot(){Name = "Player 1"},
				new PlayerSlot(){Name = "Player 2"},
				new PlayerSlot(){Name = "Player 3"},
				new PlayerSlot(){Name = "Player 4"},
				new PlayerSlot(){Name = "Player 5"},
				new PlayerSlot(){Name = "Player 6"},
				new PlayerSlot(){Name = "Player 7"}
			};
			
			int i = 0;
			foreach(PlayerSlot ps in PlayerSlots)
			{
				if(i == 3) break;
				ps.SelectedTrue += PlayerSlotSelectionChange; 
				ps.Enabled = true;
				i++;
			}
		}
		
		private ObservableCollection<string> colorOptions = new ObservableCollection<string>() {
			"Red",
			"Blue",
			"Yellow",
			"Green",
			"Purple",
			"Black"
		};
		
		private Dictionary<PlayerSlot, string> colorSelections = new Dictionary<PlayerSlot, string>();
		
		public void PlayerSlotSelectionChange(object sender, EventArgs args)
		{
			PlayerSlot ps = sender as PlayerSlot;
			if(ps.Selected)
			{
				UpdateColorOptions(ps);
				ps.Color = ps.ColorOptions.First();
				if(!PlayerSlots.Any(p => p.StartingPlayer))
					PlayerSlots.Find(p => p.Selected).StartingPlayer = true;
			}
			else 
			{
				//ps.Color = null;
				colorSelections.Remove(ps);
				var otherps = PlayerSlots.Where(p => p.Selected).First();
				if(otherps != null) otherps.StartingPlayer = true;
			}
		}
		
		public void UpdateColor(PlayerSlot playerslot, string color)
		{
			if(colorSelections.ContainsKey(playerslot))
				colorSelections[playerslot] = color;
			else
				colorSelections.Add(playerslot, color);
		}
		
		public void UpdateColorOptions(PlayerSlot playerslot)
		{
			ObservableCollection<string> updatedColorOptions = new ObservableCollection<string>(colorOptions);
			foreach(var colorSelection in colorSelections)
			{
				if(colorSelection.Key != playerslot)
					updatedColorOptions.Remove(colorSelection.Value);
			}
			playerslot.ColorOptions = updatedColorOptions;
		}
		
		public event PropertyChangedEventHandler PropertyChanged;
	}
}
