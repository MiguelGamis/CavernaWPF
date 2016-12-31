/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 12/28/2016
 * Time: 03:41
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using CavernaWPF.Resources;
using CavernaWPF.Layable;

namespace CavernaWPF
{
	/// <summary>
	/// Description of ActionBoard.
	/// </summary>
	public sealed class ActionBoardContext : INotifyPropertyChanged
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
			set { actioncards = value; 
				if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ActionCards"));
			}
		}
		
		public ObservableCollection<Dwarf> Dwarfs
		{
			get { return dwarfs; }
			set { dwarfs = value; 
				if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Dwarfs"));
			}
		}
		
		private ActionBoardContext()
		{
			actioncards = new ObservableCollection<ActionCard>();
			dwarfs = new ObservableCollection<Dwarf>();
			
			ActionCard ac = new ActionCard();
			ac.Name = "Drift Mining";
			ac.Accumulating = true;
			ac.actions.Add(new Action<Player>((p) =>
			                          {
			                                  	p.town.Resources[1].Amount++;
			                                  	Tile cavetunnel = new Tile();
			                                  	p.town.Tiles.Add(cavetunnel);
			                          }));
			actioncards.Add(ac);
		}
		
		public void Intitialize()
		{
			ActionBoard ab = new ActionBoard();
			control = ab;
			ab.DataContext = this;
		}
		
		public void Replenish()
		{
			
		}
		
		public void PromptDwarf(Dwarf d)
		{
			Dwarfs.Add(d);
		}
		
		public event PropertyChangedEventHandler PropertyChanged;
	}
}
