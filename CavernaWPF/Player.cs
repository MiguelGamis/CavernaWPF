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

namespace CavernaWPF
{
	/// <summary>
	/// Description of Player.
	/// </summary>
	public class Player : INotifyPropertyChanged, INotifyCollectionChanged
	{
		public Player()
		{
		}
		
		public TownContext town;
		
		private List<Dwarf> dwarfs = new List<Dwarf>();
		
		public List<Dwarf> Dwarfs
		{
			get { return dwarfs; }
			set { 
				dwarfs = value;
				if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Dwarfs"));
			}
		}
		
		public Dwarf GetNextDwarf()
	    {
				int last = dwarfs.Count - 1;
				Dwarf sentDwarf = dwarfs[last];
				dwarfs.RemoveAt(last);
//				this.OnNotifyCollectionChanged(
//			        new NotifyCollectionChangedEventArgs(
//			          NotifyCollectionChangedAction.Remove, sentDwarf));
				return sentDwarf;
	    }
		
		public string name;
		
		public string Color
		{
			get;
			set;
		}
		
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
