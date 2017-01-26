/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 1/9/2017
 * Time: 6:15 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using CavernaWPF.Layables;

namespace CavernaWPF
{
	/// <summary>
	/// Description of ActionCardWrapper.
	/// </summary>
	public class ActionCardWrapper : INotifyPropertyChanged, INotifyCollectionChanged
	{
		public ActionCard actionCard
		{
			get;
			set; 
		}
		
		public bool occupied;
		
		public int Row{ get; set;}
		
		public int Column{ get; set;}
		
		private bool hidden;
		
		public bool Hidden
		{ 
			get { return hidden; }
			set
			{
				hidden = value;
				if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Hidden"));
			}
		}
		
		private string img;
		
		public string Img{ 
			get { return img; }
			set
			{
				img = value;
				if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Img"));
			}
		}
		
		private ObservableCollection<Layable> stuff = new ObservableCollection<Layable>();
		
		public ObservableCollection<Layable> Stuff
		{
			get { return stuff; }
			set { 
				stuff = value;
				if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Stuff"));
			}
		}
		
		public void Accumulate()
		{
			foreach(ResourceAccumulator ra in actionCard.Accumulators)
			{
				int amountAdded = 0;
				if(stuff.Count == 0)
				{
					amountAdded = ra.StartingAmount;
				}
				else
				{
					amountAdded = ra.Accumulation;
				}
				Random r = new Random();
				for(int i = 0; i < amountAdded; i++)
				{
					double rX = r.NextDouble() * (ra.rightXLimit - ra.leftXLimit - 35);
					rX += ra.leftXLimit;
					double rY = r.NextDouble() * (ra.bottomYLimit - ra.topYLimit - 35);
					rY += ra.topYLimit;
					Stuff.Add(new ResourceItem(ra.ResourceType) {X = rX, Y = rY});
				}
			}
		}
		
		public ActionCardWrapper(ActionCard card)
		{
			actionCard = card;
			setImage();
		}
		
		public void setImage()
		{
			Img = String.Format("C:\\Users\\Miguel\\Desktop\\Caverna\\ActionCards\\{0}.png", actionCard.Name);
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
