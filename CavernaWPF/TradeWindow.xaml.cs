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
			var tag = (sender as Button).Tag;
			if(tag is Int32)
			{
				int tradeIndex = (int) tag;
				TradeManager tm = (this.DataContext as TradeManager);
				int index = 0;
				foreach(KeyValuePair<List<ResourceTab>,List<ResourceTab>> obj in tm.Exchanges)
				{
					if(tradeIndex == index)
					{
						TradeInput.ItemsSource = obj.Key;
						tradeInput = obj.Key;
						TradeOutput.ItemsSource = obj.Value;
						tradeOutput = obj.Value;
						break;
					}
					index++;
				}
			}
		}
		
		private List<ResourceTab> tradeInput;
		
		private List<ResourceTab> tradeOutput;
		
		public void OKButton_Click(object sender, RoutedEventArgs args)
		{
			TradeManager tm = (this.DataContext as TradeManager);
			
			bool hasEnough = true;
			foreach(ResourceTab input in tradeInput)
			{
				hasEnough&=tm.player.Resources[input.ResourceType].Amount>=input.Amount;
			}
			if(hasEnough)
			{
				foreach(ResourceTab input in tradeInput)
				{
					tm.player.Resources[input.ResourceType].Amount-=input.Amount;
				}
				foreach(ResourceTab input in tradeOutput)
				{
					tm.player.Resources[input.ResourceType].Amount+=input.Amount;
				}
			}
		}
		
		public void CancelButton_Click(object sender, RoutedEventArgs args)
		{
			Close();
		}
	}
}