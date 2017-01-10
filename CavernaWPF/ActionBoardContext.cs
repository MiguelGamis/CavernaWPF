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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using CavernaWPF.Resources;
using CavernaWPF.Layables;
using System.Windows;

namespace CavernaWPF
{
	/// <summary>
	/// Description of ActionBoard.
	/// </summary>
	public sealed class ActionBoardContext : INotifyPropertyChanged, INotifyCollectionChanged
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
		
		public FurnishingWindow furnishingWindow;
		
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
			set { dwarfs = value; }
		}
		
		public void AddDwarf(Dwarf dwarf)
	    {
				this.dwarfs.Add(dwarf);
				this.OnNotifyCollectionChanged(
			        new NotifyCollectionChangedEventArgs(
			          NotifyCollectionChangedAction.Add, dwarf));
	    }
		
		public List<Player> players = new List<Player>();
		
		private ActionBoardContext()
		{
			actioncards = new ObservableCollection<ActionCard>();
			dwarfs = new ObservableCollection<Dwarf>();
			
			ActionCard ac1 = new ActionCard();
			ac1.Name = "Drift Mining";
			ac1.Accumulators.Add(new ResourceAccumulator(){ResourceType = Resource.Type.Stone, StartingAmount = 1, Accumulation = 1});
			ac1.PlayerAction = new Action<Dwarf>((d) =>
			                          {
			                          	ac1.DriftMining(d);
			                          });
			actioncards.Add(ac1);
			
			ActionCard ac2 = new ActionCard();
			ac2.Name = "Excavation";
			ac2.Accumulators = new List<ResourceAccumulator>() { new ResourceAccumulator(){ResourceType = Resource.Type.Stone, StartingAmount = 1, Accumulation = 1} };
			ac2.PlayerAction = new Action<Dwarf>((d) =>
			                          {
			                          	ac2.Excavation(d);
			                          });
			actioncards.Add(ac2);
			
			ActionCard ac3 = new ActionCard();
			ac3.Name = "Excavation";
			ac3.Accumulators = new List<ResourceAccumulator>() { new ResourceAccumulator(){ResourceType = Resource.Type.Wood, StartingAmount = 3, Accumulation = 1} };
			ac3.PlayerAction = new Action<Dwarf>((d) =>
			                          {
			                          	ac3.Logging(d);
			                          });
			actioncards.Add(ac3);
			
			ActionCard ac4 = new ActionCard();
			ac3.Name = "Ruby Mining";
			ac3.Accumulators = new List<ResourceAccumulator>() { new ResourceAccumulator(){ResourceType = Resource.Type.Wood, StartingAmount = 3, Accumulation = 1} };
			ac3.PlayerAction = new Action<Dwarf>((d) =>
			                          {
			                          	ac3.Logging(d);
			                          });
			actioncards.Add(ac4);
		}
		
		public void Intitialize()
		{
			ActionBoard ab = new ActionBoard();
			control = ab;
			FurnishingWindow fw = new FurnishingWindow();
			furnishingWindow = fw;
			ab.DataContext = this;
		}
		
		public void Replenish()
		{
			foreach(ActionCard actioncard in actioncards)
			{
				actioncard.Accumulate();
			}
		}
		
		public void PromptDwarf(Dwarf d)
		{
			Dwarfs.Add(d);
		}

		public void StartGame()
		{
			Replenish();
			
			PrepareActionCards();
			
			CurrentTurn = StartPlayerIndex;
			
			Dwarf dwarf = null;
			
//			LinkedList<Player> playersPlaying = new LinkedList<Player>();
			
			foreach(Player p in players)
			{
				playersPlaying.AddLast(new LinkedListNode<Player>(p));
			}
			
//			LinkedListNode<Player> pl = playersPlaying.First;
			
//			while(pl != null)
//			{
//				if(pl.Value.Dwarfs.Count > 0)
//				{
//					int last = pl.Value.Dwarfs.Count - 1;
//					AddDwarf(pl.Value.Dwarfs[last]);
//					pl.Value.Dwarfs.RemoveAt(last);
//					pl = pl.Next;
//					if(pl == null)
//						pl = playersPlaying.First;
//				}
//				else
//				{
//					LinkedListNode<Player> tmp = pl;
//					playersPlaying.Remove(pl);
//					pl = tmp.Next;
//					if(pl == null)
//						pl = playersPlaying.First;
//				}
//			}
			
			currentPlayer = playersPlaying.First;
		}
		
		LinkedList<Player> playersPlaying = new LinkedList<Player>();
		
		LinkedListNode<Player> currentPlayer;
		
		public bool readyForNextDwarf = true;
		
		public void NextTurn()
		{	
			if(readyForNextDwarf)
			{
				bool roundOver = false;
				
				Dwarf nextDwarf = null;
				
				currentPlayer = currentPlayer.Next;
				
				do
				{
					if(playersPlaying.Count == 0)
					{
						roundOver = true;
						return;
					}
					if(currentPlayer == null)
						currentPlayer = playersPlaying.First;
					else if(currentPlayer.Value.Dwarfs.Count > 0)
						nextDwarf = currentPlayer.Value.GetNextDwarf();
					else
					{
						LinkedListNode<Player> tmp = currentPlayer;
						playersPlaying.Remove(currentPlayer);
						currentPlayer = tmp.Next;
						if(currentPlayer == null)
							currentPlayer = playersPlaying.First;
					}
				}
				while(nextDwarf == null);
				
				if(roundOver)
				{
					
				}
				else
				{
					readyForNextDwarf = false;
					AddDwarf(nextDwarf);
				}
				
			}
		}
		
		private void PrepareActionCards()
		{
			
		}
		
		private int StartPlayerIndex = 0;
		
		private int CurrentTurn = 0;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
	    #region INotifyCollectionChanged
	    private void OnNotifyCollectionChanged(NotifyCollectionChangedEventArgs args)
	    {
			if (this.CollectionChanged != null)
			{
			    this.CollectionChanged(this, args);
			    Window window = new Window();
			    window.ShowDialog();
			}
	    }
	    public event NotifyCollectionChangedEventHandler CollectionChanged;
	    #endregion INotifyCollectionChanged
	}
}
