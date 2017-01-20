﻿/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 12/26/2016
 * Time: 16:53
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections.ObjectModel;
using CavernaWPF.Layables;
using CavernaWPF.Resources;
using System.Windows.Media.Imaging;
using System.Linq;

namespace CavernaWPF
{
	/// <summary>
	/// Interaction logic for Village.xaml
	/// </summary>
	public partial class Village : UserControl
	{
		public Village()
		{
			InitializeComponent();
		}

	    private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
	    {
	    	object obj = ((FrameworkElement)sender).DataContext;
	    	if(obj is Layable)
	    	{
	        	Layable n = (Layable) obj;
				if(n is Tile || n is Sowable || n is Stable)
	        	{
	        		if(n.Locked)
	        		{
	        			(sender as Thumb).DragDelta -= Thumb_DragDelta;
	        			(sender as Thumb).MouseRightButtonDown -= Rotate;
	        			(sender as Thumb).DragCompleted -= Thumb_DragCompleted;
	        			return;
	        		}
	        	}
	        	n.X += e.HorizontalChange;
	        	n.Y += e.VerticalChange;
	    	}		
	    }
	    
	    private void Thumb_DragCompleted(object sender, DragCompletedEventArgs e)
	    {
	        object obj = ((FrameworkElement)sender).DataContext;
			if(obj is Layable)
	    	{
				Layable n = (Layable) obj;
				
				if(n is Tile || n is Sowable || n is Stable)
	        	{
	        		if(n.Locked)
	        		{
	        			(sender as Thumb).DragDelta -= Thumb_DragDelta;
	        			(sender as Thumb).MouseRightButtonDown -= Rotate;
	        			(sender as Thumb).DragCompleted -= Thumb_DragCompleted;
	        			return;
	        		}
	        	}
				
				//Snap dwarf to closest action card location
				double w = 35;//((FrameworkElement)sender).Width / 2;
				double h = 35;//((FrameworkElement)sender).Height / 2;
				double x = w + n.X;
				double y = h + n.Y;
				int row = 0; int col = 0;
				double _x = 0; double _y = 0;
				foreach(ColumnDefinition rd in board.ColumnDefinitions)
				{
					if(_x + rd.ActualWidth < x)
					{
						_x += rd.ActualWidth;
						col++;
					}
					else
					{	
						if(col == 4)
							ReleaseLayable(n);
						if(col > 4)
							col--;
						break;
					}
				}
				foreach(RowDefinition rd in board.RowDefinitions)
				{
					if(_y + rd.ActualHeight < y)
					{
						_y += rd.ActualHeight;
						row++;
					}
					else
					{
						break;
					}
				}
				
				int twinrow = row;
				int twincol = col;
				if(n is Tile)
				{
					Tile t = n as Tile;
					Tile twinTile= t.GetTwinTile();
					if(twinTile != null)
					{
						switch(t.Rot)
	 	       			{
	 	       				case 0:
	 	       					twincol++;
	 	       					break;
	 	       				case 90:
	 	       					twinrow++;
	 	       					break;
	 	       				case 180:
	 	       					twincol--;
	 	       					break;
	 	       				case 270:
	 	       					twinrow--;
	 	       					break;
	 	       			}
					}
					
					if(1 <= col && col <= 6 && 1 <= row && row <= 4 && t.Twin ? (1 <= twincol && twincol <= 6 && 1 <= twinrow && twinrow <= 4) : true)
					{
						//if changing positions remove farm animals and stables 
						if(col != t.column || row != t.row)
						{
							ReleaseFarmAnimals(t);
							ReleaseStables(t);
						}
						
						//if changing positions remove farm animals and stables
						if(twinTile != null && (twincol != twinTile.column || twinrow != twinTile.column))
						{
						   	ReleaseFarmAnimals(twinTile);
							ReleaseStables(twinTile);
						}
							
						if(!isAllowed(t, col, row) || (twinTile != null && !isAllowed(twinTile, twincol, twinrow)) )
			        	{
			  	        	ReleaseLayable(n);
		 	        		return;
			        	}
						
						if(!isAdjacent(t, col, row) && (twinTile != null && !isAdjacent(twinTile, twincol, twinrow)))
				        {
		 	       			ReleaseLayable(n);
		 	        		return;
				        }
						
						PlaceLayable(t, row, col, _x, _y);
						if(twinTile != null) PlaceLayable(twinTile, twinrow, twincol, 0, 0);
						return;
					}
					ReleaseLayable(n);
		 	        return;
				}
				
				if(n is FarmAnimal)
 	        	{
					if(1 <= col && col <= 6 && 1 <= row && row <= 4)
	 	        	{
	 	        		TownContext tc = this.DataContext as TownContext;
	 	        		
	 	        		FarmAnimal fa = n as FarmAnimal;
						
	 	        		if(fa.column == col && fa.row == row)
	 	        			return;
	 	        		
	 	        		List<FarmAnimal> occupyingFarmAnimals = tc.Tiles.OfType<FarmAnimal>().Where(s => s.row == row && s.column == col).ToList();
	 	        		if(occupyingFarmAnimals.Count > 0)
	 	        		{
	 	        			if(occupyingFarmAnimals[0].type != fa.type)
	 	        			{
	 	        				ResetLayable(n);
	 	        				return;
	 	        			}
	 	        		}
	 	        		
	 	        		List<Tile> intersectingTiles  = tc.Tiles.OfType<Tile>().Where(t => t.row == row && t.column == col).ToList();
	 	        		List<Stable> intersectingStable = tc.Tiles.OfType<Stable>().Where(s => s.row == row && s.column == col).ToList();
	 	        		List<Tile> fenced= intersectingTiles.Where(t => t.type == Tile.Type.Fence || t.type == Tile.Type.BigFence).ToList();
	 	        		if(fenced.Count == 1)
	 	        		{
	 	        			int cap = 0;
	 	        			switch(fenced[0].type)
	 	        			{
	 	        				case Tile.Type.Fence:
	 	        					cap = 2;
	 	        					break;
	 	        				case Tile.Type.BigFence:
	 	        					cap = 4;
	 	        					break;
	 	        			}
	 	        			if(intersectingStable.Count == 1)
	 	        			{
	 	        				cap *= 2;
	 	        			}
	 	        			if(occupyingFarmAnimals.Count + 1 <= cap)
	 	        			{
	 	        				occupyingFarmAnimals.Add(fa);
	 	        				PlaceFarmAnimals(occupyingFarmAnimals, row, col, _x, _y);
			 	        		
			 	        		return;
	 	        			}
	 	        		}
	 	        		List<Tile> meadows= intersectingTiles.Where(t => t.type == Tile.Type.Meadow || t.type == Tile.Type.MeadowField).ToList();
	 	        		if(meadows.Count == 1)
	 	        		{
	 	        			if(fa.type == FarmAnimal.Type.Sheep)
	 	        			{
	 	        				List<Dog> intersectingDogs = tc.Tiles.OfType<Dog>().Where(d => d.row == row && d.column == col).ToList();
	 	        				if(intersectingDogs.Count > 0 ? intersectingDogs.Count + 1 >= occupyingFarmAnimals.Count : false)
	 	        				{
									occupyingFarmAnimals.Add(fa);
	 	        					PlaceFarmAnimals(occupyingFarmAnimals, row, col, _x, _y);
				 	        		return;
	 	        				}
	 	        			}
	 	        		}
					}
 	        		ResetLayable(n);
 	        		return;
 	        	}
				if(n is Sowable)
 	        	{
					Sowable sow = n as Sowable;
					TownContext tc = this.DataContext as TownContext;
					List<Tile> fieldsg = tc.Tiles.OfType<Tile>().ToList();
					List<Tile> fields = tc.Tiles.OfType<Tile>().Where(t => t.row == row && t.column == col && (t as Tile).type == Tile.Type.Field || (t as Tile).type == Tile.Type.FieldDummy).ToList();
					bool isSowed = tc.Tiles.OfType<Sowable>().Any(sw => sw.row == row && sw.column == col);
					if(fields.Count == 1 && !isSowed)
					{
						if(sow.type == Sowable.Type.Grain)
						{
							if(sow.row == 0 && sow.column == 0)
								ActionBoardContext.Instance.currentPlayer.Value.Resources[Resource.Type.Grain].Amount--;
							PlaceLayable(sow, row, col, _x, _y);
							return;
						}
						else if(sow.type == Sowable.Type.Vegetable)
						{
							if(sow.row == 0 && sow.column == 0)
								ActionBoardContext.Instance.currentPlayer.Value.Resources[Resource.Type.Vegetable].Amount--;
							PlaceLayable(sow, row, col, _x, _y);
							return;
						}
					}
					ResetLayable(sow);
 	        	}
				
				
	 	        if(1 <= col && col <= 6 && 1 <= row && row <= 4)
	 	        {
	 	        	if(n is Dog)
	 	        	{
	 	        		TownContext tc = this.DataContext as TownContext;

	 	        	}
	 	        	else if(n is Stable)
	 	        	{
	 	        		TownContext tc = this.DataContext as TownContext;
	 	        		
	 	        		Stable st = n as Stable;
	 	        		
	 	        		//if changing positions remove farm animals and stables 
						if(col != st.column || row != st.row)
						{
							//ReleaseFarmAnimals(t);
							//ReleaseStables(t);
						}
	 	        		
	 	        		if(col > 3)
	 	        		{
	 	        			ReleaseLayable(n);
	 	        		}
	 	        		
	 	        		bool otherStables = tc.Tiles.OfType<Stable>().Any(t => t.row == row && t.column == col);
	 	        		if(otherStables)
	 	        		{
	 	        			ReleaseLayable(n);
	 	        		}
	 	        		
						PlaceLayable(n, row, col, _x, _y);
	 	        	}
	 	        }
 	        	ReleaseLayable(n);
 	        	return;
			}
	    }
	    
	    private void ResetLayable(Layable l)
	    {
	    	if(l.row == 0 && l.column == 0)
	    		return;
	    	int col = l.column;
	    	if(col > 3) col++;
	    	l.X = board.ColumnDefinitions.ToList().GetRange(0, col).Sum(c => c.ActualWidth);
	    	l.Y = board.RowDefinitions.ToList().GetRange(0, l.row).Sum(r => r.ActualHeight);
 	    }
	    
	   	private void ReleaseLayable(Layable l)
	    {
	   		if(l is Tile)
	    	{
	   			ReleaseFarmAnimals(l.row, l.column);
	   			//DON'T ASK IF Twintile is null
	   			if((l as Tile).Twin)
	   			{
	   				ReleaseLayable((l as Tile).GetTwinTile());
	   			}
	    	}
	   		
	   		if(l is Sowable)
	   		{
	   			Sowable s = l as Sowable;
	   			if(s.type == Sowable.Type.Grain)
	   			{
	   				if(l.column != 0 || l.row != 0)
	   					ActionBoardContext.Instance.currentPlayer.Value.Resources[Resource.Type.Grain].Amount++;
	   				l.X = 40*6;
	   				l.Y = 420;
	   			}
	   			else
	   			{
	   				if(l.column != 0 || l.row != 0)
	   					ActionBoardContext.Instance.currentPlayer.Value.Resources[Resource.Type.Vegetable].Amount++;
	   				l.X = 40*7;
	   				l.Y = 420;
	   			}
	   		}
	   		else
	   		{
		    	l.X = 0;
		    	l.Y = 0;
	   		}
	    	l.column = 0;
	    	l.row = 0;
	    }
	    
	   	private void PlaceLayable(Layable l, int row, int col, double x, double y)
	   	{
			l.column = col;
			l.row = row;
			l.X = x;
			l.Y = y;
	   	}
	   	
	   	private void PlaceLayable(Layable l, int row, int col)
	   	{
	   		l.column = col;
			l.row = row;
	   		if(col > 3) col++;
			l.X = board.ColumnDefinitions.ToList().GetRange(0, col).Sum(c => c.ActualWidth);
	    	l.Y = board.RowDefinitions.ToList().GetRange(0, row).Sum(r => r.ActualHeight);
	   	}
	   	
	    private void PlaceFarmAnimals(List<FarmAnimal> fas, int row, int col, double x, double y)
	   	{
	    	int count = 0;
	    	foreach(FarmAnimal fa in fas)
	    	{
				fa.column = col;
				fa.row = row;
				fa.X = x + count*10;
				fa.Y = y + count*10;
				count++;
	    	}
	   	}
	   	
	   	private void ReleaseFarmAnimals(int row, int col)
	   	{
	   		TownContext tc = this.DataContext as TownContext;
	   		List<FarmAnimal> occupyingFarmAnimals = tc.Tiles.OfType<FarmAnimal>().Where(fa => fa.row == row && fa.column == col).ToList();
	 	    occupyingFarmAnimals.ForEach(fa => ReleaseLayable(fa));
	   	}
	   	
	   	private void ReleaseFarmAnimals(Tile t)
	   	{
	   		TownContext tc = this.DataContext as TownContext;
   			List<FarmAnimal> occupyingFarmAnimals = tc.Tiles.OfType<FarmAnimal>().Where(fa => fa.row == t.row && fa.column == t.column ).ToList();
 	    	occupyingFarmAnimals.ForEach(fa => ReleaseLayable(fa));
	   	}
	   	
	    private void ReleaseStables(Tile t)
	   	{
   			TownContext tc = this.DataContext as TownContext;
   			List<Stable> occupyingStables = tc.Tiles.OfType<Stable>().Where(st => st.row == t.row && st.column == t.column).ToList();
 	    	occupyingStables.ForEach(st => ReleaseLayable(st));
	   	}
	   	
	    private void LockIn(object sender, MouseButtonEventArgs e)
	    {
	    	
	    }
	    
	   	private void Rotate(object sender, MouseButtonEventArgs e)
	    {
	        object obj = ((FrameworkElement)sender).DataContext;
	        if(obj is Layable)
	        {
	        	Layable l = obj as Layable;
	        	
	        	if(l is Tile || l is Sowable || l is Stable)
	        	{
	        		if(l.Locked)
	        		{
	        			(sender as Thumb).DragDelta -= Thumb_DragDelta;
	        			(sender as Thumb).MouseRightButtonDown -= Rotate;
	        			(sender as Thumb).DragCompleted -= Thumb_DragCompleted;
	        			return;
	        		}
	        	}
	        	
		        if(l is Tile)
		        {
		        	Tile t = l as Tile;
		        	if(t.Twin)
		        	{
			        	t.Rotate();
		        	}
		        }
		        ReleaseLayable(l);
	        }
	    }
	   	
	   	private bool isAllowed(Tile tile, int col, int row)
	   	{
	   		List<Tile.Type> acceptableTiles = new List<Tile.Type>();
	   		
	   		bool isPreTile = false;
	   		
	   		switch(tile.type)
	   		{
    			case Tile.Type.CaveCave:
    			case Tile.Type.CaveTunnel:
    			case Tile.Type.Cave:
	   			case Tile.Type.CaveDummy:
    			case Tile.Type.Tunnel:
	   			case Tile.Type.TunnelDummy:
	   				if(col < 4)
	   				{
	   					return false;
	   				}
	   				isPreTile = true;
	   				break;
    			case Tile.Type.MeadowField:
    			case Tile.Type.Meadow:
	   			case Tile.Type.Field:
	   			case Tile.Type.FieldDummy:
	   				if(col > 3)
	   				{
	   					return false;
	   				}
	   				isPreTile = true;
    				break;
    			case Tile.Type.BigFence:
    			case Tile.Type.Fence:
    			case Tile.Type.BigFenceDummy:
    				acceptableTiles.Add(Tile.Type.Meadow);
    				acceptableTiles.Add(Tile.Type.MeadowField);
    				break;
    			case Tile.Type.OreMine:
    				acceptableTiles.Add(Tile.Type.Tunnel);
    				acceptableTiles.Add(Tile.Type.TunnelDummy);
    				break;
    			case Tile.Type.RubyMine:
    				acceptableTiles.Add(Tile.Type.Tunnel);
    				acceptableTiles.Add(Tile.Type.TunnelDummy);
    				acceptableTiles.Add(Tile.Type.DeepTunnelDummy);
    				break;
    			case Tile.Type.Dwelling:
    				acceptableTiles.Add(Tile.Type.Cave);
    				acceptableTiles.Add(Tile.Type.CaveCave);
    				acceptableTiles.Add(Tile.Type.CaveTunnel);
    				acceptableTiles.Add(Tile.Type.CaveDummy);
    				break;
	   		}
	   		TownContext tc = (DataContext as TownContext);
	   		List<Tile> occupyingTiles = tc.Tiles.OfType<Tile>().Where(t => t.row == row && t.column == col && t != tile && t != tile.GetTwinTile()).ToList();
	   		
   			if(isPreTile)
   			{
   				return occupyingTiles.Count == 0;
   			}
   			else
   			{
   				List<Tile> rightPreTiles = occupyingTiles.Where(t => acceptableTiles.Any(type => type == t.type)).ToList();
   				return rightPreTiles.Count > 0;
   			}
	   	}
	   	
//	   	private bool isAllowed(Tile tile, int col, int row)
//	   	{
//	   		List<Tile.Type> acceptableTiles = new List<Tile.Type>();
//	   		
//	   		bool isPreTile = false;
//	   		
//	   		switch(tile.type)
//	   		{
//    			case Tile.Type.CaveCave:
//    			case Tile.Type.CaveTunnel:
//    			case Tile.Type.Cave:
//    			case Tile.Type.Tunnel:
//	   				if(col < 4)
//	   				{
//	   					return false;
//	   				}
//	   				isPreTile = true;
//	   				break;
//    			case Tile.Type.MeadowField:
//    			case Tile.Type.Field:
//    			case Tile.Type.Meadow:
//	   				if(col > 3)
//	   				{
//	   					return false;
//	   				}
//	   				isPreTile = true;
//    				break;
//    			case Tile.Type.BigFence:
//    			case Tile.Type.Fence:
//    				acceptableTiles.Add(Tile.Type.Meadow);
//    				acceptableTiles.Add(Tile.Type.MeadowField);
//    				break;
//    			case Tile.Type.OreMine:
//    				acceptableTiles.Add(Tile.Type.Tunnel);
//    				acceptableTiles.Add(Tile.Type.TunnelDummy);
//    				break;
//    			case Tile.Type.RubyMine:
//    				acceptableTiles.Add(Tile.Type.Tunnel);
//    				acceptableTiles.Add(Tile.Type.TunnelDummy);
//    				acceptableTiles.Add(Tile.Type.DeepTunnelDummy);
//    				break;
//	   		}
//	   		TownContext tc = (DataContext as TownContext);
//	   		
//	   		List<Tile> intersectingTiles = tc.Tiles.OfType<Tile>().Where(t => t.row == row && t.column == col && t != tile && t != tile.GetTwinTile()).ToList();
//	   		List<Tile> rightPreTiles = intersectingTiles.Where(t => acceptableTiles.Any(type => type == t.type)).ToList();
//	   		
//	   		if(tile.Twin)
//	   		{
//    			switch(tile.Rot)
//    			{
//    				case 0:
//    					col++;
//    					break;
//    				case 90:
//    					row++;
//    					break;
//    				case 180:
//    					col--;
//    					break;
//    				case 270:
//    					row--;
//    					break;
//    				default:
//    					break;
//    			}
//	   			
//    			switch(tile.type)
//	   			{
//	    			case Tile.Type.CaveCave:
//	    			case Tile.Type.CaveTunnel:
//	    			case Tile.Type.Cave:
//	    			case Tile.Type.Tunnel:
//		   				if(col < 4)
//		   				{
//		   					return false;
//		   				}
//		   				break;
//	    			case Tile.Type.MeadowField:
//	    			case Tile.Type.Field:
//	    			case Tile.Type.Meadow:
//		   				if(col > 3)
//		   				{
//		   					return false;
//		   				}
//		   				break;
//    			}
//    			
//	   			List<Tile> intersectingTiles2 = tc.Tiles.OfType<Tile>().Where(t => t.row == row && t.column == col && t != tile && t != tile.GetTwinTile()).ToList();
//	   			List<Tile> rightPreTiles2 = intersectingTiles2.Where(t => acceptableTiles.Any(type => type == t.type)).ToList();
//	   			
//	   			if(isPreTile)
//	   				return intersectingTiles.Count == 0 && intersectingTiles2.Count == 0;
//	   			else
//	   				return rightPreTiles.Count > 0 && rightPreTiles2.Count > 0;
//	   		}
//	   		else
//	   		{
//	   			if(isPreTile)
//	   				return intersectingTiles.Count == 0;
//	   			else
//	   				return rightPreTiles.Count > 0;
//	   		}
//	   	}

	   	private bool isAdjacent(Tile tile, int col, int row)
	   	{
	   		TownContext tc = (DataContext as TownContext);
	   		var adjacentTiles = tc.Tiles.Where(t => ((t.row == row && Math.Abs(t.column - col) == 1 ) || (t.column == col && Math.Abs(t.row - row) == 1)) && t != tile && t != tile.GetTwinTile() ).ToList();
	   		return adjacentTiles.Count > 0;
	   	}

//	   	private bool isAdjacent(Tile tile, int col, int row)
//	   	{
//	   		TownContext tc = (DataContext as TownContext);
//	   		var adjs1 = tc.Tiles.Where(t => ((t.row == row && Math.Abs(t.column - col) == 1 ) || (t.column == col && Math.Abs(t.row - row) == 1)) && t != tile && t != tile.GetTwinTile() ).ToList();
//	   		bool bool1 = adjs1.Count > 0;
//	   		if(tile.Twin)
//	   		{
//	   			switch(tile.Rot)
//    			{
//    				case 0:
//    					col++;
//    					break;
//    				case 90:
//    					row++;
//    					break;
//    				case 180:
//    					col--;
//    					break;
//    				case 270:
//    					row--;
//    					break;
//    			}
//	   			var adjs2 = tc.Tiles.Where(t => ((t.row == row && Math.Abs(t.column - col) == 1 ) || (t.column == col && Math.Abs(t.row - row) == 1)) && t != tile && t != tile.GetTwinTile() ).ToList();
//	   			bool bool2 = adjs2.Count > 0;
//	   			return bool1 || bool2;
//	   		}
//	   		return bool1;
//	   	}
	}
}