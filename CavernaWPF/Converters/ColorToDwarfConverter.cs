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
			return String.Format("pack://application:,,,/Images/Dwarfs/{0}Dwarf.png", (string) value);
		}
		
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
