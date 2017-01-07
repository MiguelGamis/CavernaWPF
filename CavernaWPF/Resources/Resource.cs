/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 12/29/2016
 * Time: 00:37
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace CavernaWPF.Resources
{
	/// <summary>
	/// Description of Resource.
	/// </summary>
	public class Resource : Tradable
	{	
		public enum Type {Wood, Stone, Ore, Ruby, Food, Gold,
						Grain, Vegetable,
						Sheep, Donkey, Boar, Cow};
		
		public Resource()
		{
		}
	}
}
