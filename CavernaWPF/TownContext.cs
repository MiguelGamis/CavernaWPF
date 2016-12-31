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
using System.Collections.Generic;
using System.Collections.Specialized;
using CavernaWPF.Layable;
using CavernaWPF.Resources;

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
			
			tab = new ResourcesTab();
			tab.DataContext = this;
		}
		
		public Town control;
		public ResourcesTab tab;
		
		private ObservableCollection<Tile> tiles = new ObservableCollection<Tile>();
		
		public ObservableCollection<Tile> Tiles{ 
			get{ return tiles; }
			set{ tiles = value; 
			 	if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Tiles"));
			}
		}
		
		public BoardTile[,] forest = new BoardTile[3,4];
		public BoardTile[,] cave = new BoardTile[3,4];
		
//		public Dictionary<Resource.Type, int> resources = new Dictionary<Resource.Type, int>() { 
//			{Resource.Type.Food, 0}, 
//			{Resource.Type.Gold, 0},
//			{Resource.Type.Wood, 0},
//			{Resource.Type.Stone, 0},
//			{Resource.Type.Ore, 0},
//			{Resource.Type.Ruby, 0}
//		};
		
		//public ResourcesTabContext ResourcesTabContext = new ResourcesTabContext();
		
		private ObservableCollection<ResourceTab> resources = new ObservableCollection<ResourceTab>(){
			new ResourceTab(){ ResourceType = Resource.Type.Wood, Amount = 0 },
			new ResourceTab(){ ResourceType = Resource.Type.Stone, Amount = 0 },
			new ResourceTab(){ ResourceType = Resource.Type.Ore, Amount = 0 },
			new ResourceTab(){ ResourceType = Resource.Type.Ruby, Amount = 0 },
			new ResourceTab(){ ResourceType = Resource.Type.Gold, Amount = 0 },
			new ResourceTab(){ ResourceType = Resource.Type.Food, Amount = 0 }
		};
		
		public ObservableCollection<ResourceTab> Resources{ 
			get{ return resources; }
			set{ resources = value; 
			 	if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Resources"));
			}
		}
		
		public event PropertyChangedEventHandler PropertyChanged;
	}
}
