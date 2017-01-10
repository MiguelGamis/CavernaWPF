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
		        int _x = (int) x;
		        int _y = (int) y;
		        
		        
		        string dockname = String.Format("Panel{0}{1}", _x, _y);
	 	        object uc = this.FindName(dockname);
	 	        if(uc is DockPanel && String.Compare((uc as DockPanel).Name, dockname) == 0)
	 	        {
	 	        	DockPanel dp = (uc as DockPanel);
	 	        	
	 	        	if(n is Tile)
	 	        	{
	 	        		Tile t = n as Tile;
	 	        		
		 	       		//see if the boardtile(s) is open

		 	       		
		 	       		if(!isAllowed(t, _x+1, _y+1))
			        	{
			  	        	t.X = 0;
		 	        		t.Y = 0;
		 	        		return;
			        	}
				        
		 	       		if(!isAdjacent(t, _x+1, _y+1))
				        {
		 	       			t.X = 0;
		 	        		t.Y = 0;
		 	        		return;
				        }
	 	        	}
			        	
	 	        	n.X = (uc as DockPanel).Margin.Left;
	 	        	n.Y = (uc as DockPanel).Margin.Top;
	 	        	
//	 	        	tc.Tiles.Remove(obj);
//	 	        	
//	 	        	BitmapImage bimage = new BitmapImage();
//					bimage.BeginInit();
//					bimage.UriSource = new Uri(n.Img, UriKind.Absolute);
//					bimage.EndInit();
//	 	        	Image img = new Image() {Source = bimage, Height=n.Height, Width=n.Width};
//	 	        	if(n is Tile)
//	 	        	{
//	 	        		Tile t = n as Tile;
//	 	        		if(t.Twin)
//	 	        		{
//	 	        			RotateTransform rotateTransform = new RotateTransform(t.Rot*90, t.Width/2, t.Height/2);
//	 	        			img.RenderTransform = rotateTransform;	
//	 	        		}
//	 	        	}
//	 	        	
//	 	        	(uc as DockPanel).Children.Add(img);
	 	        }
	 	        else
	 	        {
	 	        	n.X = 0;
	 	        	n.Y = 0;
	 	        }
			}
	    }
	    
	   	private void Rotate(object sender, MouseButtonEventArgs e)
	    {
	        object dc = ((FrameworkElement)sender).DataContext;
	        if(dc is Tile)
	        {
	        	Tile t = dc as Tile;
	        	if(t.Twin)
	        	{
		        	t.Rotate();
//		        	t.X = 0;
//		        	t.Y = 0;
	        	}
	        }
	    }
	   	
	   	private bool isAllowed(Tile tile, int col, int row)
	   	{
	 	    List<BoardTile.Type> acceptableTiles = new List<BoardTile.Type>();
    		
    		switch(tile.type)
    		{
    			case Tile.Type.BigFence:
    			case Tile.Type.Fence:
    				acceptableTiles.Add(BoardTile.Type.Meadow);
    				break;
    			case Tile.Type.CaveCave:
    			case Tile.Type.CaveTunnel:
    			case Tile.Type.Cave:
    			case Tile.Type.Tunnel:
    				acceptableTiles.Add(BoardTile.Type.Rock);
    				break;
    			case Tile.Type.MeadowField:
    			case Tile.Type.Field:
    			case Tile.Type.Meadow:
    				acceptableTiles.Add(BoardTile.Type.Forest);
    				break;
    			case Tile.Type.OreMine:
    				acceptableTiles.Add(BoardTile.Type.Tunnel);
    				break;
    			case Tile.Type.RubyMine:
    				acceptableTiles.Add(BoardTile.Type.Tunnel);
    				acceptableTiles.Add(BoardTile.Type.DeepTunnel);
				break;
				default:
					break;
    		}
    		TownContext tc = (DataContext as TownContext);
    		BoardTile bt = tc.boardtiles[col,row];
    		
    		if(bt == null || !acceptableTiles.Exists(at => at == bt.state))
    			return false;
    		
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
    			
    			BoardTile bt2 = tc.boardtiles[col,row];
    			if(bt2 == null || !acceptableTiles.Exists(at => at == bt2.state))
    				return false;
    		}
    		return true;
	   	}
	   	
	   	//needs work but it works for my purposes
	   	private bool isAdjacent(Tile t, int col, int row)
	   	{
	   		TownContext tc = (DataContext as TownContext);
	   		List<KeyValuePair<int,int>> coords = new List<KeyValuePair<int, int>>() 
	   		{
	   			new KeyValuePair<int,int>(col - 1, row),
	   			new KeyValuePair<int,int>(col + 1, row),
	   			new KeyValuePair<int,int>(col, row-1),
	   			new KeyValuePair<int,int>(col, row+1)
	   		};
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
    				default:
    					break;
    			}
	   			coords.Add(new KeyValuePair<int,int>(col - 1, row));
	   			coords.Add(new KeyValuePair<int,int>(col + 1, row));
	   			coords.Add(new KeyValuePair<int,int>(col, row-1));
	   			coords.Add(new KeyValuePair<int,int>(col, row+1));
	   		}
	   		
	   		foreach(KeyValuePair<int,int> coord in coords)
	   		{
		   		try
		   		{
		   			BoardTile bt = tc.boardtiles[coord.Key, coord.Value];
		   			if(bt != null && bt.state != BoardTile.Type.Rock && bt.state != BoardTile.Type.Forest)
		   			   return true;
		   			   
		   		}
		   		catch(IndexOutOfRangeException ex)
		   		{
		   			continue;
		   		}
	   		}
	   		return false;
	   	}
	}
}