/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 12/23/2016
 * Time: 01:08
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
using System.Collections.ObjectModel;
using CavernaWPF.Layables;

namespace CavernaWPF
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window
	{
		public Window1()
		{	
			StackPanel rootPanel = new StackPanel() { Orientation = Orientation.Vertical };
			rootPanel.Height = this.Height;
			rootPanel.Width = this.Width;
		
			Grid.SetRow(ActionBoardContext.Instance.control, 0);
			StackPanel publicPanel = new StackPanel() { Orientation = Orientation.Horizontal };
			publicPanel.Children.Add(ActionBoardContext.Instance.control);
			publicPanel.Children.Add(ActionBoardContext.Instance.furnishingWindow);
			rootPanel.Children.Add(publicPanel);
			
			StackPanel playerPanels = new StackPanel() { Orientation = Orientation.Horizontal };
			
			List<string> colors = new List<string>() {"Blue","Yellow","Green","Purple"};
			
			int numPlayers = 2;
			for(int i = 0; i < numPlayers; i++)
			{
				Player p = new Player(); p.Color = colors[i];
				ActionBoardContext.Instance.players.Add(p);
				
				StackPanel playerPanel = new StackPanel() { Orientation = Orientation.Vertical };
				
				TownContext tc = new TownContext();
				p.town = tc;
				
				Dwarf dwarf1 = new Dwarf() { player = p}; Dwarf dwarf2 = new Dwarf() { player = p };
				p.Dwarfs.Add(dwarf1); p.Dwarfs.Add(dwarf2);
				
				playerPanel.Children.Add(tc.control);
				Button lockinButton = new Button() { Height = 30, Width = 60, Content = "Confirm", Tag = p }; LayoutManager.Instance.confirmButtons.Add(p, lockinButton);
				playerPanel.Children.Add(lockinButton);
				StackPanel resourcesPanel = new StackPanel() {Orientation = Orientation.Horizontal};
				resourcesPanel.Children.Add(p.tab); 
				Button tradeButton = new Button() { Height = 30, Width = 60, Content = "Trade", Tag = p}; tradeButton.Click += new RoutedEventHandler(Trade);
				resourcesPanel.Children.Add(tradeButton);
				playerPanel.Children.Add(resourcesPanel);
				
				LayoutManager.Instance.map.Add(p, playerPanel);
				
				DwarfQueue dwarfLineUp = new DwarfQueue();
				dwarfLineUp.ItemsSource = p.Dwarfs;
				playerPanel.Children.Add(dwarfLineUp);
				
				playerPanels.Children.Add(playerPanel);
			}
			Grid.SetRow(playerPanels, 1);
			rootPanel.Children.Add(playerPanels);
			
			ActionBoardContext.Instance.startButton.Click += new RoutedEventHandler(StartGame);
			Grid.SetRow(ActionBoardContext.Instance.startButton,2);
			rootPanel.Children.Add(ActionBoardContext.Instance.startButton);
			
			this.Content = rootPanel;
			InitializeComponent();
			
			ActionBoardContext.Instance.StartingPlayer = ActionBoardContext.Instance.players[1];
			ActionBoardContext.Instance.StartGame();
		}
		
		private void StartGame(object sender, RoutedEventArgs e)
		{
			ActionBoardContext.Instance.NextTurn();
		}
		
		private void Trade(object sender, RoutedEventArgs e)
		{
			TradeManager tm = new TradeManager();
			tm.PlayerTrading((sender as Button).Tag as Player);
		}
	}
}