/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 1/4/2017
 * Time: 10:01 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace CavernaWPF.ActionCardControls
{
	/// <summary>
	/// Description of ActionCardWindowContext.
	/// </summary>
	public class ActionCardWindowContext
	{
		public ActionCardWindow Control = new ActionCardWindow();
		public List<ActionCardOption> Options {get; set;}
		public string Name {get; set;}
		
		public ActionCardWindowContext()
		{
			Control.DataContext = this;
			Options = new List<ActionCardOption>();
		}
	}
}
