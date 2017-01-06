﻿/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 12/27/2016
 * Time: 23:57
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

namespace CavernaWPF
{
	/// <summary>
	/// Interaction logic for ActionBoard.xaml
	/// </summary>
	public partial class ActionBoard : UserControl
	{
		public ActionBoard()
		{
			InitializeComponent();
		}
		
		private void Thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
	    {
			Dwarf n = (Dwarf)((FrameworkElement)sender).DataContext;
	        n.X += e.HorizontalChange;
	        n.Y += e.VerticalChange;
	    }
	    
	    private void Thumb_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
	    {
			Dwarf n = (Dwarf)((FrameworkElement)sender).DataContext;
			double w = ((FrameworkElement)sender).Width;
			double h = ((FrameworkElement)sender).Height;
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
					n.X = _x;
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
					n.Y = _y;
					break;
				}
			}
			ActionBoardContext abc = (this.DataContext as ActionBoardContext);
			ActionCard ac = abc.ActionCards[2];
			Action<Player> action = ac.PlayerAction;
			if(action != null)
			{
				action.Invoke(n.player);
			}
	    }
	}
}