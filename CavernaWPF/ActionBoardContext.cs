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
		
		private ObservableCollection<ActionCardWrapper> actioncards;
		
		private ObservableCollection<Dwarf> dwarfs;
		
		public ActionBoard control;
		
		public FurnishingWindow furnishingWindow;
		
		public ObservableCollection<ActionCardWrapper> ActionCards
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
			actioncards = new ObservableCollection<ActionCardWrapper>();
			dwarfs = new ObservableCollection<Dwarf>();
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
			foreach(ActionCardWrapper actioncardwrapper in actioncards)
			{
				actioncardwrapper.actionCard.Accumulate();
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
			AddActionCard(GetActionCard("Drift mining"));
			
			AddActionCard(GetActionCard("Logging"));
			
			AddActionCard(GetActionCard("Wood gathering"));
			
			AddActionCard(GetActionCard("Excavation"));
			
			AddActionCard(GetActionCard("Supplies"));
			
			AddActionCard(GetActionCard("Clearing"));
			
			AddActionCard(GetActionCard("Starting player"));
			
			AddActionCard(GetActionCard("Ore mining"));
			
			AddActionCard(GetActionCard("Sustenance"));
			
			AddActionCard(GetActionCard("Ruby mining"));
			
			AddActionCard(GetActionCard("Housework"));
			
			AddActionCard(GetActionCard("Slash-and-burn"));
		}
		
		private ActionCard GetActionCard(string Name)
		{
			ActionCard ac = new ActionCard();
			switch(Name)
			{
				case "Drift mining": 
					ac.Name = "Drift mining";
					ac.Accumulators.Add(new ResourceAccumulator(){ResourceType = Resource.Type.Stone, StartingAmount = 1, Accumulation = 1});
					ac.PlayerAction = new Action<Dwarf>((d) =>
                          {
                          	ac.Driftmining(d);
                          });
					break;
				case "Logging":
					ac.Name = "Logging";
					ac.Accumulators.Add(new ResourceAccumulator(){ResourceType = Resource.Type.Wood, StartingAmount = 3, Accumulation = 1});
					ac.PlayerAction = new Action<Dwarf>((d) =>
					                          {
					                          	ac.Logging(d);
					                          });
					break;
				case "Wood gathering":
					ac.Name = "Wood gathering";
					ac.Accumulators.Add(new ResourceAccumulator(){ResourceType = Resource.Type.Wood, StartingAmount = 1, Accumulation = 1});
					ac.PlayerAction = new Action<Dwarf>((d) =>
			                          {
			                          	ac.Woodgathering(d);
			                          });
					break;
				case "Excavation":
					ac.Name = "Excavation";
					ac.Accumulators.Add(new ResourceAccumulator(){ResourceType = Resource.Type.Stone, StartingAmount = 1, Accumulation = 1});
					ac.PlayerAction = new Action<Dwarf>((d) =>
					                          {
					                          	ac.Excavation(d);
					                          });
					break;
				case "Supplies":
					ac.Name = "Supplies";
					ac.PlayerAction = new Action<Dwarf>((d) =>
			                          {
			                          	ac.Supplies(d);
			                          });
					break;
				case "Clearing":
					ac.Name = "Clearing";
					ac.Accumulators.Add(new ResourceAccumulator(){ResourceType = Resource.Type.Wood, StartingAmount = 1, Accumulation = 1});
					ac.PlayerAction = new Action<Dwarf>((d) =>
			                          {
			                          	ac.Clearing(d);
			                          });
					break;
				case "Starting player":
					ac.Name = "Starting player";
					ac.Accumulators.Add(new ResourceAccumulator(){ResourceType = Resource.Type.Food, StartingAmount = 1, Accumulation = 1});
					ac.PlayerAction = new Action<Dwarf>((d) =>
			                          {
			                          	ac.Startingplayer(d);
			                          });
					break;	
				case "Ore mining":
					ac.Name = "Ore mining";
					ac.Accumulators.Add(new ResourceAccumulator(){ResourceType = Resource.Type.Ore, StartingAmount = 2, Accumulation = 1});
					ac.PlayerAction = new Action<Dwarf>((d) =>
			                          {
			                          	ac.Oremining(d);
			                          });
					break;
				case "Sustenance":
					ac.Name = "Sustenance";
					ac.Accumulators.Add(new ResourceAccumulator(){ResourceType = Resource.Type.Food, StartingAmount = 1, Accumulation = 1});
					ac.PlayerAction = new Action<Dwarf>((d) =>
			                          {
			                          	ac.Woodgathering(d);
			                          });
					break;
				case "Ruby mining":
					ac.Name = "Ruby mining";
					ac.Accumulators.Add(new ResourceAccumulator(){ResourceType = Resource.Type.Ruby, StartingAmount = 1, Accumulation = 1});
					ac.PlayerAction = new Action<Dwarf>((d) =>
			                          {
			                          	ac.Rubymining(d);
			                          });
					break;
				case "Housework":
					ac.Name = "Housework";
					ac.PlayerAction = new Action<Dwarf>((d) =>
			                          {
			                          	ac.Housework(d);
			                          });
					break;
				case "Slash-and-burn":
					ac.Name = "Slash-and-burn";
					ac.PlayerAction = new Action<Dwarf>((d) =>
			                          {
			                          	ac.Slashandburn(d);
			                          });
					break;
				default:
						return null;
						break;
			}
			return ac;
		}
		
		private void AddActionCard(ActionCard ac)
		{
			if(ac == null)
				return;
			ActionCardWrapper acw = new ActionCardWrapper(ac) { Column=actioncards.Count/3, Row=actioncards.Count%3 };
			actioncards.Add(acw);
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
