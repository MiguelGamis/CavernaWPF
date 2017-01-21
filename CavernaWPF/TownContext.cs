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
			control = new Village();
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
			
			Tile cavedummy = new Tile(Tile.Type.CaveDummy) {Locked = true};
			cavedummy.row = 3; cavedummy.column = 4;
			Tiles.Add(cavedummy);
			Tile dwellingdummy = new Tile(Tile.Type.DwellingDummy) {Locked = true};
			dwellingdummy.row = 4; dwellingdummy.column = 4;
			Tiles.Add(dwellingdummy);
		}
		
		public void PositionLayable(Layable l, int col, int row)
		{
			Tiles.Add(l);
			l.column = col;
			l.row = row;
			if(col > 3) col++;
			l.X = control.board.ColumnDefinitions.ToList().GetRange(0, col).Sum(c => c.ActualWidth);
	    	l.Y = control.board.RowDefinitions.ToList().GetRange(0, row).Sum(r => r.ActualHeight);
	    	if(l is Tile)
	    	{
	    		Tile t = (l as Tile);
	    		Tile tt = t.GetTwinTile();
	    		if(tt != null)
	    		{
		    		switch((l as Tile).Rot)
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
		    		tt.row = row;
		    		tt.column = col;
		    		Tiles.Add(tt);
	    		}
	    	}
		}
		
		public Village control;
		
		private ObservableCollection<Layable> tiles = new ObservableCollection<Layable>();
		
		public ObservableCollection<Layable> Tiles{ 
			get{ return tiles; }
			set{ tiles = value; 
			 	if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Tiles"));
			}
		}
		
		public void AddTile(Layable layable)
		{
			Tiles.Add(layable);
		}
		
		public BoardTile[,] boardtiles = new BoardTile[8,6];
		
		public int GetCapacity(int col, int row, FarmAnimal.Type animalType)
		{		
			List<Tile> occupyingTiles = Tiles.OfType<Tile>().Where(t => t.row == row && t.column == col).ToList();
			
			List<Tile> fenced = occupyingTiles.Where(t => t.type == Tile.Type.Fence || t.type == Tile.Type.BigFence || t.type == Tile.Type.BigFenceDummy).ToList();
			List<Stable> occupyingStable = Tiles.OfType<Stable>().Where(s => s.row == row && s.column == col).ToList();
			if(fenced.Count == 1)
	 	    {
				int cap = 0;
				Tile enclosure = fenced[0];
		 	    switch(enclosure.type)
    			{
    				case Tile.Type.BigFence:
    				case Tile.Type.BigFenceDummy:
    					Tile extraEnclosure = enclosure.GetTwinTile();
    					cap = 4;
    					break;
					case Tile.Type.Fence:
    					cap = 2;
    					break;
    			}
    			
    			if(occupyingStable.Count == 1)
    			{
    				cap *= 2;
    			}
    			return cap;
			}
			
			List<Tile> meadows = occupyingTiles.Where(t => t.type == Tile.Type.Meadow || t.type == Tile.Type.MeadowField).ToList();
    		if(meadows.Count == 1)
    		{
    			if(animalType == FarmAnimal.Type.Sheep)
    			{
    				List<Dog> occupyingDogs = Tiles.OfType<Dog>().Where(d => d.row == row && d.column == col).ToList();
    				int cap = occupyingDogs.Count > 0 ? occupyingDogs.Count + 1 : 0;
    				if(cap > 0) return cap;
    			}
    		}
    		
    		if(occupyingStable.Count == 1 && animalType == FarmAnimal.Type.Donkey)
    			return 1;
    		
    		List<Tile> mines = occupyingTiles.Where(t => t.type == Tile.Type.OreMine || t.type == Tile.Type.RubyMine).ToList();
    		if(mines.Count == 1 && animalType == FarmAnimal.Type.Donkey)
    			return 1;
    		
			return 0;
		}
		
		public List<FarmAnimal> GetFarmAnimals(int col, int row)
		{
			List<Tile> occupyingTiles = Tiles.OfType<Tile>().Where(t => t.row == row && t.column == col).ToList();
			
			List<FarmAnimal> occupyingFarmAnimals = new List<FarmAnimal>();
			
			if(occupyingTiles.Count == 1)
			{
				Tile enclosure = occupyingTiles[0]; 
				Tile extraEnclosure = enclosure.GetTwinTile();
				if(extraEnclosure != null)
					occupyingFarmAnimals = Tiles.OfType<FarmAnimal>().Where(s => (s.row == row && s.column == col) || (s.row == extraEnclosure.row && s.column == extraEnclosure.column)).ToList();
				else
					occupyingFarmAnimals = Tiles.OfType<FarmAnimal>().Where(s => s.row == row && s.column == col).ToList();
			}
 	        return occupyingFarmAnimals;
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
