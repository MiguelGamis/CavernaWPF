/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 01/06/2017
 * Time: 01:59
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace CavernaWPF.ActionCardControls
{
	/// <summary>
	/// Description of Expedition.
	/// </summary>
	public class Expedition
	{
		public ExpeditionWindow Control = new ExpeditionWindow();
		public List<String> Loot = new List<String>();
		public Expedition()
		{
			Control.DataContext = this;
		}
	}
}
