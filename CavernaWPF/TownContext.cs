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
using System.Linq;
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
			
			for(int row = 1; row < 5; row++)
			{
				for(int col = 1; col < 4; col++)
				{
					boardtiles[col,row] = new BoardTile() { state = BoardTile.Type.Forest, Row = row, Column = col };
				}
				for(int col = 4; col < 7; col++)
				{
					boardtiles[col,row] = new BoardTile() { state = BoardTile.Type.Rock, Row = row, Column = col  };
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
		
		private ObservableCollection<Layable> tiles = new ObservableCollection<Layable>();
		
		public ObservableCollection<Layable> Tiles{ 
			get{ return tiles; }
			set{ tiles = value; 
			 	if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Tiles"));
			}
		}
		
		public void AddTile(Tile t)
		{
		}
		
		public BoardTile[,] boardtiles = new BoardTile[8,6];
		
		public int HowManyBoardTilesOfType(BoardTile.Type type)
		{
			var query = from BoardTile item in boardtiles
						where item != null
						where item.state == BoardTile.Type.Cave
						select item;
			return query == null ? 0 : query.Count();
		}
		
		public int HasAdjacent(BoardTile.Type type)
		{
			var query = from BoardTile item in boardtiles
						where item.state == type
						select item;
			
			return -1;
		}
		
//		public Dictionary<Resource.Type, int> resources = new Dictionary<Resource.Type, int>() { 
//			{Resource.Type.Food, 0}, 
//			{Resource.Type.Gold, 0},
//			{Resource.Type.Wood, 0},
//			{Resource.Type.Stone, 0},
//			{Resource.Type.Ore, 0},
//			{Resource.Type.Ruby, 0}
//		};
		
		//public ResourcesTabContext ResourcesTabContext = new ResourcesTabContext();
		
		public event PropertyChangedEventHandler PropertyChanged;
	}
}
