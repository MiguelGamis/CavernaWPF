/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 12/28/2016
 * Time: 03:41
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.ObjectModel;

namespace CavernaWPF
{
	/// <summary>
	/// Description of ActionBoard.
	/// </summary>
	public sealed class ActionBoardContext
	{
		private static ActionBoardContext instance = new ActionBoardContext();
		
		public static ActionBoardContext Instance {
			get {
				return instance;
			}
		}
		
		private ObservableCollection<ActionCard> actioncards;
		
		private ObservableCollection<Dwarf> dwarfs;
		
		public ActionBoard control;
		
		public ObservableCollection<ActionCard> ActionCards
		{
			get { return actioncards; }
			set { actioncards = value; }
		}
		
		public ObservableCollection<Dwarf> Dwarfs
		{
			get { return dwarfs; }
			set { dwarfs = value; }
		}
		
		private ActionBoardContext()
		{
			actioncards = new ObservableCollection<ActionCard>();
			dwarfs = new ObservableCollection<Dwarf>();
			
			ActionCard ac = new ActionCard();
			ac.Name = "Drift Mining";
			ac.Accumulating = true;
			ac.actions.Add(new Action(() => { 
			                          	Console.WriteLine("Hey"); 
			                          }));
			
			Dwarf d = new Dwarf();
			dwarfs.Add(d);
		}
		
		public void Intitialize()
		{
			ActionBoard ab = new ActionBoard();
			control = ab;
			ab.DataContext = this;
		}
	}
}
