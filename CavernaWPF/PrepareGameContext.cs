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
				ps.SelectedTrue += delegate(object sender, EventArgs args) { if(ps.Selected) ps.Color = ps.ColorOptions[0]; else {ps.Color = null; ps.oldcolor = null;}};
				ps.Enabled = true;
				i++;
			}
		}
		
		public void UpdateColor(PlayerSlot colorDecider, string color)
		{
			foreach(PlayerSlot ps in PlayerSlots)
			{
				if(colorDecider == ps) continue;
				ps.ColorOptions.Remove(color);
				if(colorDecider.oldcolor != null) ps.ColorOptions.Add(colorDecider.oldcolor);
			}
			colorDecider.oldcolor = color;
		}
	
		public List<Player> GetPlayers()
		{
			List<Player> playersRegistered = new List<Player>();
			foreach(PlayerSlot ps in PlayerSlots)
			{
				if(ps.Enabled)
				playersRegistered.Add(new Player(){Name = ps.Name, Color = ps.Color});
			}
			return playersRegistered;
		}
		
		public event PropertyChangedEventHandler PropertyChanged;
	}
}
