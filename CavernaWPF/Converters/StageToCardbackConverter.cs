/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 1/14/2017
 * Time: 12:19 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Globalization;
using System.Windows.Data;

namespace CavernaWPF.Converters
{
	/// <summary>
	/// Description of StageToCardbackConverter.
	/// </summary>
	public class StageToCardbackConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return String.Format("pack://application:,,,/Images/ActionCards/Stages/Stage{0}.png", (int) value);
		}
		
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
