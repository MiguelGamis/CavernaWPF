/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 01/16/2017
 * Time: 19:19
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace CavernaWPF.Layables
{
	/// <summary>
	/// Description of Stable.
	/// </summary>
	public class Stable : Layable
	{
		public Stable(string color)
		{
			Img = String.Format("pack://application:,,,/Images/Stables/{0}Stable.png",color);
			Width=35;
			Height=35;
			Z = 4;
		}
	}
}
