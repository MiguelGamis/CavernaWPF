/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 01/23/2017
 * Time: 00:00
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using CavernaWPF.Resources;

namespace CavernaWPF
{
	/// <summary>
	/// Description of ResourceTracker.
	/// </summary>
	public class ResourceTracker : ResourceTab
	{
		public ResourceTracker(Resource.Type type, int amount) : base(type, amount)
		{
		}
		
		public delegate void ResourceIncrementedEventHandler(object sender, EventArgs args);
		
		public event ResourceIncrementedEventHandler ResourceIncremented;
		
		public void Increment(int amount)
		{
			Amount += amount;
			OnResourceIncremented();
		}
		
		protected virtual void OnResourceIncremented()
		{
			if (ResourceIncremented != null)
				ResourceIncremented(this, EventArgs.Empty);
		}
	}
}
