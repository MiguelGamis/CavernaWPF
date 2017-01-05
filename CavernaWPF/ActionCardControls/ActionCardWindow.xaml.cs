/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 1/4/2017
 * Time: 9:01 PM
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

namespace CavernaWPF.ActionCardControls
{
	/// <summary>
	/// Interaction logic for ActionCardWindow.xaml
	/// </summary>
	public partial class ActionCardWindow : Window
	{
		public ActionCardWindow()
		{
			InitializeComponent();
		}
		
		public void OKButton_Click(object sender, RoutedEventArgs args)
		{
			DialogResult = true;
			Close();
		}
		
		public void CancelButton_Click(object sender, RoutedEventArgs args)
		{
			DialogResult = false;
			Close();
		}
	}
}