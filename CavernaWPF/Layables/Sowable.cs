/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 01/12/2017
 * Time: 03:30
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace CavernaWPF.Layables
{
	/// <summary>
	/// Description of Sowable.
	/// </summary>
	public class Sowable : Layable
	{
		public enum Type {Grain, Vegetable};
		
		public Type type;
		
		public Sowable(Type type)
		{
			switch(type)
			{
				case Type.Grain:
					Img = "C:\\Users\\Miguel\\Desktop\\Caverna\\Grain.png"; 
					break;
				case Type.Vegetable:
					Img = "C:\\Users\\Miguel\\Desktop\\Caverna\\Vegetable.png"; 
					break;
			}
			Width=45;
			Height=35;
		}
	}
}
