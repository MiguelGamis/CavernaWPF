/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 01/28/2017
 * Time: 01:09
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace CavernaWPF.ActionCardControls
{
	/// <summary>
	/// Description of ActionCardTicker.
	/// </summary>
	public class ActionCardTicker : ActionCardOption
	{
		public int Minimum
		{
			get;
			set;
		}
		
		public int Maximum
		{
			get;
			set;
		}
		
		public ActionCardTicker(int min, int max)
		{
			Control = new ActionCardTickerControl();
			Control.DataContext = this;
			Minimum = min;
			Maximum = max;
			Value = min;
		}
	}
}
