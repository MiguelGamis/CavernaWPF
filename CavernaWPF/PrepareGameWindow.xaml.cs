/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 1/29/2017
 * Time: 3:56 AM
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
	/// Interaction logic for PrepareGameWindow.xaml
	/// </summary>
	public partial class PrepareGameWindow : Window
	{
		public PrepareGameWindow()
		{
			InitializeComponent();
		}
		
		private void ColorChanged(object sender, RoutedEventArgs e)
		{
			(DataContext as PrepareGameContext).UpdateColor(((sender as ComboBox).Parent as FrameworkElement).DataContext as PlayerSlot, (sender as ComboBox).SelectedValue as string);
		}
		
		private void UpdateColorOptions(object sender, EventArgs e)
		{
			(DataContext as PrepareGameContext).UpdateColorOptions(((sender as ComboBox).Parent as FrameworkElement).DataContext as PlayerSlot);
		}
		
		private void OKButtonClick(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
			Close();
		}
		
		private void CancelButtonClick(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
			Close();
		}
	}
}