/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 01/18/2017
 * Time: 15:18
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
	/// Interaction logic for BoardTile.xaml
	/// </summary>
	public partial class Board_Tile : UserControl
	{
		public Board_Tile()
		{
			InitializeComponent();
		}
		
		private void dragEnter(object sender, DragEventArgs e)
		{
			e.Effects = DragDropEffects.All;
		}
		
		private void drapDrop(object sender, DragEventArgs e)
		{
			
		}
		
		private void dragOver(object sender, DragEventArgs e)
		{
			grid.Background = new SolidColorBrush();
		}
	}
}