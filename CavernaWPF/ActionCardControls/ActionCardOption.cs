/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 01/16/2017
 * Time: 14:42
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Windows.Controls;

namespace CavernaWPF.ActionCardControls
{
	/// <summary>
	/// Description of ActionCardOption.
	/// </summary>
	public abstract class ActionCardOption : INotifyPropertyChanged
	{
		public string Text { get; set; }
		private bool selected;
		public bool Selected
		{
			get {return selected;}
			set 
			{
				selected = value;
				if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Selected"));
			}
		}
		private int value_;
		public int Value
		{
			get {return value_;}
			set 
			{
				value_ = value;
				if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Value"));
			}
		}
		
		public UserControl Control { get; set; }
		public bool Able { get; set; }
		
		public event PropertyChangedEventHandler PropertyChanged;
	}
}
