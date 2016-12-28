/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 12/23/2016
 * Time: 23:07
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
	/// Interaction logic for Village.xaml
	/// </summary>
	public partial class Village : ItemsControl
	{
		private bool overhang = false; 
		
		public Village()
		{
			Dwarfs = new ObservableCollection<Dwarf>();
			Dwarf d = new Dwarf();
			Dwarf d2 = new Dwarf();
			Dwarfs.Add(d);
			Dwarfs.Add(d2);
			
			InitializeComponent();

			this.ItemsSource = Dwarfs;
		}
		
		private ObservableCollection<Dwarf> dwarfs = new ObservableCollection<Dwarf>();
		
		public ObservableCollection<Dwarf> Dwarfs{ 
			get{ return dwarfs; }
			set{ dwarfs = value; }
		}

	    private void Thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
	    {
	        Dwarf n = (Dwarf)((FrameworkElement)sender).DataContext;
	        n.X += e.HorizontalChange;
	        n.Y += e.VerticalChange;
	    }
	    
	    private void Thumb_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
	    {
	        Dwarf n = (Dwarf)((FrameworkElement)sender).DataContext;
	        double villageWidth = this.ActualWidth;
	        double villageHeight = this.ActualHeight;
	        double blockWidth = villageWidth / 6;
	        double blockHeight = villageHeight / 4; 
	        n.X = ((int) (n.X / blockWidth)) * blockWidth;
	        n.Y = ((int) (n.Y / blockHeight)) * blockHeight;
	    }
	}
}