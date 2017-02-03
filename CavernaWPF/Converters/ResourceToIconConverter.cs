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
					ImgSrc = "pack://application:,,,/Images/Resources/Wood.png";
					break;
				case Resource.Type.Stone:
					ImgSrc = "pack://application:,,,/Images/Resources/Stone.png";
					break;
				case Resource.Type.Ore:
					ImgSrc = "pack://application:,,,/Images/Resources/Ore.png";
					break;
				case Resource.Type.Ruby:
					ImgSrc = "pack://application:,,,/Images/Resources/Ruby.png";
					break;
				case Resource.Type.Gold:
					ImgSrc = "pack://application:,,,/Images/Resources/Gold.png";
					break;
				case Resource.Type.Food:
					ImgSrc = "pack://application:,,,/Images/Resources/Food.png";
					break;
				case Resource.Type.Grain:
					ImgSrc = "pack://application:,,,/Images/Resources/Grain.png";
					break;
				case Resource.Type.Vegetable:
					ImgSrc = "pack://application:,,,/Images/Resources/Vegetable.png";
					break;
				case Resource.Type.Sheep:
					ImgSrc = "pack://application:,,,/Images/Resources/Sheep.png";
					break;
				case Resource.Type.Donkey:
					ImgSrc = "pack://application:,,,/Images/Resources/Donkey.png";
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
