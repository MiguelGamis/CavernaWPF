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
using CavernaWPF.Layables;
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
			
			for(int row = 1; row < 5; row++)
			{
				for(int col = 1; col < 4; col++)
				{
					boardtiles[col,row] = new BoardTile() { state = BoardTile.Type.Forest };
				}
				for(int col = 4; col < 7; col++)
				{
					boardtiles[col,row] = new BoardTile() { state = BoardTile.Type.Rock };
				}
			}
			
			boardtiles[3,1].bonusResource = Resource.Type.Boar; boardtiles[3,1].bonusResourceAmount = 1;
			boardtiles[1,3].bonusResource = Resource.Type.Boar; boardtiles[1,3].bonusResourceAmount = 1;
			boardtiles[2,4].bonusResource = Resource.Type.Food; boardtiles[2,4].bonusResourceAmount = 1;
			boardtiles[5,4].bonusResource = Resource.Type.Food; boardtiles[5,4].bonusResourceAmount = 1;
			boardtiles[6,1].bonusResource = Resource.Type.Food; boardtiles[6,1].bonusResourceAmount = 2;
			
			boardtiles[4,4].state = BoardTile.Type.Dwelling;
			boardtiles[4,3].state = BoardTile.Type.Cave;
		}
		
		public Town control;
		public ResourcesTab tab;
		
		private ObservableCollection<object> tiles = new ObservableCollection<object>();
		
		public ObservableCollection<object> Tiles{ 
			get{ return tiles; }
			set{ tiles = value; 
			 	if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Tiles"));
			}
		}
		
		public BoardTile[,] boardtiles = new BoardTile[8,6];
		
//		public Dictionary<Resource.Type, int> resources = new Dictionary<Resource.Type, int>() { 
//			{Resource.Type.Food, 0}, 
//			{Resource.Type.Gold, 0},
//			{Resource.Type.Wood, 0},
//			{Resource.Type.Stone, 0},
//			{Resource.Type.Ore, 0},
//			{Resource.Type.Ruby, 0}
//		};
		
		//public ResourcesTabContext ResourcesTabContext = new ResourcesTabContext();
		
		private Dictionary<Resource.Type, ResourceTab> resources = new Dictionary<Resource.Type, ResourceTab>(){
			{ Resource.Type.Wood, new ResourceTab(){ ResourceType = Resource.Type.Wood, Amount = 0 } },
			{ Resource.Type.Stone, new ResourceTab(){ ResourceType = Resource.Type.Stone, Amount = 0 } },
			{ Resource.Type.Ore, new ResourceTab(){ ResourceType = Resource.Type.Ore, Amount = 0 } },
			{ Resource.Type.Ruby, new ResourceTab(){ ResourceType = Resource.Type.Ruby, Amount = 0 } },
			{ Resource.Type.Gold, new ResourceTab(){ ResourceType = Resource.Type.Gold, Amount = 0 } },
			{ Resource.Type.Food, new ResourceTab(){ ResourceType = Resource.Type.Food, Amount = 0 } },
			{ Resource.Type.Grain, new ResourceTab(){ ResourceType = Resource.Type.Grain, Amount = 0 } },
			{ Resource.Type.Vegetable, new ResourceTab(){ ResourceType = Resource.Type.Vegetable, Amount = 0 } },
			{ Resource.Type.Sheep, new ResourceTab(){ ResourceType = Resource.Type.Sheep, Amount = 0 } },
			{ Resource.Type.Donkey, new ResourceTab(){ ResourceType = Resource.Type.Donkey, Amount = 0 } },
			{ Resource.Type.Boar, new ResourceTab(){ ResourceType = Resource.Type.Boar, Amount = 0 } },
			{ Resource.Type.Cow, new ResourceTab(){ ResourceType =Resource.Type.Cow, Amount = 0 } }
		};
		
		public Dictionary<Resource.Type, ResourceTab> Resources{ 
			get { return resources; }
			set { resources = value; }
		}
		
		public event PropertyChangedEventHandler PropertyChanged;
	}
}
