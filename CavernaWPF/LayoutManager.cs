/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 1/14/2017
 * Time: 4:07 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace CavernaWPF
{
	/// <summary>
	/// Description of LayoutManager.
	/// </summary>
	public sealed class LayoutManager
	{
		private static LayoutManager instance = new LayoutManager();
		
		public static LayoutManager Instance {
			get {
				return instance;
			}
		}
		
		private LayoutManager()
		{
			BitmapImage startingPlayerPieceSource = new BitmapImage();
//			startingPlayerPieceSource.BeginInit();
//			startingPlayerPieceSource.UriSource = new Uri("pack://application:,,,/Images/StartingPlayer.png", UriKind.RelativeOrAbsolute);
//			startingPlayerPieceSource.EndInit();
			startingPlayerPiece.Source = startingPlayerPieceSource;
			Grid.SetRow(startingPlayerPiece, 3);
		}
		
		public Dictionary<Player, Grid> map = new Dictionary<Player, Grid>();
		
		public Dictionary<Player, StackPanel> map2 = new Dictionary<Player, StackPanel>();
		
		public Window appWindow;
		
		public Image startingPlayerPiece = new Image(){Height = 40, Width = 28};
	}
}
