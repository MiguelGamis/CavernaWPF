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
			//this.Loaded += BackgroundInitialize;
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
		
		private void MakeTiles(object sender, RoutedEventArgs e)
		{
			DockPanel dp = new DockPanel();
		}
	}
}