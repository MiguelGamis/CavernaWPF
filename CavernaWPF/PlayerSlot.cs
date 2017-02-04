/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 01/31/2017
 * Time: 23:25
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CavernaWPF
{
	/// <summary>
	/// Description of PlayerSlot.
	/// </summary>
	public class PlayerSlot : INotifyPropertyChanged
	{
		private bool enabled;
		
		public bool Enabled
		{
	        get { return enabled; }
	        set
	        {
	            enabled = value;
	            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Enabled"));
	        }
		}
		
		public delegate void SelectedTrueEventHandler(object sender, EventArgs args);
		
		public event SelectedTrueEventHandler SelectedTrue;
		
		private void OnSelectedTrue()
		{
			if (SelectedTrue != null)
				SelectedTrue(this, EventArgs.Empty);
		}
		
		private bool selected;
		
		public bool Selected
		{
	        get { return selected; }
	        set
	        {
	            selected = value;
	            OnSelectedTrue();
	            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Selected"));
	        }
		}
		
		private bool startingplayer;
		
		public bool StartingPlayer
		{
	        get { return startingplayer; }
	        set
	        {
	            startingplayer = value;
	            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("StartingPlayer"));
	        }
		}
		
		public string Name
		{
			get;
			set;
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
		
		public string oldcolor;
		
		private ObservableCollection<string> colorOptions = new ObservableCollection<string>() {"Red","Blue","Yellow","Green","Purple","Black"};
		
		public ObservableCollection<string> ColorOptions
		{
			get{ return colorOptions; }
			set{
				colorOptions = value;
				if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ColorOptions"));
			}
		}
		
		public event PropertyChangedEventHandler PropertyChanged;
	}
}
