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
			(DataContext as FeedingPhase).FeedingTime = false;
		}
		
		void ComboBox_DropDownOpened(object sender, EventArgs e)
		{
			var feedingOption = sender as ComboBox;
			//feedingOption.ItemsSource = (DataContext as FeedingPhase).GetFeedingOptions();
		}
	}
}