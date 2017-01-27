/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 1/8/2017
 * Time: 6:21 PM
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
using System.Windows.Media.Imaging;
using CavernaWPF.Layables;

namespace CavernaWPF
{
	/// <summary>
	/// Interaction logic for FurnishingWindow.xaml
	/// </summary>
	public partial class FurnishingWindow : UserControl
	{
		public FurnishingWindow()
		{
			InitializeComponent();
		}
		
		private void Buy(object sender, RoutedEventArgs e)
		{
			ActionBoardContext abc = (DataContext as ActionBoardContext);
			if(abc.FurnishingCavern)
			{		
				object obj = ((FrameworkElement)sender).DataContext;
				
				if(obj is KeyValuePair<string,FurnishingTile>)
				{
					Player p = abc.currentPlayer.Value;
					FurnishingTile ft = ((KeyValuePair<string,FurnishingTile>) obj).Value;
					
					if(CanAfford(p,ft))
					{
						abc.FurnishingCavern = false;
						
						foreach(ResourceTab rt in ft.Cost)
						{
							//TODO: Add discount checking
							p.Resources[rt.type].Amount -= rt.Amount;
						}
						
						ft.player = p;
						ft.Effect.Invoke(abc.currentPlayer.Value);
						abc.currentPlayer.Value.town.AddTile(new Tile(Tile.Type.FurnishingTile){ Img = ft.Img });
					}
				}
			}
		}
		
		public bool CanAfford(Player p, FurnishingTile ft)
		{
			foreach(ResourceTab rt in ft.Cost)
			{
				if(p.Resources[rt.type].Amount < rt.Amount)
					return false;
			}
			return true;
		}
		
		private void BackgroundInitialize(object sender, RoutedEventArgs e)
		{
			BitmapImage bimage = new BitmapImage();
			bimage.BeginInit();
			bimage.UriSource = new Uri("C:\\Users\\Miguel\\Desktop\\Caverna\\Tiles.jpg", UriKind.Absolute);
			bimage.EndInit();
			//var bm = new Bitmap(bimage, new Size(this.Width, this.Height));
			this.Background = new ImageBrush(bimage);
		}
	}
}