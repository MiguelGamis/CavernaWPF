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
				readyForNextDwarf = false;
	    }
		
		public List<Player> players = new List<Player>();
		
		private ActionBoardContext()
		{
			actioncards = new ObservableCollection<ActionCardWrapper>();
			dwarfs = new ObservableCollection<Dwarf>();
			harvestmarkers = new ObservableCollection<HarvestMarker>();
			Intitialize();
		}
		
		private void Intitialize()
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
			PrepareActionCards();
		
			Replenish();	
			
			PrepareHarvestMarkers();
			
			CurrentTurn = StartPlayerIndex;
			
			foreach(Player p in players)
			{
				playersPlaying.AddLast(new LinkedListNode<Player>(p));
			}
		}
		
		LinkedList<Player> playersPlaying = new LinkedList<Player>();
		
		LinkedListNode<Player> currentPlayer;
		
		public bool readyForNextDwarf = true;
		
		public void NextTurn()
		{
			Harvest();
			NextRound();
			return;
			
			if(!readyForNextDwarf)
			{
				return;
			}
			
			if(currentPlayer == null)
				currentPlayer = playersPlaying.First;
			else
			{
				if(currentPlayer.Next == null)
				{
					currentPlayer = playersPlaying.First;
				}
				else
				{
					currentPlayer = currentPlayer.Next;
				}
			}
			
			Dwarf d = null;
			while(d == null)
			{
				if(currentPlayer == null)
				{
					//TODO: Encapsulate -----------------
					for(int i = ActionBoardContext.Instance.Dwarfs.Count - 1; i >= 0; i--)
					{
						Dwarf dw = ActionBoardContext.Instance.Dwarfs[i];
						dw.player.Dwarfs.Add(dw);
						ActionBoardContext.Instance.Dwarfs.RemoveAt(i);
					}
					Harvest();
					NextRound();
					return;
					//------------------------------------
				}
				
				Player p = currentPlayer.Value;
				d = p.GetNextDwarf();
				if(d == null)
				{
					LinkedListNode<Player> tmp = currentPlayer;
					playersPlaying.Remove(tmp);
					if(currentPlayer.Next == null)
					{
						currentPlayer = playersPlaying.First;
					}
					else
					{
						currentPlayer = currentPlayer.Next;
					}
				}
			}
			ActionBoardContext.Instance.AddDwarf(d);
		}
		
		int Round = 0;
		
		private void Harvest()
		{
		}
		
		private void NextRound()
		{
			HarvestMarkers[Round].Hidden = false;
			
			Round++;
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
			
			List<string> round1ActionCards = new List<string> { "Blacksmithing", "Sheep farming", "Ore mine construction" };
			ShuffleList(round1ActionCards);
			List<string> round2ActionCards = new List<string> { "Donkey farming", "Ruby mine construction" };
			ShuffleList(round2ActionCards);
			round2ActionCards.Insert(0, "Wish for Children");
			List<string> round3ActionCards = new List<string> { "Family life", "Ore delivery", "Exploration"};
			ShuffleList(round3ActionCards);
			List<string> round4ActionCards = new List<string> { "Ruby delivery", "Ore trading", "Adventure"};
			ShuffleList(round4ActionCards);
		}
		
		private ObservableCollection<HarvestMarker> harvestmarkers;
		
		public ObservableCollection<HarvestMarker> HarvestMarkers
		{
			get { return harvestmarkers; }
			set { harvestmarkers = value; 
				if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("HarvestMarkers"));
			}
		}
		
		private void PrepareHarvestMarkers()
		{
			List<HarvestMarker> tmp = new List<HarvestMarker>();
			for(int i = 0; i < 3; i++)
				tmp.Add(new HarvestMarker(HarvestMarker.Type.QuestionMark){Hidden = true});
			for(int i = 0; i < 4; i++)
				tmp.Add(new HarvestMarker(HarvestMarker.Type.Harvest){Hidden= true});
			
			//TODO: Use a generic shuffle function
			ShuffleList(tmp);
			
			tmp.Insert(0, new HarvestMarker(HarvestMarker.Type.NoHarvest));
			tmp.Insert(1, new HarvestMarker(HarvestMarker.Type.NoHarvest));
			tmp.Insert(2, new HarvestMarker(HarvestMarker.Type.Harvest));
			tmp.Insert(3, new HarvestMarker(HarvestMarker.Type.Pay1FoodPerDwarf));
			tmp.Insert(4, new HarvestMarker(HarvestMarker.Type.Harvest));
			
			int j = 12;
			foreach(HarvestMarker hm in tmp)
			{
				hm.Row = j%3;
				hm.Column = j/3;
				harvestmarkers.Add(hm);
				j++;
			}
			
		}
		
		private void ShuffleList<T>(List<T> list)
		{
			Random r = new Random();
			list.Sort((x, y) => r.Next(-1, 1));
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
				case "Blacksmithing":
					ac.Name = "Blacksmithing";
					ac.PlayerAction = new Action<Dwarf>((d) =>
			                          {
			                          	ac.Blacksmithing(d);
			                          });
					break;
				case "Sheep farming":
					ac.Name = "Sheep farming";
					ac.Accumulators.Add(new ResourceAccumulator(){ResourceType = Resource.Type.Sheep, StartingAmount = 1, Accumulation = 1});
					ac.PlayerAction = new Action<Dwarf>((d) =>
			                          {
			                          	ac.Sheepfarming(d);
			                          });
					break;
				case "Ore mine construction":
					ac.Name = "Ore mine construction";
					ac.Accumulators.Add(new ResourceAccumulator(){ResourceType = Resource.Type.Sheep, StartingAmount = 1, Accumulation = 1});
					ac.PlayerAction = new Action<Dwarf>((d) =>
			                          {
			                          	ac.Oremineconstruction(d);
			                          });
					break;
					
				case "Wish for children":
					
					break;
				case "Donkey farming":
					ac.Name = "Donkey farming";
					ac.Accumulators.Add(new ResourceAccumulator(){ResourceType = Resource.Type.Donkey, StartingAmount = 1, Accumulation = 1});
					ac.PlayerAction = new Action<Dwarf>((d) =>
			                          {
			                          	ac.Donkeyfarming(d);
			                          });
					break;
				case "Ruby mine construction":
					break;
				case "Family life":
					break;
				case "Ore delivery":
					break;
				case "Exploration":
					break;
				default:
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
