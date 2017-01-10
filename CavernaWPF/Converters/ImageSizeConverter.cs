/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 1/7/2017
 * Time: 3:03 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Globalization;
using System.Windows.Data;
using CavernaWPF.Layables;

namespace CavernaWPF.Converters
{
	/// <summary>
	/// Description of ImageSizeConverter.
	/// </summary>
	public class ImageSizeConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return ((double) value)/2;
		}
		
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
