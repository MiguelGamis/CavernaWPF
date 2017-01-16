/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 12/29/2016
 * Time: 00:18
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows;

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
	public class TownContext : INotifyPropertyChanged, INotifyCollectionChanged
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
		
		public void AddTile(Layable layable)
		{
			//ActionBoardContext.Instance.playerLocks[ActionBoardContext.Instance.currentPlayer.Value] = true;
			ActionBoardContext.Instance.promptingPlacement = true;
			Tiles.Add(layable);
			//this.OnNotifyCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, layable));
			LayoutManager.Instance.confirmButtons[ActionBoardContext.Instance.currentPlayer.Value].Click += Confirm;
		}
		
		public void Confirm(object sender, RoutedEventArgs e)
		{
			IEnumerable<Layable> deleteList = Tiles.ToList().Where(layable => layable.row == 0 && layable.column == 0);
			
			if(deleteList.Count() > 0)
			{
				MessageBoxResult result = MessageBox.Show("Are you sure you want to throw the tiles away", "There are tiles not placed", MessageBoxButton.YesNo, MessageBoxImage.Question);
				if (result == MessageBoxResult.No)
				{
				    return;
				}
				else
				{
					deleteList.ToList().ForEach(l => Tiles.Remove(l));
				}
			}
			
			foreach(Layable layable in Tiles)
			{
				if(layable is Tile)
				{
					if(layable.Locked)
						continue;
					
					Tile t = (layable as Tile);
					var ftype = Helpers.convertFirst(t);
					int row = t.row;
					int col = t.column;
					boardtiles[col, row].state = ftype;
					if(t.Twin)
					{
						switch(t.Rot)
						{
							case 0:
								col++;
								break;
							case 90:
								row++;
								break;
							case 180:
								col--;
								break;
							case 270:
								row--;
								break;
						}
						var stype = Helpers.convertSecond(t);
						boardtiles[col, row].state = stype;
					}
					t.Locked = true;
				}
				if(layable is FarmAnimal)
				{
					
				}
			}
			
			//ActionBoardContext.Instance.playerLocks[ActionBoardContext.Instance.currentPlayer.Value] = false;
			LayoutManager.Instance.confirmButtons[ActionBoardContext.Instance.currentPlayer.Value].Click -= Confirm;
			ActionBoardContext.Instance.promptingPlacement = false;
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
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		#region INotifyCollectionChanged
	    private void OnNotifyCollectionChanged(NotifyCollectionChangedEventArgs args)
	    {
			if (this.CollectionChanged != null)
			{
			    this.CollectionChanged(this, args);
			}
	    }
	    public event NotifyCollectionChangedEventHandler CollectionChanged;
	    #endregion INotifyCollectionChanged
	}
}
