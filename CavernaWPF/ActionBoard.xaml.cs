/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 12/27/2016
 * Time: 23:57
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
	/// Interaction logic for ActionBoard.xaml
	/// </summary>
	public partial class ActionBoard : UserControl
	{
		public ActionBoard()
		{
			InitializeComponent();
			
			//this.DataContext = ActionBoardContext.Instance;
		}
		
		private void Thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
	    {
			
	    }
	    
	    private void Thumb_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
	    {

	    }
	}
}