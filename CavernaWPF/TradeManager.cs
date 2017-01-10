/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 01/09/2017
 * Time: 00:15
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using CavernaWPF.Resources;
using System.Collections.Generic;
using System.ComponentModel;

namespace CavernaWPF
{
	/// <summary>
	/// Description of Trade.
	/// </summary>
	public class TradeManager : INotifyPropertyChanged
	{
		private Dictionary<List<ResourceTab>,List<List<ResourceTab>>> exchanges = new Dictionary<List<ResourceTab>,List<List<ResourceTab>>>()
		{
			{ new List<ResourceTab>() { new ResourceTab() {ResourceType = Resource.Type.Ruby, Amount = 1}}, new List<List<ResourceTab>>()
				{
					new List<ResourceTab>() { new ResourceTab() {ResourceType = Resource.Type.Wood, Amount = 1} },
					new List<ResourceTab>() { new ResourceTab() {ResourceType = Resource.Type.Stone, Amount = 1} },
					new List<ResourceTab>() { new ResourceTab() {ResourceType = Resource.Type.Ore, Amount = 1} },
					new List<ResourceTab>() { new ResourceTab() {ResourceType = Resource.Type.Food, Amount = 2} },
					new List<ResourceTab>() { new ResourceTab() {ResourceType = Resource.Type.Grain, Amount = 1} },
					new List<ResourceTab>() { new ResourceTab() {ResourceType = Resource.Type.Vegetable, Amount = 1} },
					new List<ResourceTab>() { new ResourceTab() {ResourceType = Resource.Type.Sheep, Amount = 1} },
					new List<ResourceTab>() { new ResourceTab() {ResourceType = Resource.Type.Donkey, Amount = 1} },
					new List<ResourceTab>() { new ResourceTab() {ResourceType = Resource.Type.Boar, Amount = 1} }
				}
			},
			{ new List<ResourceTab>() { new ResourceTab() {ResourceType = Resource.Type.Ruby, Amount = 1},
										new ResourceTab() {ResourceType = Resource.Type.Food, Amount = 1}}, 
				new List<List<ResourceTab>>()
				{
					new List<ResourceTab>() { new ResourceTab() {ResourceType = Resource.Type.Cow, Amount = 1} }
				}
			},
			{ new List<ResourceTab>() { new ResourceTab() {ResourceType = Resource.Type.Sheep, Amount = 1}}, 
				new List<List<ResourceTab>>()
				{
					new List<ResourceTab>() { new ResourceTab() {ResourceType = Resource.Type.Food, Amount = 1} }
				}
			},
			{ new List<ResourceTab>() { new ResourceTab() {ResourceType = Resource.Type.Donkey, Amount = 1}}, 
				new List<List<ResourceTab>>()
				{
					new List<ResourceTab>() { new ResourceTab() {ResourceType = Resource.Type.Food, Amount = 1} }
				}
			},
			{ new List<ResourceTab>() { new ResourceTab() {ResourceType = Resource.Type.Donkey, Amount = 1}}, 
				new List<List<ResourceTab>>()
				{
					new List<ResourceTab>() { new ResourceTab() {ResourceType = Resource.Type.Food, Amount = 1} }
				}
			},
			{ new List<ResourceTab>() { new ResourceTab() {ResourceType = Resource.Type.Donkey, Amount = 2}}, 
				new List<List<ResourceTab>>()
				{
					new List<ResourceTab>() { new ResourceTab() {ResourceType = Resource.Type.Food, Amount = 3} }
				}
			},
			{ new List<ResourceTab>() { new ResourceTab() {ResourceType = Resource.Type.Boar, Amount = 1}}, 
				new List<List<ResourceTab>>()
				{
					new List<ResourceTab>() { new ResourceTab() {ResourceType = Resource.Type.Food, Amount = 2} }
				}
			},
			{ new List<ResourceTab>() { new ResourceTab() {ResourceType = Resource.Type.Cow, Amount = 1}}, 
				new List<List<ResourceTab>>()
				{
					new List<ResourceTab>() { new ResourceTab() {ResourceType = Resource.Type.Food, Amount = 2} }
				}
			},
		};
		
//		Wood, Stone, Ore, Ruby, Food, Gold,
//						Grain, Vegetable,
//						Sheep, Donkey, Boar, Cow
		
		public Dictionary<List<ResourceTab>,List<List<ResourceTab>>> Exchanges {
			get {return exchanges;}
			set {
				exchanges = value;
				if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Exchanges"));
			}
		}
		
		public TradeWindow control;
		
		public TradeManager()
		{
			TradeWindow tw = new TradeWindow();
			control = tw;
			tw.DataContext = this;
		}
		
		public event PropertyChangedEventHandler PropertyChanged;
	}
}
