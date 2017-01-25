/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 01/09/2017
 * Time: 02:34
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
	/// Interaction logic for TradeWindow.xaml
	/// </summary>
	public partial class TradeWindow : Window
	{
		public TradeWindow()
		{
			InitializeComponent();
		}
		
		//this is fucked...
		void Button_Click(object sender, RoutedEventArgs e)
		{
			var dc = (sender as Button).DataContext;
			var exchange = ((KeyValuePair<List<ResourceTab>, List<ResourceTab>>) dc);
			tradeInput = exchange.Key;
			TradeInput.ItemsSource = exchange.Key;
			tradeOutput = exchange.Value;
			TradeOutput.ItemsSource = exchange.Value;
		}
		
		private List<ResourceTab> tradeInput;
		
		private List<ResourceTab> tradeOutput;
		
		public void OKButton_Click(object sender, RoutedEventArgs args)
		{
			TradeManager tm = (this.DataContext as TradeManager);
			
			tm.Trade(tradeInput, tradeOutput);
		}
		
		public void CancelButton_Click(object sender, RoutedEventArgs args)
		{
			Close();
		}
	}
}