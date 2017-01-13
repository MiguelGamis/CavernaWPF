/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 01/11/2017
 * Time: 19:10
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace CavernaWPF.ActionCardControls
{
	/// <summary>
	/// Description of WeaponForging.
	/// </summary>
	public class WeaponForging
	{
		public WeaponForging(Dwarf dwarf)
		{
			WeaponForgingWindow wfw = new WeaponForgingWindow();
			wfw.ShowDialog();
		}
	}
}
