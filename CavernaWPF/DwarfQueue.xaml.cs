/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 1/3/2017
 * Time: 1:08 PM
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
using CavernaWPF.Resources;

namespace CavernaWPF
{
	/// <summary>
	/// Interaction logic for DwarfQueue.xaml
	/// </summary>
	public partial class DwarfQueue : ItemsControl
	{
		public DwarfQueue()
		{
			InitializeComponent();
		}
		
		private void DwarfClick(object sender, RoutedEventArgs e)
		{
			Dwarf d = (sender as Grid).DataContext as Dwarf;
			if(ActionBoardContext.Instance.currentPlayer == null || ActionBoardContext.Instance.currentDwarf == null)
				return;
			if(ActionBoardContext.Instance.currentPlayer.Value == d.player && d.player.Resources[Resource.Type.Ruby].Amount > 0 && d.Level > ActionBoardContext.Instance.currentDwarf.Level)
			{
				MessageBoxResult result = MessageBox.Show("Do want to play this dwarf out of order for 1 Ruby?", "Play dwarf out of order", MessageBoxButton.YesNo, MessageBoxImage.Question);
				if (result == MessageBoxResult.Yes)
				{
					d.player.Resources[Resource.Type.Ruby].Amount--;
					d.player.Dwarfs.Add(ActionBoardContext.Instance.currentDwarf);
					d.player.Dwarfs.Remove(d);
					ActionBoardContext.Instance.AddDwarf(d);
					ActionBoardContext.Instance.Dwarfs.Remove(ActionBoardContext.Instance.currentDwarf);
				}
			}
		}
	}
}