/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 12/30/2016
 * Time: 1:13 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using CavernaWPF.Resources;

namespace CavernaWPF
{
	/// <summary>
	/// Description of ResourceTab.
	/// </summary>
	public class ResourceTab : INotifyPropertyChanged
	{
		public ResourceTab(Resource.Type type, int amount)
		{
			ResourceType = type;
			Amount = amount;			
		}
		
		public Resource.Type ResourceType {
			get; 
			set;
		}
		
		private int amount;
		
		public int Amount {
			get { return amount; }
			set{
				amount = value;
				if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Amount"));
			}
		}
		
		public event PropertyChangedEventHandler PropertyChanged;
	}
}
