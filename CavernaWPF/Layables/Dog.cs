/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 1/14/2017
 * Time: 4:12 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace CavernaWPF.Layables
{
	/// <summary>
	/// Description of Dog.
	/// </summary>
	public class Dog : Layable
	{
		public Dog()
		{
			Img = "pack://application:,,,/Images/Resources/Dog.png";
			Width=35;
			Height=35;
			Z = 5;
		}
	}
}
