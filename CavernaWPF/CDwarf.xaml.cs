/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 12/23/2016
 * Time: 15:42
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
	/// Interaction logic for Dwarf.xaml
	/// </summary>
	public partial class CDwarf : UserControl
	{
		Dwarf currentContext;
		
		public CDwarf()
		{
			InitializeComponent();
		}
	}
}