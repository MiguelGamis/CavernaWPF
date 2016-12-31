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

namespace CavernaWPF
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window
	{
		public Window1()
		{	
			ActionBoardContext.Instance.Intitialize();
			StackPanel rootPanel = new StackPanel() { Orientation = Orientation.Vertical };
			rootPanel.Height = this.Height;
			rootPanel.Width = this.Width;
		
			Grid.SetRow(ActionBoardContext.Instance.control, 0);
			rootPanel.Children.Add(ActionBoardContext.Instance.control);
			
			StackPanel playerPanels = new StackPanel() { Orientation = Orientation.Horizontal };
			
			//----------------
			Dwarf testDwarf = null;
			//----------------
			
			int numPlayers = 2;
			for(int i = 0; i < numPlayers; i++)
			{
				Player p = new Player();
				
				StackPanel playerPanel = new StackPanel() { Orientation = Orientation.Vertical };
				TownContext tc = new TownContext();
				
				p.town = tc;
				Dwarf dwarf1 = new Dwarf() { player = p }; Dwarf dwarf2 = new Dwarf() { player = p };
				p.Dwarfs.Add(dwarf1); p.Dwarfs.Add(dwarf2);
				
				playerPanel.Children.Add(tc.control);
				playerPanel.Children.Add(tc.tab);
				
				Grid.SetRow(playerPanel, 1);
				playerPanels.Children.Add(playerPanel);
				
				if(testDwarf == null)
					testDwarf = dwarf1;
			}
			rootPanel.Children.Add(playerPanels);
			
			this.Content = rootPanel;
			InitializeComponent();
			
			ActionBoardContext.Instance.PromptDwarf(testDwarf);
		}
	}
}