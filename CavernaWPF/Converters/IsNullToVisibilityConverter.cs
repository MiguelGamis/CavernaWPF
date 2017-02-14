/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 2/13/2017
 * Time: 12:53 AM
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
	/// Description of IsNullToVisibilityConverter.
	/// </summary>
	public class IsNullToVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if((PlayerSlot) value == null)
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
