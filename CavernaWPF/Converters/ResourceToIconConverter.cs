/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 1/19/2017
 * Time: 5:18 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Globalization;
using System.Windows.Data;
using CavernaWPF.Resources;

namespace CavernaWPF.Converters
{
	/// <summary>
	/// Description of ResourceToIconConverter.
	/// </summary>
	public class ResourceToIconConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string ImgSrc = "";
			switch((Resource.Type) value)
			{
				case Resource.Type.Wood:
					ImgSrc = "C:\\Users\\Miguel\\Desktop\\Caverna\\Wood.png";
					break;
				case Resource.Type.Stone:
					ImgSrc = "C:\\Users\\Miguel\\Desktop\\Caverna\\Stone.png";
					break;
				case Resource.Type.Ore:
					ImgSrc = "C:\\Users\\Miguel\\Desktop\\Caverna\\Ore.png";
					break;
				case Resource.Type.Ruby:
					ImgSrc = "C:\\Users\\Miguel\\Desktop\\Caverna\\Ruby.png";
					break;
				case Resource.Type.Gold:
					ImgSrc = "C:\\Users\\Miguel\\Desktop\\Caverna\\Gold.png";
					break;
				case Resource.Type.Food:
					ImgSrc = "C:\\Users\\Miguel\\Desktop\\Caverna\\Food.png";
					break;
				case Resource.Type.Grain:
					ImgSrc = "C:\\Users\\Miguel\\Desktop\\Caverna\\Grain.png";
					break;
				case Resource.Type.Vegetable:
					ImgSrc = "C:\\Users\\Miguel\\Desktop\\Caverna\\Vegetable.png";
					break;
				case Resource.Type.Sheep:
					ImgSrc = "C:\\Users\\Miguel\\Desktop\\Caverna\\Sheep.png";
					break;
				case Resource.Type.Donkey:
					ImgSrc = "C:\\Users\\Miguel\\Desktop\\Caverna\\Donkey.png";
					break;					
			}
			return ImgSrc;
		}
		
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
