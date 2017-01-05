/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 1/4/2017
 * Time: 9:01 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Controls;

namespace CavernaWPF.ActionCardControls
{
	/// <summary>
	/// Description of ActionCardOption.
	/// </summary>
	public class ActionCardOption
	{
		public string Text { get; set; }
		public bool Selected { get; set; }
		public UserControl Control { get; set; }
		public ActionCardOption()
		{
			Control =  new ActionCardCheckBox();
			Control.DataContext = this;
		}
	}
}
