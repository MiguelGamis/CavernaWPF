/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 01/23/2017
 * Time: 02:12
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
	/// Description of PhaseToGameStatusConverter.
	/// </summary>
	public class PhaseToGameStatusConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			switch((ActionBoardContext.Phase) value)
			{
				case ActionBoardContext.Phase.ActionPhase: return "Action Phase";
				case ActionBoardContext.Phase.BreedingPhase: return "Breeding Phase";
				case ActionBoardContext.Phase.FeedingPhase: return "Feeding Phase";
				case ActionBoardContext.Phase.FieldPhase: return "Field Phase";
				case ActionBoardContext.Phase.NoHarvest: return "No Harvest";
				case ActionBoardContext.Phase.Pay1FoodPerDwarf: return "Pay 1 Food";
				case ActionBoardContext.Phase.SkipFieldOrBreed: return "Skip Field or Breeding Phase";;
			}
			return "";
		}
		
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
