/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 12/29/2016
 * Time: 00:47
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace CavernaWPF.Resources
{
	/// <summary>
	/// Description of Animal.
	/// </summary>
	public class FarmAnimal : Tradable
	{
		public enum Type {Sheep, Donkey, Boar, Cow};
		
		public Type type;
		
		public FarmAnimal()
		{
		}
	}
}
