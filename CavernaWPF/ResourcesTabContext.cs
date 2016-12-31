/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 12/30/2016
 * Time: 4:50 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using CavernaWPF.Resources;

namespace CavernaWPF
{
	/// <summary>
	/// Description of ResourcesTabContext.
	/// </summary>
	public class ResourcesTabContext : INotifyCollectionChanged
	{
		public Dictionary<Resource.Type, int> Resources;
		
		public ResourcesTabContext()
		{
			Resources = new Dictionary<Resource.Type, int>(){ 
				{Resource.Type.Food, 0}, 
				{Resource.Type.Gold, 0},
				{Resource.Type.Wood, 0},
				{Resource.Type.Stone, 0},
				{Resource.Type.Ore, 0},
				{Resource.Type.Ruby, 0}
			};
		}
		
		public void AddResource(Resource.Type resource, int amount)
		{
			this.Resources[resource] += amount;
		    this.OnNotifyCollectionChanged(
		        new NotifyCollectionChangedEventArgs(
		          NotifyCollectionChangedAction.Remove, resource));
		}
		
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
