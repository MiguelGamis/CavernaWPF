/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 1/21/2017
 * Time: 11:57 PM
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

namespace CavernaWPF
{
	/// <summary>
	/// Interaction logic for SkipFieldOrBreedWindow.xaml
	/// </summary>
	public partial class SkipFieldOrBreedWindow : Window
	{
		public SkipFieldOrBreedWindow()
		{
			InitializeComponent();
		}
		
		public void OKButton_Click(object sender, RoutedEventArgs args)
		{
			if((bool) Field.IsChecked || (bool) Breed.IsChecked )
			{
				if((bool) Field.IsChecked)
					DialogResult = true;
				else
					DialogResult = false;
			
				Close();
			}
		}
	}
}