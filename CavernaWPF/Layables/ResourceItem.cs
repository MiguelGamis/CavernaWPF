/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 1/25/2017
 * Time: 11:55 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using CavernaWPF.Resources;

namespace CavernaWPF.Layables
{
	/// <summary>
	/// Description of ResourceItem.
	/// </summary>
	public class ResourceItem : Layable
	{
		//TODO: Might Want to put FarmAnimal and Sowable together with this class because there's really no difference
		
		public Resource.Type type
		{
			get;
			set;
		}
		
		public ResourceItem(Resource.Type resourcetype)
		{
			type = resourcetype;
			switch(type)
			{
				case Resource.Type.Wood:
					Img = "pack://application:,,,/Images/Resources/Wood.png"; 
					break;
				case Resource.Type.Stone:
					Img = "pack://application:,,,/Images/Resources/Stone.png"; 
					break;
				case Resource.Type.Ore:
					Img = "pack://application:,,,/Images/Resources/Ore.png"; 
					break;
				case Resource.Type.Ruby:
					Img = "pack://application:,,,/Images/Resources/Ruby.png";
					break;					
				case Resource.Type.Food:
					Img = "pack://application:,,,/Images/Resources/Food.png"; 
					break;
				case Resource.Type.Sheep:
					Img = "pack://application:,,,/Images/Resources/Sheep.png"; 
					break;
				case Resource.Type.Donkey:
					Img = "pack://application:,,,/Images/Resources/Donkey.png"; 
					break;
				case Resource.Type.Starvation:
					Img = "pack://application:,,,/Images/Starvation.png";
					break;
			}
			Width=35;
			Height=35;
		}
	}
}
