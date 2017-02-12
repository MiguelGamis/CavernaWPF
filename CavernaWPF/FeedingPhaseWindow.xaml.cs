/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 2/4/2017
 * Time: 3:11 PM
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
using System.Linq;
using System.Windows.Controls.Primitives;
using CavernaWPF.Layables;
using CavernaWPF.Resources;

namespace CavernaWPF
{
	/// <summary>
	/// Interaction logic for FeedingPhaseWindow.xaml
	/// </summary>
	public partial class FeedingPhaseWindow : UserControl
	{
		public FeedingPhaseWindow()
		{
			InitializeComponent();
		}
		
		private void Confirm(object sender, RoutedEventArgs e)
		{
			(DataContext as FeedingPhase).Confirm();
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
				
				double w = 17.5;
				double h = 17.5;
				double x = w + n.X;
				double y = h + n.Y;
				
				int row = (int) (x/48);
				int col = (int) (y/38);
				
				(DataContext as FeedingPhase).PlaceFood(n, row, col);
			}
		}
	}
}