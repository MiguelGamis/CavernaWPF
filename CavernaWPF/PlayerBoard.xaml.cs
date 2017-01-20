/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 01/18/2017
 * Time: 15:14
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

namespace CavernaWPF
{
	/// <summary>
	/// Interaction logic for PlayerBoard.xaml
	/// </summary>
	public partial class PlayerBoard : UserControl
	{
		public PlayerBoard()
		{
			InitializeComponent();
			Loaded += Setup;
		}
		
		private void Setup(object sender, RoutedEventArgs e)
		{
			int col = 0;
			foreach(ColumnDefinition cd in board.ColumnDefinitions.ToList())
			{
				if(col == 4)
				{
					col++;
					continue;
				}
				int row = 0;
				foreach(RowDefinition rd in board.RowDefinitions.ToList())
				{
					Board_Tile bt = new Board_Tile();
					Grid.SetColumn(bt, col);
					Grid.SetRow(bt, row);
					board.Children.Add(bt);
					row++;
				}
				col++;
			}
		}
	}
}