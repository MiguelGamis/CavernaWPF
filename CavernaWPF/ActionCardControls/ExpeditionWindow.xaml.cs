/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 1/5/2017
 * Time: 1:15 AM
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
using System.Windows.Controls.Primitives;
using System.Linq;

namespace CavernaWPF.ActionCardControls
{
	/// <summary>
	/// Interaction logic for Expedition.xaml
	/// </summary>
	public partial class ExpeditionWindow : Window
	{
		private IEnumerable<ToggleButton> loot;
		
		public ExpeditionWindow(int level, int max)
		{
			InitializeComponent();
			this.level = level;
			this.max = max;
			loot = FindVisualChildren<ToggleButton>(this);
			loot = loot.Where(l => l.Parent is Grid ? Int32.Parse((l.Parent as Grid).Name.TrimStart("Level".ToCharArray())) <= level : false );
			Loaded += Loaded_Method;
		}
		
		private void Loaded_Method(object sender, RoutedEventArgs e)
		{
			foreach(ToggleButton tb in loot)
			{
				tb.IsEnabled = true;
			}
		}
		
		private int level;
		
		private int max;
		
		private int totalChecked = 0;
		
		void Check_Event(object sender, EventArgs e)
		{
				ToggleButton ctrl = (sender as ToggleButton);
				string ctrlName = ctrl.Name;
				Grid parentGrid = (ctrl.Parent as Grid);
				bool isRadio = parentGrid.Children.Count > 1;
				if(isRadio)
				{
					foreach(UIElement child in (ctrl.Parent as Grid).Children)
					{
						ToggleButton tb = (child as ToggleButton);
						if(tb.Name != ctrlName)
						{
							if((bool) tb.IsChecked)
								tb.IsChecked = false;
						}
					}
				}
				totalChecked++;
				
				if(totalChecked == max)
				{
					foreach(ToggleButton tb in loot.Where(tb => tb.IsChecked == false))
					{	
						Grid grdprnt = (tb.Parent as Grid);
						if(grdprnt.Children.Count > 1)
						{
							if(grdprnt.Children.OfType<ToggleButton>().Any(child => child.IsChecked == true))
								continue;
						}
						tb.IsEnabled = false;
					}
				}
				ctrl.Opacity = 0.3;
		}
		
		void Uncheck_Event(object sender, EventArgs e)
		{
			totalChecked--;
			ToggleButton ctrl = (sender as ToggleButton);
			ctrl.Opacity = 0;
			foreach(ToggleButton tb in loot)
			{	
				tb.IsEnabled = true;
			}
		}
		
		public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
		{
		    if (depObj != null)
		    {
		        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
		        {
		            DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
		            if (child != null && child is T)
		            {
		                yield return (T)child;
		            }
		
		            foreach (T childOfChild in FindVisualChildren<T>(child))
		            {
		                yield return childOfChild;
		            }
		        }
		    }
		}
		
		public void OKButton_Click(object sender, RoutedEventArgs args)
		{
			int numChecked = loot.Count(tb => tb.IsChecked == true);
			if(numChecked == max || numChecked == level)
			{
				(this.DataContext as Expedition).Loot =  loot.Where(l => l.IsChecked == true).Select(l => l.Name).ToList();
				DialogResult = true;
				Close();
			}
		}
		
		public void CancelButton_Click(object sender, RoutedEventArgs args)
		{
//			DialogResult = false;
//			Close();
		}
	}
}