/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 1/12/2017
 * Time: 3:40 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Globalization;
using System.Windows.Data;

namespace CavernaWPF.Converters
{
	/// <summary>
	/// Description of LevelToWeaponConverter.
	/// </summary>
	public class LevelToWeaponConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return String.Format("C:\\Users\\Miguel\\Desktop\\Caverna\\Weapons\\Weapon{0}.png", value);
		}
		
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
