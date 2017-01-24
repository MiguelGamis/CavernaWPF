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
		public string Group
		{
			get;
			set;
		}
//		
//	    private void radioButton_CheckedChanged(object sender, EventArgs e)
//	    {
//	    	Selected = (bool) (Control as ActionCardRadioButtonControl).radiobutton.IsChecked;
//	    }
//	
//	    private void radioButton_Click(object sender, EventArgs e)
//	    {
//	    	if ((bool) (Control as ActionCardRadioButtonControl).radiobutton.IsChecked && !Selected)
//	            (Control as ActionCardRadioButtonControl).radiobutton.IsChecked = false;
//	        else
//	        {
//	            (Control as ActionCardRadioButtonControl).radiobutton.IsChecked = true;
//	            Selected = false;
//	        }
//	    }
		
		public ActionCardRadioButton(bool optional = false, string group = "default")
		{
			Group = group;
			Control =  new ActionCardRadioButtonControl();
			Control.DataContext = this;
			
//			if(optional)
//			{
//				(Control as ActionCardRadioButtonControl).radiobutton.Click += radioButton_Click;
//			}
		}
	}
}
