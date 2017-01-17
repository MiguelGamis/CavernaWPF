/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 1/4/2017
 * Time: 9:01 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace CavernaWPF.ActionCardControls
{
	/// <summary>
	/// Description of ActionCardOption.
	/// </summary>
	public class ActionCardCheckBox : ActionCardOption
	{
		public ActionCardCheckBox(bool mandatory = false)
		{
			if(mandatory)
				Control = new ActionCardText();
			else
				Control =  new ActionCardCheckBoxControl();
			Control.DataContext = this;
		}
	}
}
