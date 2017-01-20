/*
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
using System.Windows.Media.Imaging;
using System.Linq;

namespace CavernaWPF
{
	/// <summary>
	/// Interaction logic for Town.xaml
	/// </summary>
	public partial class Town : UserControl
	{
		public Town()
		{
			InitializeComponent();
		}
		
	    private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
	    {
	    	object obj = ((FrameworkElement)sender).DataContext;
	    	if(obj is Layable)
	    	{
	        	Layable n = (Layable) obj;
	        	if(n is Tile)
	        	{
	        		if((n as Tile).Locked)
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
				
				if(n is Tile)
	        	{
	        		if((n as Tile).Locked)
	        		{
	        			(sender as Thumb).DragDelta -= Thumb_DragDelta;
	        			(sender as Thumb).MouseRightButtonDown -= Rotate;
	        			(sender as Thumb).DragCompleted -= Thumb_DragCompleted;
	        			return;
	        		}
	        	}
				
				double adjX = 35; double adjY = 35;
				
		        double x = n.X + adjX;
				double y = n.Y + adjY;
				
				//get _x the panel column, and _y the panelrow
				if(x > 265.0)
				{
					x -= 265.0;
					x = 3 + (int) (x/70.0);
				}
				else
				{
					x -= 25.0;
					x /= 70.0;
					if(x >= 3)
					{
						n.X = 0;
	 	        		n.Y = 0;
	 	        		return;
					}
				}
				
		        y -= 28.0;
		        y /= 70.0;
		        int col = (int) x + 1;
		        int row = (int) y + 1;
		        
		        
		        string dockname = String.Format("Panel{0}{1}", col, row);
	 	        object uc = this.FindName(dockname);
	 	        if(uc is DockPanel && String.Compare((uc as DockPanel).Name, dockname) == 0)
	 	        {
    				DockPanel dp = (uc as DockPanel);
	 	        	
	 	        	if(n is Tile)
	 	        	{
	 	        		Tile t = n as Tile;

	 	        		TownContext tc = this.DataContext as TownContext;
	 	        		ReleaseFarmAnimals(t.row, t.column);
	 	        		List<Stable> occupyingStable = tc.Tiles.OfType<Stable>().Where(s => s.row == t.row && s.column == t.column).ToList();
	 	        		occupyingStable.ForEach(s => ReleaseLayable(s));
	 	        		                             
		 	       		if(!isAllowed(t, col, row))
			        	{
			  	        	ReleaseLayable(n);
		 	        		return;
			        	}
				        
		 	       		if(!isAdjacent(t, col, row))
				        {
		 	       			ReleaseLayable(n);
		 	        		return;
				        }
		 	       		
		 	       		if(t.Twin)
		 	       		{
		 	       			Tile twinTile= t.GetTwinTile();
		 	       			int twinsrow = row;
		 	       			int twinscolumn = col;
		 	       			switch(t.Rot)
		 	       			{
		 	       				case 0:
		 	       					twinscolumn++;
		 	       					twinTile.row = twinsrow;
		 	       					twinTile.column = twinscolumn;
		 	       					tc.Tiles.Add(twinTile);
		 	       					break;
		 	       				case 90:
		 	       					twinsrow++;
		 	       					twinTile.row = twinsrow;
		 	       					twinTile.column = twinscolumn;
		 	       					tc.Tiles.Add(twinTile);
		 	       					break;
		 	       				case 180:
		 	       					twinscolumn--;
		 	       					twinTile.row = twinsrow;
		 	       					twinTile.column = twinscolumn;
		 	       					tc.Tiles.Add(twinTile);
		 	       					break;
		 	       				case 270:
		 	       					twinsrow--;
		 	       					twinTile.row = twinsrow;
		 	       					twinTile.column = twinscolumn;
		 	       					tc.Tiles.Add(twinTile);
		 	       					break;
		 	       			}
		 	       		}
						PlaceLayable(n, row, col);
	 	        		
	 	        		return;
	 	        	}
	 	        	else if(n is Dog)
	 	        	{
	 	        		TownContext tc = this.DataContext as TownContext;

	 	        	}
	 	        	else if(n is Stable)
	 	        	{
	 	        		TownContext tc = this.DataContext as TownContext;
	 	        		
	 	        		if(col > 3)
	 	        		{
	 	        			ReleaseLayable(n);
	 	        		}
	 	        		
	 	        		bool otherStable = tc.Tiles.OfType<Stable>().Any(t => t.row == row && t.column == col);
	 	        		if(otherStable)
	 	        		{
	 	        			ReleaseLayable(n);
	 	        		}
	 	        		
						PlaceLayable(n, row, col);
	 	        	}
	 	        	else if(n is FarmAnimal)
	 	        	{
	 	        		TownContext tc = this.DataContext as TownContext;
	 	        		
	 	        		FarmAnimal fa = n as FarmAnimal;
	
	 	        		List<FarmAnimal> intersectingFarmAnimals = tc.Tiles.OfType<FarmAnimal>().Where(s => s.row == row && s.column == col).ToList();
	 	        		if(intersectingFarmAnimals.Count > 0)
	 	        		{
	 	        			if(intersectingFarmAnimals[0].type != fa.type)
	 	        			{
	 	        				ReleaseLayable(fa);
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
	 	        			if(intersectingFarmAnimals.Count + 1 <= cap)
	 	        			{
	 	        				PlaceLayable(n, row, col);
			 	        		
			 	        		return;
	 	        			}
	 	        		}
	 	        		List<Tile> meadows= intersectingTiles.Where(t => t.type == Tile.Type.Meadow || t.type == Tile.Type.MeadowField).ToList();
	 	        		if(meadows.Count == 1)
	 	        		{
	 	        			if(fa.type == FarmAnimal.Type.Sheep)
	 	        			{
	 	        				List<Dog> intersectingDogs = tc.Tiles.OfType<Dog>().Where(d => d.row == row && d.column == col).ToList();
	 	        				if(intersectingDogs.Count > 0 ? intersectingDogs.Count + 1 >= intersectingFarmAnimals.Count : false)
	 	        				{
									PlaceLayable(n, row, col);
				 	        		
				 	        		return;
	 	        				}
	 	        			}
	 	        		}
	 	        		ReleaseLayable(n);
	 	        		return;
	 	        	}
	 	        	else if(n is Sowable)
	 	        	{
	 	        		Sowable s = n as Sowable;
	 	        	}
	 	        }
 	        	ReleaseLayable(n);
 	        	return;
			}
	    }
	    
	    private void ResetLayable(Layable l)
	    {
	    	if(!(l.row == 0 && l.column == 0))
	    	{
	    		string dockname = String.Format("Panel{0}{1}", l.column, l.row);
	 	        object uc = this.FindName(dockname);
	 	        if(uc is DockPanel && String.Compare((uc as DockPanel).Name, dockname) == 0)
	 	        {
	 	        	DockPanel dp = (uc as DockPanel);
	 	        	
	 	        	l.X = dp.Margin.Left;
	 	        	l.Y = dp.Margin.Top;
	 	        	return;
	 	        }
	    	}
	    	l.X = 0;
	    	l.Y = 0;
	    }
	    
	   	private void ReleaseLayable(Layable l)
	    {
	   		if(l is Tile)
	    	{
	   			ReleaseFarmAnimals(l.row, l.column);
	   			if((l as Tile).Twin)
	   			{
	   				ReleaseLayable((l as Tile).GetTwinTile());
	   			}
	    	}
	   		
	    	l.X = 0;
	    	l.Y = 0;
	    	l.column = 0;
	    	l.row = 0;
	    }
	    
	   	private void PlaceLayable(Layable l, int row, int col)
	   	{
			string dockname = String.Format("Panel{0}{1}", col, row);
 	        object uc = this.FindName(dockname);
 	        if(uc is DockPanel && String.Compare((uc as DockPanel).Name, dockname) == 0)
 	        {
 	        	DockPanel dp = (uc as DockPanel);
 	        	
				l.column = col;
	    		l.row = row;
	    		l.X = dp.Margin.Left;
	    		l.Y = dp.Margin.Top;
 	        }
	   	}
	   	
	   	private void ReleaseFarmAnimals(int row, int col)
	   	{
	   		TownContext tc = this.DataContext as TownContext;
	   		List<FarmAnimal> occupyingFarmAnimals = tc.Tiles.OfType<FarmAnimal>().Where(fa => fa.row == row && fa.column == col).ToList();
	 	    occupyingFarmAnimals.ForEach(fa => ReleaseLayable(fa));
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
    			case Tile.Type.Tunnel:
	   				if(col < 4)
	   				{
	   					return false;
	   				}
	   				isPreTile = true;
	   				break;
    			case Tile.Type.MeadowField:
    			case Tile.Type.Field:
    			case Tile.Type.Meadow:
	   				if(col > 3)
	   				{
	   					return false;
	   				}
	   				isPreTile = true;
    				break;
    			case Tile.Type.BigFence:
    			case Tile.Type.Fence:
    				acceptableTiles.Add(Tile.Type.Meadow);
    				acceptableTiles.Add(Tile.Type.MeadowField);
    				break;
    			case Tile.Type.OreMine:
    				acceptableTiles.Add(Tile.Type.Tunnel);
    				//acceptableTiles.Add(Tile.Type.TunnelDummy);
    				break;
    			case Tile.Type.RubyMine:
    				acceptableTiles.Add(Tile.Type.Tunnel);
    				//acceptableTiles.Add(Tile.Type.TunnelDummy);
    				//acceptableTiles.Add(Tile.Type.DeepTunnelDummy);
    				break;
	   		}
	   		TownContext tc = (DataContext as TownContext);
	   		
	   		List<Tile> intersectingTiles = tc.Tiles.OfType<Tile>().Where(t => t.row == row && t.column == col && t != tile && t != tile.GetTwinTile()).ToList();
	   		List<Tile> rightPreTiles = intersectingTiles.Where(t => acceptableTiles.Any(type => type == t.type)).ToList();
	   		
	   		if(tile.Twin)
	   		{
    			switch(tile.Rot)
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
    				default:
    					break;
    			}
	   			
    			switch(tile.type)
	   			{
	    			case Tile.Type.CaveCave:
	    			case Tile.Type.CaveTunnel:
	    			case Tile.Type.Cave:
	    			case Tile.Type.Tunnel:
		   				if(col < 4)
		   				{
		   					return false;
		   				}
		   				break;
	    			case Tile.Type.MeadowField:
	    			case Tile.Type.Field:
	    			case Tile.Type.Meadow:
		   				if(col > 3)
		   				{
		   					return false;
		   				}
		   				break;
    			}
    			
	   			List<Tile> intersectingTiles2 = tc.Tiles.OfType<Tile>().Where(t => t.row == row && t.column == col && t != tile && t != tile.GetTwinTile()).ToList();
	   			List<Tile> rightPreTiles2 = intersectingTiles2.Where(t => acceptableTiles.Any(type => type == t.type)).ToList();
	   			
	   			if(isPreTile)
	   				return intersectingTiles.Count == 0 && intersectingTiles2.Count == 0;
	   			else
	   				return rightPreTiles.Count > 0 && rightPreTiles2.Count > 0;
	   		}
	   		else
	   		{
	   			if(isPreTile)
	   				return intersectingTiles.Count == 0;
	   			else
	   				return rightPreTiles.Count > 0;
	   		}
	   	}
	   	
//	   	private bool isAllowed(Tile tile, int col, int row)
//	   	{
//	 	    List<BoardTile.Type> acceptableTiles = new List<BoardTile.Type>();
//    		
//    		switch(tile.type)
//    		{
//    			case Tile.Type.BigFence:
//    			case Tile.Type.Fence:
//    				acceptableTiles.Add(BoardTile.Type.Meadow);
//    				break;
//    			case Tile.Type.CaveCave:
//    			case Tile.Type.CaveTunnel:
//    			case Tile.Type.Cave:
//    			case Tile.Type.Tunnel:
//    				acceptableTiles.Add(BoardTile.Type.Rock);
//    				break;
//    			case Tile.Type.MeadowField:
//    			case Tile.Type.Field:
//    			case Tile.Type.Meadow:
//    				acceptableTiles.Add(BoardTile.Type.Forest);
//    				break;
//    			case Tile.Type.OreMine:
//    				acceptableTiles.Add(BoardTile.Type.Tunnel);
//    				break;
//    			case Tile.Type.RubyMine:
//    				acceptableTiles.Add(BoardTile.Type.Tunnel);
//    				acceptableTiles.Add(BoardTile.Type.DeepTunnel);
//				break;
//				default:
//					break;
//    		}
//    		TownContext tc = (DataContext as TownContext);
//    		BoardTile bt = tc.boardtiles[col,row];
//    		
//    		if(bt == null || !acceptableTiles.Exists(at => at == bt.state))
//    			return false;
//    		
//    		if(tile.Twin)
//    		{
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
//    			BoardTile bt2 = tc.boardtiles[col,row];
//    			if(bt2 == null || !acceptableTiles.Exists(at => at == bt2.state))
//    				return false;
//    		}
//    		return true;
//	   	}
	   	
	   	private bool isAdjacent(Tile tile, int col, int row)
	   	{
	   		TownContext tc = (DataContext as TownContext);
	   		var adjs1 = tc.Tiles.Where(t => ((t.row == row && Math.Abs(t.column - col) == 1 ) || (t.column == col && Math.Abs(t.row - row) == 1)) && t != tile && t != tile.GetTwinTile() ).ToList();
	   		bool bool1 = adjs1.Count > 0;
	   		if(tile.Twin)
	   		{
	   			switch(tile.Rot)
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
	   			var adjs2 = tc.Tiles.Where(t => ((t.row == row && Math.Abs(t.column - col) == 1 ) || (t.column == col && Math.Abs(t.row - row) == 1)) && t != tile && t != tile.GetTwinTile() ).ToList();
	   			bool bool2 = adjs2.Count > 0;
	   			return bool1 || bool2;
	   		}
	   		return bool1;
	   	}
	   	
	   	//needs work but it works for my purposes
//	   	private bool isAdjacent(Tile t, int col, int row)
//	   	{
//	   		TownContext tc = (DataContext as TownContext);
//	   		List<KeyValuePair<int,int>> coords = new List<KeyValuePair<int, int>>() 
//	   		{
//	   			new KeyValuePair<int,int>(col - 1, row),
//	   			new KeyValuePair<int,int>(col + 1, row),
//	   			new KeyValuePair<int,int>(col, row-1),
//	   			new KeyValuePair<int,int>(col, row+1)
//	   		};
//	   		if(t.Twin)
//	   		{
//	   			switch(t.Rot)
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
//	   			coords.Add(new KeyValuePair<int,int>(col - 1, row));
//	   			coords.Add(new KeyValuePair<int,int>(col + 1, row));
//	   			coords.Add(new KeyValuePair<int,int>(col, row-1));
//	   			coords.Add(new KeyValuePair<int,int>(col, row+1));
//	   		}
//	   		
//	   		foreach(KeyValuePair<int,int> coord in coords)
//	   		{
//		   		try
//		   		{
//		   			BoardTile bt = tc.boardtiles[coord.Key, coord.Value];
//		   			if(bt != null && bt.state != BoardTile.Type.Rock && bt.state != BoardTile.Type.Forest)
//		   			   return true;
//		   			   
//		   		}
//		   		catch(IndexOutOfRangeException)
//		   		{
//		   			continue;
//		   		}
//	   		}
//	   		return false;
//	   	}
	}
}