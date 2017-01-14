/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 12/30/2016
 * Time: 02:47
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using CavernaWPF.Resources;

namespace CavernaWPF
{
	/// <summary>
	/// Description of Player.
	/// </summary>
	public class Player : INotifyPropertyChanged, INotifyCollectionChanged
	{
		public Player()
		{
			tab = new ResourcesTab();
			tab.DataContext = this;
		}
		
		public TownContext town;
		
		private ObservableCollection<Dwarf> dwarfs = new ObservableCollection<Dwarf>();
		
		public ObservableCollection<Dwarf> Dwarfs
		{
			get { return dwarfs; }
			set { 
				dwarfs = value;
				if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Dwarfs"));
			}
		}
		
		public Dwarf GetNextDwarf()
	    {
			if(dwarfs.Count > 0)
			{
				int last = dwarfs.Count - 1;
				Dwarf nextDwarf = dwarfs[last];
				dwarfs.RemoveAt(last);
				this.OnNotifyCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, nextDwarf));
				return nextDwarf;
			}
			return null;
	    }
		
		public string name;
		
		public string Color
		{
			get;
			set;
		}
		
		private Dictionary<Resource.Type, ResourceTab> resources = new Dictionary<Resource.Type, ResourceTab>(){
			{ Resource.Type.Wood, new ResourceTab(){ ResourceType = Resource.Type.Wood, Amount = 0 } },
			{ Resource.Type.Stone, new ResourceTab(){ ResourceType = Resource.Type.Stone, Amount = 0 } },
			{ Resource.Type.Ore, new ResourceTab(){ ResourceType = Resource.Type.Ore, Amount = 0 } },
			{ Resource.Type.Ruby, new ResourceTab(){ ResourceType = Resource.Type.Ruby, Amount = 0 } },
			{ Resource.Type.Gold, new ResourceTab(){ ResourceType = Resource.Type.Gold, Amount = 0 } },
			{ Resource.Type.Food, new ResourceTab(){ ResourceType = Resource.Type.Food, Amount = 0 } },
			{ Resource.Type.Grain, new ResourceTab(){ ResourceType = Resource.Type.Grain, Amount = 0 } },
			{ Resource.Type.Vegetable, new ResourceTab(){ ResourceType = Resource.Type.Vegetable, Amount = 0 } },
			{ Resource.Type.Sheep, new ResourceTab(){ ResourceType = Resource.Type.Sheep, Amount = 0 } },
			{ Resource.Type.Donkey, new ResourceTab(){ ResourceType = Resource.Type.Donkey, Amount = 0 } },
			{ Resource.Type.Boar, new ResourceTab(){ ResourceType = Resource.Type.Boar, Amount = 0 } },
			{ Resource.Type.Cow, new ResourceTab(){ ResourceType =Resource.Type.Cow, Amount = 0 } }
		};
		
		public Dictionary<Resource.Type, ResourceTab> Resources{ 
			get { return resources; }
			set { resources = value; }
		}
		
		public ResourcesTab tab;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		#region INotifyCollectionChanged
	    private void OnNotifyCollectionChanged(NotifyCollectionChangedEventArgs args)
	    {
			if (this.CollectionChanged != null)
			{
			    this.CollectionChanged(this, args);
			}
	    }
	    public event NotifyCollectionChangedEventHandler CollectionChanged;
	    #endregion INotifyCollectionChanged
	}
}
