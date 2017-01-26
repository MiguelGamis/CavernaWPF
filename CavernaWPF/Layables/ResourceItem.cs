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
		
		public Resource.Type type;
		
		public ResourceItem(Resource.Type resourcetype)
		{
			type = resourcetype;
			switch(type)
			{
				case Resource.Type.Wood:
					Img = "C:\\Users\\Miguel\\Desktop\\Caverna\\Wood.png"; 
					break;
				case Resource.Type.Stone:
					Img = "C:\\Users\\Miguel\\Desktop\\Caverna\\Stone.png"; 
					break;
				case Resource.Type.Ore:
					Img = "C:\\Users\\Miguel\\Desktop\\Caverna\\Ore.png"; 
					break;
				case Resource.Type.Ruby:
					Img = "C:\\Users\\Miguel\\Desktop\\Caverna\\Ruby.png";
					break;					
				case Resource.Type.Food:
					Img = "C:\\Users\\Miguel\\Desktop\\Caverna\\Food.png"; 
					break;
				case Resource.Type.Sheep:
					Img = "C:\\Users\\Miguel\\Desktop\\Caverna\\Sheep.png"; 
					break;
				case Resource.Type.Donkey:
					Img = "C:\\Users\\Miguel\\Desktop\\Caverna\\Donkey.png"; 
					break;
			}
			Width=35;
			Height=35;
		}
	}
}
