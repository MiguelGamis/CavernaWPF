/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 1/13/2017
 * Time: 3:42 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Globalization;
using System.Windows.Data;

namespace CavernaWPF.Converters
{
	/// <summary>
	/// Description of HarvestMarkerConverter.
	/// </summary>
	public class HarvestMarkerConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			switch((HarvestMarker.Type) value)
			{
				case HarvestMarker.Type.Pay1FoodPerDwarf:
					return "C:\\Users\\Miguel\\Desktop\\Caverna\\HarvestMarkers\\Pay1FoodPerDwarf.png";
				case HarvestMarker.Type.Harvest:
					return "C:\\Users\\Miguel\\Desktop\\Caverna\\HarvestMarkers\\GreenLeaf.png";
				case HarvestMarker.Type.QuestionMark:
					return "C:\\Users\\Miguel\\Desktop\\Caverna\\HarvestMarkers\\RedQuestion.png";
				case HarvestMarker.Type.NoHarvest:
					return "C:\\Users\\Miguel\\Desktop\\Caverna\\HarvestMarkers\\NoHarvest.png";
			}
			return "";
		}
		
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
