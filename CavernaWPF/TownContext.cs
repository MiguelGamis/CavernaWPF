/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 12/29/2016
 * Time: 00:18
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using CavernaWPF.Layable;

namespace CavernaWPF
{
	/// <summary>
	/// Description of TownContext.
	/// </summary>
	public class TownContext : INotifyPropertyChanged
	{
		public TownContext()
		{
			control = new Town();
			control.DataContext = this;
			
			Tiles.Add(new Tile());
		}
		
		public Town control;
		
		private ObservableCollection<Tile> tiles = new ObservableCollection<Tile>();
		
		public ObservableCollection<Tile> Tiles{ 
			get{ return tiles; }
			set{ tiles = value; 
			 	if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Tiles"));
			}
		}
		
		public event PropertyChangedEventHandler PropertyChanged;
	}
}
