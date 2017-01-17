/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 01/16/2017
 * Time: 14:42
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
	public abstract class ActionCardOption
	{
		public string Text { get; set; }
		public bool Selected { get; set; }
		public UserControl Control { get; set; }
		public bool Able { get; set; }
	}
}
