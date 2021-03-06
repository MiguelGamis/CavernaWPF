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
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Linq;

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
			
			if(n.Locked)
			{
				(sender as Thumb).DragDelta -= Thumb_DragDelta;
				(sender as Thumb).DragCompleted -= Thumb_DragCompleted;
				return;
			}
			
	        n.X += e.HorizontalChange;
	        n.Y += e.VerticalChange;
	    }
	    
	    private void Thumb_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
	    {
			Dwarf n = (Dwarf)((FrameworkElement)sender).DataContext;
			
			if(n.Locked)
			{
				(sender as Thumb).DragDelta -= Thumb_DragDelta;
				(sender as Thumb).DragCompleted -= Thumb_DragCompleted;
				return;
			}
			
			//Snap dwarf to closest action card location
			double w = n.Width/2;
			double h = n.Height/2;
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
					n.X = _x + rd.ActualWidth/2 - w;
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
					n.Y = _y + rd.ActualHeight/2 - w;
					break;
				}
			}
			
			int actionCardIndex = col*3 + row;
			
			//Activate the correct action card
			ActionBoardContext abc = (this.DataContext as ActionBoardContext);
			try
			{
				ActionCardWrapper acw = abc.ActionCards[actionCardIndex];
				if(!acw.occupied && !acw.Hidden)
				{
					ActionCard ac = acw.actionCard;
					Action<Dwarf> action = ac.PlayerAction;
					if(action != null)
					{
						action.Invoke(n);
						//TODO:Action of Action card should return a bool to make dwarf immovable and set action card occupied
						if(ActionBoardContext.Instance.readyForNextDwarf)
						{
							acw.occupied = true;
							if(!ac.Accumulators.Any(a => a.Amount > 0 )) acw.AccumulatedResources.Clear();
							(sender as Thumb).DragDelta -= Thumb_DragDelta;
							(sender as Thumb).DragCompleted -= Thumb_DragCompleted;
							return;
						}
						else
						{
							n.X = 0;
							n.Y = 0;
						}
					}
				}
				else
				{
					n.X = 0;
					n.Y = 0;
				}
			}
			catch(IndexOutOfRangeException)
			{
				n.X = 0;
				n.Y = 0;
			}
	    }
	}
}