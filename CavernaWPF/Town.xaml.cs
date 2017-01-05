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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections.ObjectModel;
using CavernaWPF.Layable;
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

	    private void Thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
	    {
	        Tile n = (Tile)((FrameworkElement)sender).DataContext;
	        n.X += e.HorizontalChange;
	        n.Y += e.VerticalChange;
	    }
	    
	    private void Thumb_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
	    {
	        Tile n = (Tile)((FrameworkElement)sender).DataContext;
			double x = n.X + 35.0;
			double y = n.Y + 35.0;
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
	        y -= 25.0;
	        y /= 70.0;
	        int _x = (int) x;
	        int _y = (int) y;
	        string dockname = String.Format("Panel{0}{1}", _x, _y);
 	        object uc = this.FindName(dockname);
 	        if(uc != null && uc is DockPanel && String.Compare((uc as DockPanel).Name, dockname) == 0)
 	        {
 	        	n.X = (uc as DockPanel).Margin.Left;
 	        	n.Y = (uc as DockPanel).Margin.Top;
 	        }
 	        else
 	        {
 	        	n.X = 0;
 	        	n.Y = 0;
 	        }
	    }
	    
	   	private void Rotate(object sender, MouseButtonEventArgs e)
	    {
	        object dc = ((FrameworkElement)sender).DataContext;
	        if(dc is Tile)
	        {
	        	Tile t = dc as Tile;
	        	t.Rotate();
	        	t.X = 0;
 	        	t.Y = 0;
	        }
	    }
	}
}