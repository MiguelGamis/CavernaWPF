﻿/*
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
using CavernaWPF.Resources;
using System.Linq;

namespace CavernaWPF
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window
	{
		public Window1()
		{
			LayoutManager.Instance.appWindow = this;
			
			StackPanel rootPanel = new StackPanel() { Orientation = Orientation.Vertical };
			rootPanel.Height = this.Height;
			rootPanel.Width = this.Width;
			
			Grid.SetRow(ActionBoardContext.Instance.control, 0);
			StackPanel publicPanel = new StackPanel() { Orientation = Orientation.Horizontal };
			publicPanel.Children.Add(ActionBoardContext.Instance.control);
			publicPanel.Children.Add(ActionBoardContext.Instance.harvesteventscard);
			publicPanel.Children.Add(ActionBoardContext.Instance.furnishingWindow);
			ActionBoardContext.Instance.startButton.Click += new RoutedEventHandler(StartGame);
			StackPanel gameProgressBar = new StackPanel();
			gameProgressBar.Children.Add(ActionBoardContext.Instance.startButton);
			gameProgressBar.Children.Add(ActionBoardContext.Instance.statusControl);
			publicPanel.Children.Add(gameProgressBar);
			rootPanel.Children.Add(publicPanel);
			
			this.Content = rootPanel;
			InitializeComponent();
			
			PrepareGameContext pg = new PrepareGameContext();
			pg.Control.ShowDialog();
			
			if((bool) pg.Control.DialogResult == false)
				Environment.Exit(0);
			
			StackPanel playerPanels = new StackPanel() { Orientation = Orientation.Horizontal };
			
			foreach(PlayerSlot playerSlot in pg.PlayerSlots.Where(ps=>ps.Selected).ToList())
			{
				Player p = new Player(){Name = playerSlot.Name, Color = playerSlot.Color};
				ActionBoardContext.Instance.Players.Add(p);
				
				Grid playerPanel = new Grid();//StackPanel playerPanel = new StackPanel() { Orientation = Orientation.Vertical };
				playerPanel.RowDefinitions.Add(new RowDefinition());
				playerPanel.RowDefinitions.Add(new RowDefinition());
				playerPanel.RowDefinitions.Add(new RowDefinition());
				
				TownContext tc = new TownContext();
				p.town = tc;
				
				Dwarf dwarf1 = new Dwarf() { player = p, Level = 8}; Dwarf dwarf2 = new Dwarf() { player = p };
				p.Dwarfs.Add(dwarf1); p.Dwarfs.Add(dwarf2);
				
				Grid.SetRow(tc.control,0);
				Grid.SetZIndex(tc.control,1);
				playerPanel.Children.Add(tc.control);
				
				StackPanel bottomBoard = new StackPanel() {Orientation = Orientation.Horizontal};
				Grid.SetRow(bottomBoard,1);
				Grid.SetZIndex(bottomBoard,0);
				playerPanel.Children.Add(bottomBoard);
				
				Grid piecesPool = new Grid() { Width = 220, Height = 100 };
				bottomBoard.Children.Add(piecesPool);
				
				StackPanel southEastPanel = new StackPanel();
				bottomBoard.Children.Add(southEastPanel);
				
				StackPanel resourcesPanel = new StackPanel() {Orientation = Orientation.Horizontal};
				resourcesPanel.Children.Add(p.tab); 
				Button tradeButton = new Button() { Height = 30, Width = 60, Content = "Trade", Tag = p}; tradeButton.Click += new RoutedEventHandler(Trade);
				resourcesPanel.Children.Add(tradeButton);
				southEastPanel.Children.Add(resourcesPanel);
				
				DwarfQueue dwarfLineUp = new DwarfQueue();
				dwarfLineUp.ItemsSource = p.Dwarfs;
				Grid.SetRow(dwarfLineUp,2);
				southEastPanel.Children.Add(dwarfLineUp);
				
				LayoutManager.Instance.map.Add(p, playerPanel);
				
				playerPanels.Children.Add(playerPanel);
				
				if(playerSlot.StartingPlayer) ActionBoardContext.Instance.StartingPlayer = p;
			}
			Grid.SetRow(playerPanels, 1);
			rootPanel.Children.Add(playerPanels);
			
			ActionBoardContext.Instance.StartGame();
			
//			this.Loaded += Test;
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
		
		private void Test(object sender, RoutedEventArgs e)
		{
			Tile test = new Tile(Tile.Type.BigFence){Locked = true, Rot = 90};
			ActionBoardContext.Instance.Players[1].town.PositionLayable(test, 1, 1);
			FarmAnimal fa = new FarmAnimal(FarmAnimal.Type.Sheep){Locked = true};
			ActionBoardContext.Instance.Players[1].town.PositionLayable(fa, 1, 1);
			FarmAnimal fa2 = new FarmAnimal(FarmAnimal.Type.Sheep){Locked = true};
			ActionBoardContext.Instance.Players[1].town.PositionLayable(fa2, 1, 1);
		
			Tile test2 = new Tile(Tile.Type.Fence){Locked = true};
			ActionBoardContext.Instance.Players[1].town.PositionLayable(test2, 2, 1);
			FarmAnimal fa3 = new FarmAnimal(FarmAnimal.Type.Donkey){Locked = true};
			ActionBoardContext.Instance.Players[1].town.PositionLayable(fa3, 2, 1);
			FarmAnimal fa4 = new FarmAnimal(FarmAnimal.Type.Donkey){Locked = true};
			ActionBoardContext.Instance.Players[1].town.PositionLayable(fa4, 2, 1);
			
			Tile test3 = new Tile(Tile.Type.Fence){Locked = true};
			ActionBoardContext.Instance.Players[1].town.PositionLayable(test3, 2, 2);
			Stable stable = new Stable(ActionBoardContext.Instance.Players[1].Color){Locked = true};
			ActionBoardContext.Instance.Players[1].town.PositionLayable(stable, 2, 2);
			FarmAnimal fa5 = new FarmAnimal(FarmAnimal.Type.Boar){Locked = true};
			ActionBoardContext.Instance.Players[1].town.PositionLayable(fa5, 2, 2);
			FarmAnimal fa6 = new FarmAnimal(FarmAnimal.Type.Boar){Locked = true};
			ActionBoardContext.Instance.Players[1].town.PositionLayable(fa6, 2, 2);
			
			Tile test4 = new Tile(Tile.Type.Fence){Locked = true};
			ActionBoardContext.Instance.Players[1].town.PositionLayable(test4, 3, 1);
			FarmAnimal fa7 = new FarmAnimal(FarmAnimal.Type.Boar){Locked = true};
			ActionBoardContext.Instance.Players[1].town.PositionLayable(fa7, 3, 1);
			FarmAnimal fa8 = new FarmAnimal(FarmAnimal.Type.Boar){Locked = true};
			ActionBoardContext.Instance.Players[1].town.PositionLayable(fa8, 3, 1);
			
			FurnishingTile ft = new FurnishingTile();
			ft.Seam(ActionBoardContext.Instance.Players[1]);
			
			ActionBoardContext.Instance.FurnishingTiles["Seam"].player = ActionBoardContext.Instance.Players[1];
		}
		
	}
}