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
		
		private Dictionary<string, ColorOption> colorOptions = new Dictionary<string, ColorOption>() {
			{"Red", new ColorOption("Red")},
			{"Blue", new ColorOption("Blue")},
			{"Yellow", new ColorOption("Yellow")},
			{"Green", new ColorOption("Green")},
			{"Purple", new ColorOption("Purple")},
			{"Black", new ColorOption("Black")}
		};
		
		public Dictionary<string, ColorOption> ColorOptions
		{
			get{ return colorOptions; }
			set{
				colorOptions = value;
				if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ColorOptions"));
			}
		}
		
		public class ColorOption : INotifyPropertyChanged
		{
			private PlayerSlot selector;
			
			public PlayerSlot Selector
			{
				get { return selector; }
				set {
					selector = value;
					if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Selector"));
				}
			}
			
			private string color;
			
			public string Color
			{
				get { return color; }
				set {
					color = value;
					if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Color"));
				}
			}
			
			public ColorOption(string color)
			{
				Color = color;
			}
			
			public event PropertyChangedEventHandler PropertyChanged;
		}
		
		public void PlayerSlotSelectionChange(object sender, EventArgs args)
		{
			PlayerSlot ps = sender as PlayerSlot;
			if(ps.Selected)
			{
				ps.Color = ColorOptions.First().Value.Color;
				if(!PlayerSlots.Any(p => p.StartingPlayer))
					PlayerSlots.Find(p => p.Selected).StartingPlayer = true;
			}
			else 
			{
				ColorOptions[ps.Color].Selector = null;
				ps.Color = null; 

			}
		}
		
		public void UpdateColor(string color)
		{

		}
		
		public event PropertyChangedEventHandler PropertyChanged;
	}
}
