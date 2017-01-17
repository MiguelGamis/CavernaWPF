/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 1/16/2017
 * Time: 1:58 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace CavernaWPF.ActionCardControls
{
	/// <summary>
	/// Description of ActionCardRadio.
	/// </summary>
	public class ActionCardRadioButton : ActionCardOption
	{
		public List<string> Options
		{
			get;
			set;
		}
		
		public ActionCardRadioButton(List<String> options, bool mandatory = false)
		{
			Options = options;
			Control =  new ActionCardRadioButtonControl();
			Control.DataContext = this;
		}
	}
}
