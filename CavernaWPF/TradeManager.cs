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
		public Player player;
		
		private Dictionary<List<ResourceTab>,List<ResourceTab>> exchanges = new Dictionary<List<ResourceTab>,List<ResourceTab>>()
		{
			{ new List<ResourceTab>() { new ResourceTab(Resource.Type.Ruby, 1) },
				new List<ResourceTab>() { new ResourceTab(Resource.Type.Wood, 1) }
			},
			{ new List<ResourceTab>() { new ResourceTab(Resource.Type.Ruby, 1) },
				new List<ResourceTab>() { new ResourceTab(Resource.Type.Stone, 1) }
			},
			{ new List<ResourceTab>() { new ResourceTab(Resource.Type.Ruby, 1) },
				new List<ResourceTab>() { new ResourceTab(Resource.Type.Ore, 1) }
			},
			{ new List<ResourceTab>() { new ResourceTab(Resource.Type.Ruby, 1) },
				new List<ResourceTab>() { new ResourceTab(Resource.Type.Food, 2) }
			},
			{ new List<ResourceTab>() { new ResourceTab(Resource.Type.Ruby, 1) },
				new List<ResourceTab>() { new ResourceTab(Resource.Type.Sheep, 1) }
			},
			{ new List<ResourceTab>() { new ResourceTab(Resource.Type.Ruby, 1) }, 
				new List<ResourceTab>() { new ResourceTab(Resource.Type.Donkey, 1) }
			},
			{ new List<ResourceTab>() { new ResourceTab(Resource.Type.Ruby, 1) }, 
				new List<ResourceTab>() { new ResourceTab(Resource.Type.Boar, 1) }
			},
			{ new List<ResourceTab>() { new ResourceTab(Resource.Type.Ruby, 1),
									new ResourceTab(Resource.Type.Food, 1)},
				new List<ResourceTab>() { new ResourceTab( Resource.Type.Cow, 1) }
			},
			{ new List<ResourceTab>() { new ResourceTab(Resource.Type.Sheep, 1) }, 
				new List<ResourceTab>() { new ResourceTab(Resource.Type.Food, 1) }
			},
			{ new List<ResourceTab>() { new ResourceTab(Resource.Type.Donkey, 1) }, 
				new List<ResourceTab>() { new ResourceTab(Resource.Type.Food, 1) }
			},
			{ new List<ResourceTab>() { new ResourceTab(Resource.Type.Donkey, 1) }, 
				new List<ResourceTab>() { new ResourceTab(Resource.Type.Food, 1) }
			},
			{ new List<ResourceTab>() { new ResourceTab(Resource.Type.Donkey, 2) }, 
				new List<ResourceTab>() { new ResourceTab(Resource.Type.Food, 3) }
			},
			{ new List<ResourceTab>() { new ResourceTab(Resource.Type.Boar, 1) }, 
				new List<ResourceTab>() { new ResourceTab(Resource.Type.Food, 2) }
			},
			{ new List<ResourceTab>() { new ResourceTab(Resource.Type.Cow, 1) }, 
				new List<ResourceTab>() { new ResourceTab(Resource.Type.Food, 2) }
			},
		};
		
//		Wood, Stone, Ore, Ruby, Food, Gold,
//						Grain, Vegetable,
//						Sheep, Donkey, Boar, Cow
		
		public Dictionary<List<ResourceTab>,List<ResourceTab>> Exchanges {
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
		
		public void PlayerTrading(Player p)
		{
			player = p;
			control.ShowDialog();
		}
		
		public event PropertyChangedEventHandler PropertyChanged;
	}
}
