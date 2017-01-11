/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 1/10/2017
 * Time: 4:51 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using CavernaWPF.Resources;

namespace CavernaWPF
{
	/// <summary>
	/// Description of TradeOptions.
	/// </summary>
	public class ResourceBatch
	{
		private List<ResourceTab> batch = new List<ResourceTab>(); 
		
		public List<ResourceTab> Batch
		{
			get { return batch; }
			set { batch = value; }
		}
	}
}
