/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 1/24/2017
 * Time: 8:28 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using CavernaWPF.Layables;
using CavernaWPF.Resources;

namespace CavernaWPF.Converters
{
	/// <summary>
	/// Description of TradeExchangeConverter.
	/// </summary>
	public class TradeExchangeConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
//			if(value is FarmAnimal)
//			{
//				
//			}
			return "Hello";
		}
		
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
