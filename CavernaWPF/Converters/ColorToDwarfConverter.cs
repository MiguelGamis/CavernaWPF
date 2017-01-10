/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 1/3/2017
 * Time: 4:48 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Globalization;
using System.Windows.Data;

namespace CavernaWPF.Converters
{
	/// <summary>
	/// Description of ColorToDwarfConverter.
	/// </summary>
	public class ColorToDwarfConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			switch((string) value)
			{
				case "Blue": return "C:\\Users\\Miguel\\Desktop\\Caverna\\BlueDwarf.png";
				case "Yellow": return "C:\\Users\\Miguel\\Desktop\\Caverna\\YellowDwarf.png";
				case "Green": return "C:\\Users\\Miguel\\Desktop\\Caverna\\GreenDwarf.png";
				case "Purple": return "C:\\Users\\Miguel\\Desktop\\Caverna\\PurpleDwarf.png";
				default: return "C:\\Users\\Miguel\\Desktop\\Caverna\\Dwarf.png";
			}
		}
		
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
