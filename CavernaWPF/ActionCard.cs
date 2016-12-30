/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 12/23/2016
 * Time: 12:38
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace CavernaWPF
{
	/// <summary>
	/// Description of ActionCard.
	/// </summary>
	public class ActionCard
	{
		private string name;
		private bool accumulating;
		public List<Action> actions;
		
		public ActionCard()
		{
			actions = new List<Action>();
		}
		
		public string Name
		{
			get { return name; }
			set { name = value; }
		}
		
		public bool Accumulating
		{
			get;
			set;
		}
		
		public bool IsAccumulating()
		{
			return accumulating;
		}
		
		public bool IsMultiAction()
		{
			return actions.Count > 1;
		}
	}
}
