/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 01/28/2017
 * Time: 01:01
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

namespace CavernaWPF.ActionCardControls
{
	/// <summary>
	/// Interaction logic for ActionCardTickerControl.xaml
	/// </summary>
	public partial class ActionCardTickerControl : UserControl
	{
		public ActionCardTickerControl()
		{
			InitializeComponent();
		}
		
		private void ValueChanged(object sender, RoutedEventArgs e)
		{
			(DataContext as ActionCardTicker).Selected = (sender as Slider).Value > 0;
		}
	}
}