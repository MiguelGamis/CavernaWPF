/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 01/17/2017
 * Time: 04:29
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CavernaWPF.Converters
{
	/// <summary>
	/// Description of BoolToVisibilityConverter.
	/// </summary>
	public class BoolToVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if((bool) value)
				return Visibility.Visible;
			else
				return Visibility.Collapsed;
		}
		
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
