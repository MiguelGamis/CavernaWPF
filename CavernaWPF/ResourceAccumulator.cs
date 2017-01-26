/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 12/31/2016
 * Time: 03:19
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using CavernaWPF.Resources;
using System.ComponentModel;

namespace CavernaWPF
{
	/// <summary>
	/// Description of ResourceAccumulator.
	/// </summary>
	public class ResourceAccumulator : INotifyPropertyChanged
	{
		public Resource.Type ResourceType;
		public int StartingAmount;
		public int Accumulation;
		private int amount;
		public int Amount
		{
			get	{ return amount; }
			set { 
					amount = value;
					if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Amount"));
				}
		}
		public double topYLimit = 0;
		public double bottomYLimit = 137;
		public double leftXLimit = 0;
		public double rightXLimit = 101;
		
		public event PropertyChangedEventHandler PropertyChanged;
	}
}
