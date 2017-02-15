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
		
		public string Name
		{
			get;
			set;
		}
		
		public string Color
		{
			get;
			set;
		}
		
		private Dictionary<Resource.Type, ResourceTracker> resources = new Dictionary<Resource.Type, ResourceTracker>(){
			{ Resource.Type.Wood, new ResourceTracker(Resource.Type.Wood, 5) },
			{ Resource.Type.Stone, new ResourceTracker(Resource.Type.Stone, 5) },
			{ Resource.Type.Ore, new ResourceTracker(Resource.Type.Ore, 0) },
			{ Resource.Type.Ruby, new ResourceTracker(Resource.Type.Ruby, 3) },
			{ Resource.Type.Gold, new ResourceTracker(Resource.Type.Gold, 0) },
			{ Resource.Type.Food, new ResourceTracker(Resource.Type.Food, 0) },
			{ Resource.Type.Grain, new ResourceTracker(Resource.Type.Grain, 0) },
			{ Resource.Type.Vegetable, new ResourceTracker(Resource.Type.Vegetable, 0) },
			{ Resource.Type.Starvation, new	ResourceTracker(Resource.Type.Starvation, 0) }
		};
		
		public Dictionary<Resource.Type,  ResourceTracker> Resources{ 
			get { return resources; }
			set { resources = value; }
		}
		
		public ResourcesTab tab;
		
		public int maxDwarfs = 2;
		
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
