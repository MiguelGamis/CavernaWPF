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
using System.Windows.Controls;
using System.Linq;

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
			
			NextRound();
			
			QueuePlayers();
		}
		
		LinkedList<Player> playersPlaying = new LinkedList<Player>();
		
		public LinkedListNode<Player> currentPlayer;
		
		public bool readyForNextDwarf = true;
		
		//public Dictionary<Player, bool> playerLocks = new Dictionary<Player, bool>();
		public bool promptingPlacement= false;
		
		public void NextTurn()
		{
			if(!readyForNextDwarf || promptingPlacement)
			{
				return;
			}
			
			if(currentPlayer == null)
				currentPlayer = playersPlaying.Find(startingPlayer);
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
					ActionCards.ToList().ForEach(ac => ac.occupied = false);
					
					//TODO: Encapsulate -----------------
					for(int i = ActionBoardContext.Instance.Dwarfs.Count - 1; i >= 0; i--)
					{
						Dwarf dw = ActionBoardContext.Instance.Dwarfs[i];
						dw.X = 0;
						dw.Y = 0;
						dw.player.Dwarfs.Add(dw);
						ActionBoardContext.Instance.Dwarfs.RemoveAt(i);
					}
					Harvest();
					NextRound();
					Replenish();
					QueuePlayers();
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
		
		public Button startButton = new Button() { Height = 30, Width = 60, Content = "Proceed"};
		
		int Round = 0;
		
		private void Harvest()
		{
		}
		
		private void NextRound()
		{
			if(Round < 12)
			{
				HarvestMarkers[Round].Hidden = false;
				ActionCards[Round+12].Hidden = false;
				Round++;
			}
			else
				Scoring();
		}
		
		private Player startingPlayer;
		
		public Player StartingPlayer
		{
			get { return startingPlayer; }
			set
			{
				if(value != null && value != startingPlayer)
				{
					if(startingPlayer != null)
						LayoutManager.Instance.map[startingPlayer].Children.Remove(LayoutManager.Instance.startingPlayerPiece);
					startingPlayer = value;
					LayoutManager.Instance.map[startingPlayer].Children.Add(LayoutManager.Instance.startingPlayerPiece);
					//LayoutManager.Instance.updateStartingPlayerPiece(value);
				}
			}
		}
		
		private void QueuePlayers()
		{
			foreach(Player p in players)
			{
				LinkedListNode<Player> pNode = new LinkedListNode<Player>(p);
				playersPlaying.AddLast(pNode);
			}
		}
		
		private void Scoring()
		{
			
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
			foreach(String actionCardName in round1ActionCards)
			{
				AddActionCard(GetActionCard(actionCardName), true);
			}
			
			AddActionCard(GetActionCard("Wish For Children"));
			List<string> round2ActionCards = new List<string> { "Donkey farming", "Ruby mine construction" };
			ShuffleList(round2ActionCards);
			foreach(String actionCardName in round2ActionCards)
			{
				AddActionCard(GetActionCard(actionCardName), true);
			}
			
			List<string> round3ActionCards = new List<string> { "Family life", "Ore delivery", "Exploration"};
			ShuffleList(round3ActionCards);
			foreach(String actionCardName in round3ActionCards)
			{
				AddActionCard(GetActionCard(actionCardName), true);
			}
			List<string> round4ActionCards = new List<string> { "Ruby delivery", "Ore trading", "Adventure"};
			ShuffleList(round4ActionCards);
			foreach(String actionCardName in round4ActionCards)
			{
				AddActionCard(GetActionCard(actionCardName), true);
			}
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
			ac.Name = Name;
			switch(Name)
			{
				case "Drift mining":
					ac.Accumulators.Add(new ResourceAccumulator(){ResourceType = Resource.Type.Stone, StartingAmount = 1, Accumulation = 1});
					ac.PlayerAction = new Action<Dwarf>((d) =>
                          {
                          	ac.Driftmining(d);
                          });
					break;
				case "Logging":
					ac.Accumulators.Add(new ResourceAccumulator(){ResourceType = Resource.Type.Wood, StartingAmount = 3, Accumulation = 1});
					ac.PlayerAction = new Action<Dwarf>((d) =>
					                          {
					                          	ac.Logging(d);
					                          });
					break;
				case "Wood gathering":
					ac.Accumulators.Add(new ResourceAccumulator(){ResourceType = Resource.Type.Wood, StartingAmount = 1, Accumulation = 1});
					ac.PlayerAction = new Action<Dwarf>((d) =>
			                          {
			                          	ac.Woodgathering(d);
			                          });
					break;
				case "Excavation":
					ac.Accumulators.Add(new ResourceAccumulator(){ResourceType = Resource.Type.Stone, StartingAmount = 1, Accumulation = 1});
					ac.PlayerAction = new Action<Dwarf>((d) =>
					                          {
					                          	ac.Excavation(d);
					                          });
					break;
				case "Supplies":
					ac.PlayerAction = new Action<Dwarf>((d) =>
			                          {
			                          	ac.Supplies(d);
			                          });
					break;
				case "Clearing":
					ac.Accumulators.Add(new ResourceAccumulator(){ResourceType = Resource.Type.Wood, StartingAmount = 1, Accumulation = 1});
					ac.PlayerAction = new Action<Dwarf>((d) =>
			                          {
			                          	ac.Clearing(d);
			                          });
					break;
				case "Starting player":
					ac.Accumulators.Add(new ResourceAccumulator(){ResourceType = Resource.Type.Food, StartingAmount = 1, Accumulation = 1});
					ac.PlayerAction = new Action<Dwarf>((d) =>
			                          {
			                          	ac.Startingplayer(d);
			                          });
					break;	
				case "Ore mining":
					ac.Accumulators.Add(new ResourceAccumulator(){ResourceType = Resource.Type.Ore, StartingAmount = 2, Accumulation = 1});
					ac.PlayerAction = new Action<Dwarf>((d) =>
			                          {
			                          	ac.Oremining(d);
			                          });
					break;
				case "Sustenance":
					ac.Accumulators.Add(new ResourceAccumulator(){ResourceType = Resource.Type.Food, StartingAmount = 1, Accumulation = 1});
					ac.PlayerAction = new Action<Dwarf>((d) =>
			                          {
			                          	ac.Sustenance(d);
			                          });
					break;
				case "Ruby mining":
					ac.Accumulators.Add(new ResourceAccumulator(){ResourceType = Resource.Type.Ruby, StartingAmount = 1, Accumulation = 1});
					ac.PlayerAction = new Action<Dwarf>((d) =>
			                          {
			                          	ac.Rubymining(d);
			                          });
					break;
				case "Housework":
					ac.PlayerAction = new Action<Dwarf>((d) =>
			                          {
			                          	ac.Housework(d);
			                          });
					break;
				case "Slash-and-burn":
					ac.PlayerAction = new Action<Dwarf>((d) =>
			                          {
			                          	ac.Slashandburn(d);
			                          });
					break;
				case "Blacksmithing":
					ac.Stage = 1;
					ac.PlayerAction = new Action<Dwarf>((d) =>
			                          {
			                          	ac.Blacksmithing(d);
			                          });
					break;
				case "Sheep farming":
					ac.Stage = 1;
					ac.Accumulators.Add(new ResourceAccumulator(){ResourceType = Resource.Type.Sheep, StartingAmount = 1, Accumulation = 1});
					ac.PlayerAction = new Action<Dwarf>((d) =>
			                          {
			                          	ac.Sheepfarming(d);
			                          });
					break;
				case "Ore mine construction":
					ac.Stage = 1;
					ac.PlayerAction = new Action<Dwarf>((d) =>
			                          {
			                          	ac.Oremineconstruction(d);
			                          });
					break;
					
				case "Wish for children":
					ac.PlayerAction = new Action<Dwarf>((d) =>
			                          {
			                          	ac.Wishforchildren(d);
			                          });
					break;
				case "Donkey farming":
					ac.Stage = 2;
					ac.Accumulators.Add(new ResourceAccumulator(){ResourceType = Resource.Type.Donkey, StartingAmount = 1, Accumulation = 1});
					ac.PlayerAction = new Action<Dwarf>((d) =>
			                          {
			                          	ac.Donkeyfarming(d);
			                          });
					break;
				case "Ruby mine construction":
					ac.Stage = 2;
					ac.PlayerAction = new Action<Dwarf>((d) =>
					                  {
					                  	ac.Rubymineconstruction(d);
					                  });
					break;
				case "Family life":
					ac.Stage = 3;
					ac.PlayerAction = new Action<Dwarf>((d) =>
					                  {
					                  	ac.Familylife(d);
					                  });
					break;
				case "Ore delivery":
					ac.Stage = 3;
					ac.Accumulators.Add(new ResourceAccumulator(){ResourceType = Resource.Type.Stone, StartingAmount = 1, Accumulation = 1});
					ac.Accumulators.Add(new ResourceAccumulator(){ResourceType = Resource.Type.Ore, StartingAmount = 1, Accumulation = 1});
					ac.PlayerAction = new Action<Dwarf>((d) =>
					                  {
					                  	ac.Oredelivery(d);
					                  });
					break;
				case "Exploration":
					ac.Stage = 3;
					ac.PlayerAction = new Action<Dwarf>((d) =>
					                  {
					                  	ac.Exploration(d);
					                  });
					break;
				case "Adventure":
					ac.Stage = 4;
					ac.PlayerAction = new Action<Dwarf>((d) =>
					                  {
					                  	ac.Adventure(d);
					                  });
					break;
				case "Ore trading":
					ac.Stage = 4;
					ac.PlayerAction = new Action<Dwarf>((d) =>
					                  {
					                  	ac.Oretrading(d);
					                  });
					break;
				case "Ruby delivery":
					ac.Stage = 4;
					ac.Accumulators.Add(new ResourceAccumulator(){ResourceType = Resource.Type.Ruby, StartingAmount = 2, Accumulation = 1});
					ac.PlayerAction = new Action<Dwarf>((d) =>
					                  {
					                  	ac.Rubydelivery(d);
					                  });
					break;
				default:
					break;
			}
			return ac;
		}
		
		private void AddActionCard(ActionCard ac, bool isHidden = false)
		{
			if(ac == null)
				return;
			ActionCardWrapper acw = new ActionCardWrapper(ac) { Column=actioncards.Count/3, Row=actioncards.Count%3, Hidden = isHidden };
			actioncards.Add(acw);
		}

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
