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
using CavernaWPF.Layables;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

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
			{ new List<ResourceTab>() { new ResourceTab(Resource.Type.Ruby, 1) }, 
				new List<ResourceTab>() { new ResourceTab(Resource.Type.Meadow, 1) }
			},
			{ new List<ResourceTab>() { new ResourceTab(Resource.Type.Ruby, 1) }, 
				new List<ResourceTab>() { new ResourceTab(Resource.Type.Field, 1) }
			},
			{ new List<ResourceTab>() { new ResourceTab(Resource.Type.Ruby, 1) }, 
				new List<ResourceTab>() { new ResourceTab(Resource.Type.Tunnel, 1) }
			},
			{ new List<ResourceTab>() { new ResourceTab(Resource.Type.Ruby, 2) }, 
				new List<ResourceTab>() { new ResourceTab(Resource.Type.Cave, 1) }
			},
			{ new List<ResourceTab>() { new ResourceTab(Resource.Type.Sheep, 1) }, 
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
		
		public Dictionary<List<ResourceTab>,List<ResourceTab>> Exchanges {
			get {return exchanges;}
			set {
				exchanges = value;
				if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Exchanges"));
			}
		}
		
		private Dictionary<List<object>,List<object>> _exchanges = new Dictionary<List<object>,List<object>>()
		{
			{ new List<object>() { new ResourceTab(Resource.Type.Ruby, 1) },
				new List<object>{ new ResourceTab(Resource.Type.Wood, 1) }
			},
			{ new List<object>() { new ResourceTab(Resource.Type.Ruby, 1) },
				new List<object>() { new ResourceTab(Resource.Type.Stone, 1) }
			},
			{ new List<object>() { new ResourceTab(Resource.Type.Ruby, 1) },
				new List<object>() { new ResourceTab(Resource.Type.Ore, 1) }
			},
			{ new List<object>() { new ResourceTab(Resource.Type.Ruby, 1) },
				new List<object>() { new ResourceTab(Resource.Type.Food, 2) }
			},
			{ new List<object>() { new ResourceTab(Resource.Type.Ruby, 1) },
				new List<object>() { new FarmAnimal(FarmAnimal.Type.Sheep) }
			},
			{ new List<object>() { new ResourceTab(Resource.Type.Ruby, 1) }, 
				new List<object>() { new FarmAnimal(FarmAnimal.Type.Donkey) }
			},
			{ new List<object>() { new ResourceTab(Resource.Type.Ruby, 1) }, 
				new List<object>() { new FarmAnimal(FarmAnimal.Type.Boar) }
			},
			{ new List<object>() { new ResourceTab(Resource.Type.Ruby, 1),
									new ResourceTab(Resource.Type.Food, 1)},
				new List<object>() { new FarmAnimal(FarmAnimal.Type.Cow) }
			},
			{ new List<object>() { new ResourceTab(Resource.Type.Ruby, 1) }, 
				new List<object>() { new Tile(Tile.Type.Meadow) }
			},
			{ new List<object>() { new ResourceTab(Resource.Type.Ruby, 1) }, 
				new List<object>() { new Tile(Tile.Type.Field) }
			},
			{ new List<object>() { new ResourceTab(Resource.Type.Ruby, 1) }, 
				new List<object>() { new Tile(Tile.Type.Tunnel) }
			},
			{ new List<object>() { new ResourceTab(Resource.Type.Ruby, 2) }, 
				new List<object>() { new Tile(Tile.Type.Cave) }
			},
			{ new List<object>() { new ResourceTab(Resource.Type.Sheep, 1) }, 
				new List<object>() { new ResourceTab(Resource.Type.Food, 1) }
			},
			{ new List<object>() { new ResourceTab(Resource.Type.Donkey, 1) }, 
				new List<object>() { new ResourceTab(Resource.Type.Food, 1) }
			},
			{ new List<object>() { new ResourceTab(Resource.Type.Donkey, 2) }, 
				new List<object>() { new ResourceTab(Resource.Type.Food, 3) }
			},
			{ new List<object>() { new ResourceTab(Resource.Type.Boar, 1) }, 
				new List<object>() { new ResourceTab(Resource.Type.Food, 2) }
			},
			{ new List<object>() { new ResourceTab(Resource.Type.Cow, 1) }, 
				new List<object>() { new ResourceTab(Resource.Type.Food, 2) }
			},
		};
		
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
		
		public void Trade(List<ResourceTab> input, List<ResourceTab> output)
		{
			if(CanAfford(input))
			{
				foreach(ResourceTab inputcomponent in input)
				{
					if(player.Resources.ContainsKey(inputcomponent.type))
					{
						player.Resources[inputcomponent.type].Amount-=inputcomponent.Amount;
					}
					else if(farmAnimalTypes.ContainsKey(inputcomponent.type))
					{
						List<FarmAnimal> fas = player.town.Tiles.OfType<FarmAnimal>().Where(fa => fa.type == farmAnimalTypes[inputcomponent.type]).ToList();
						int amount = inputcomponent.Amount;
						foreach(FarmAnimal fa in fas)
						{
							if(amount == 0) break;
							player.town.Tiles.Remove(fa);
							amount--;
						}
					}
				}
				
				foreach(ResourceTab outputcomponent in output)
				{
					if(player.Resources.ContainsKey(outputcomponent.type))
					{
						player.Resources[outputcomponent.type].Amount+=outputcomponent.Amount;
					}
					else if(farmAnimalTypes.ContainsKey(outputcomponent.type))
					{
						for(int i = 0; i < outputcomponent.Amount; i++)
						{
							player.town.Tiles.Add(new FarmAnimal(farmAnimalTypes[outputcomponent.type]));
						}
					}
					else if(tileTypes.ContainsKey(outputcomponent.type))
					{
						for(int i = 0; i < outputcomponent.Amount; i++)
						{
							player.town.Tiles.Add(new Tile(tileTypes[outputcomponent.type]));
						}
					}
				}
			}
		}
		
		private Dictionary<Resource.Type, FarmAnimal.Type> farmAnimalTypes = new Dictionary<Resource.Type, FarmAnimal.Type>()
		{
			{Resource.Type.Sheep, FarmAnimal.Type.Sheep},
			{Resource.Type.Donkey, FarmAnimal.Type.Donkey}, 
			{Resource.Type.Boar, FarmAnimal.Type.Boar}, 
			{Resource.Type.Cow, FarmAnimal.Type.Cow}
		};
		
		private Dictionary<Resource.Type, Tile.Type> tileTypes = new Dictionary<Resource.Type, Tile.Type>() 
		{
			{Resource.Type.Tunnel, Tile.Type.Tunnel}, 
			{Resource.Type.Cave, Tile.Type.Cave}, 
			{Resource.Type.Meadow, Tile.Type.Meadow}, 
			{Resource.Type.Field, Tile.Type.Field}
		};
		
		public bool CanAfford(List<ResourceTab> input)
		{	
			foreach(ResourceTab inputcomponent in input)
			{
				if(farmAnimalTypes.ContainsKey(inputcomponent.type))
				{
					List<FarmAnimal> fas = player.town.Tiles.OfType<FarmAnimal>().Where(fa => fa.type == farmAnimalTypes[inputcomponent.type]).ToList();
					if(fas.Count < inputcomponent.Amount)
					{
						return false;
					}
				}
				else if(tileTypes.ContainsKey(inputcomponent.type))
				{
					return false;
				}
				else
				{
					if(player.Resources[inputcomponent.type].Amount < inputcomponent.Amount)
					{
						return false;
					}
				}
			}
			return true;
		}
		
		public event PropertyChangedEventHandler PropertyChanged;
	}
}
