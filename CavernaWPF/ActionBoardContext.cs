﻿/*
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
		public bool FurnishingDwelling
		{
			get;
			set;
		}
		
		public bool FurnishingCavern
		{
			get;
			set;
		}
		
		private Dictionary<string, FurnishingTile> furnishingtiles = new Dictionary<string, FurnishingTile>();
		
		public Dictionary<string, FurnishingTile> FurnishingTiles
		{
			get { return furnishingtiles; }
			set {
				furnishingtiles = value;
				if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("FurnishingTiles"));
			}
		}
		
		private void PrepareFurnishingTiles()
		{			
			AddFurnishingTile("Cuddle room");
			AddFurnishingTile("Breakfast room");
			AddFurnishingTile("Stubble room");
			AddFurnishingTile("Work room");
			AddFurnishingTile("Guest room");
			AddFurnishingTile("Office room");
			
			AddFurnishingTile("Wood supplier");
			AddFurnishingTile("Stone supplier");
			AddFurnishingTile("Ruby supplier");
			AddFurnishingTile("Dog school");
			AddFurnishingTile("Quarry");
			AddFurnishingTile("Seam");
			
			AddFurnishingTile("Weaving parlor");
			AddFurnishingTile("Milking parlor");
			AddFurnishingTile("State parlor");
			AddFurnishingTile("Hunting parlor");
			AddFurnishingTile("Beer parlor");
			AddFurnishingTile("Blacksmithing parlor");
			
			AddFurnishingTile("Broom chamber");
			AddFurnishingTile("Treasure chamber");
			AddFurnishingTile("Food chamber");
			AddFurnishingTile("Prayer chamber");
			AddFurnishingTile("Writing chamber");
			AddFurnishingTile("Fodder chamber");
			
			AddFurnishingTile("Dwelling");
			AddFurnishingTile("Simple dwelling");
			AddFurnishingTile("Simple dwelling2");
			AddFurnishingTile("Mixed dwelling");
			AddFurnishingTile("Couple dwelling");
			AddFurnishingTile("Additional dwelling");
			
			AddFurnishingTile("Carpenter");
			AddFurnishingTile("Stone carver");
			AddFurnishingTile("Blacksmith");
			AddFurnishingTile("Miner");
			AddFurnishingTile("Builder");
			AddFurnishingTile("Trader");
			
			AddFurnishingTile("Slaughtering cave");
			AddFurnishingTile("Cooking cave");
			AddFurnishingTile("Working cave");
			AddFurnishingTile("Mining cave");
			AddFurnishingTile("Breeding cave");
			AddFurnishingTile("Peaceful cave");
			
			AddFurnishingTile("Stone storage");
			AddFurnishingTile("Ore storage");
			AddFurnishingTile("Spare part storage");
			AddFurnishingTile("Main storage");
			AddFurnishingTile("Weapon storage");
			AddFurnishingTile("Supplies storage");
		}
		
		private void AddFurnishingTile(string name)
		{
			FurnishingTile ft = new FurnishingTile();
			bool found = true;
			switch(name)
			{
				case "Dwelling":
					ft.Cost.Add(new ResourceTab(Resource.Type.Wood, 4));
					ft.Cost.Add(new ResourceTab(Resource.Type.Stone, 3));
					break;
				case "Simple dwelling":
					ft.Cost.Add(new ResourceTab(Resource.Type.Wood, 4));
					ft.Cost.Add(new ResourceTab(Resource.Type.Stone, 2));
					break;
				case "Simple dwelling2":
					ft.Cost.Add(new ResourceTab(Resource.Type.Wood, 3));
					ft.Cost.Add(new ResourceTab(Resource.Type.Stone, 3));
					break;
				case "Mixed dwelling":
					ft.Cost.Add(new ResourceTab(Resource.Type.Wood, 5));
					ft.Cost.Add(new ResourceTab(Resource.Type.Stone, 4));
					break;
				case "Couple dwelling":
					ft.Cost.Add(new ResourceTab(Resource.Type.Wood, 8));
					ft.Cost.Add(new ResourceTab(Resource.Type.Stone, 6));
					break;
				case "Additional dwelling":
					ft.Cost.Add(new ResourceTab(Resource.Type.Wood, 4));
					ft.Cost.Add(new ResourceTab(Resource.Type.Stone, 3));
					break;
				case "Carpenter":
					ft.Effect = new Action<Player>((p) =>
                         {
					     	ft.Carpenter(p);
                         });
					ft.Cost.Add(new ResourceTab(Resource.Type.Stone, 1));
					break;
				case "Stone carver":
					ft.Effect = new Action<Player>((p) =>
                         {
					     	ft.Stonecarver(p);
                         });
					ft.Cost.Add(new ResourceTab(Resource.Type.Wood, 1));
					break;
				case "Blacksmith":
					ft.Effect = new Action<Player>((p) =>
                         {
					     	ft.Blacksmith(p);
                         });
					ft.Cost.Add(new ResourceTab(Resource.Type.Wood, 1));
					ft.Cost.Add(new ResourceTab(Resource.Type.Stone, 2));
					break;
				case "Miner":
					ft.Effect = new Action<Player>((p) =>
                         {
					     	ft.Miner(p);
                         });
					ft.Cost.Add(new ResourceTab(Resource.Type.Wood, 1));
					ft.Cost.Add(new ResourceTab(Resource.Type.Stone, 1));
					break;
				case "Builder":
					ft.Effect = new Action<Player>((p) =>
                         {
					     	ft.Builder(p);
                         });
					ft.Cost.Add(new ResourceTab(Resource.Type.Stone, 1));
					break;
				case "Trader":
					ft.Effect = new Action<Player>((p) =>
                         {
					     	ft.Trader(p);
                         });
					ft.Cost.Add(new ResourceTab(Resource.Type.Wood, 1));
					break;
				case "Slaughtering cave":
					ft.Effect = new Action<Player>((p) =>
                         {
					     	ft.Slaughteringcave(p);
                         });
					ft.Cost.Add(new ResourceTab(Resource.Type.Wood, 2));
					ft.Cost.Add(new ResourceTab(Resource.Type.Stone, 2));
					break;
				case "Cooking cave":
					ft.Effect = new Action<Player>((p) =>
                         {
					     	ft.Cookingcave(p);
                         });
					ft.Cost.Add(new ResourceTab(Resource.Type.Stone, 2));
					break;
				case "Working cave":
					ft.Effect = new Action<Player>((p) =>
                         {
					     	ft.Workingcave(p);
                         });
					ft.Cost.Add(new ResourceTab(Resource.Type.Wood, 1));
					ft.Cost.Add(new ResourceTab(Resource.Type.Stone, 1));
					break;
				case "Mining cave":
					ft.Effect = new Action<Player>((p) =>
                         {
					     	ft.Miningcave(p);
                         });
					ft.Cost.Add(new ResourceTab(Resource.Type.Wood, 3));
					ft.Cost.Add(new ResourceTab(Resource.Type.Stone, 2));
					break;
				case "Breeding cave":
					ft.Effect = new Action<Player>((p) =>
                         {
					     	ft.Breedingcave(p);
                         });
					ft.Cost.Add(new ResourceTab(Resource.Type.Grain, 1));
					ft.Cost.Add(new ResourceTab(Resource.Type.Stone, 1));
					break;
				case "Peaceful cave":
					ft.Effect = new Action<Player>((p) =>
                         {
					     	ft.Peacefulcave(p);
                         });
					ft.Cost.Add(new ResourceTab(Resource.Type.Wood, 2));
					ft.Cost.Add(new ResourceTab(Resource.Type.Stone, 2));
					break;
				case "Stone storage":
					ft.Effect = new Action<Player>((p) =>
                         {
                          	ft.Stonestorage(p);
                         });
					ft.Cost.Add(new ResourceTab(Resource.Type.Wood, 3));
					ft.Cost.Add(new ResourceTab(Resource.Type.Ore, 1));
					break;
				case "Ore storage":
					ft.Effect = new Action<Player>((p) =>
                         {
                          	ft.Orestorage(p);
                         });
					ft.Cost.Add(new ResourceTab(Resource.Type.Wood, 1));
					ft.Cost.Add(new ResourceTab(Resource.Type.Stone, 1));
					break;	
				case "Spare part storage":
					ft.Effect = new Action<Player>((p) =>
                         {
                          	ft.Sparepartstorage(p);
                         });
					ft.Cost.Add(new ResourceTab(Resource.Type.Wood, 2));
					break;	
				case "Main storage":
					ft.Effect = new Action<Player>((p) =>
                         {
                          	ft.Mainstorage(p);
                         });
					ft.Cost.Add(new ResourceTab(Resource.Type.Wood, 2));
					ft.Cost.Add(new ResourceTab(Resource.Type.Stone, 1));
					break;	
				case "Weapon storage":
					ft.Effect = new Action<Player>((p) =>
                         {
                          	ft.Weaponstorage(p);
                         });
					ft.Cost.Add(new ResourceTab(Resource.Type.Wood, 3));
					ft.Cost.Add(new ResourceTab(Resource.Type.Stone, 2));
					break;	
				case "Supplies storage":
					ft.Effect = new Action<Player>((p) =>
                         {
                          	ft.Suppliesstorage(p);
                         });
					ft.Cost.Add(new ResourceTab(Resource.Type.Food, 3));
					ft.Cost.Add(new ResourceTab(Resource.Type.Wood, 1));
					break;	
				case "Cuddle room":
					ft.Effect = new Action<Player>((p) =>
                         {
                          	ft.Cuddleroom(p);
                         });
					ft.Cost.Add(new ResourceTab(Resource.Type.Wood, 1));
					break;
				case "Breakfast room":
					ft.Effect = new Action<Player>((p) =>
                         {
                          	ft.Breakfastroom(p);
                         });
					ft.Cost.Add(new ResourceTab(Resource.Type.Wood, 1));
					break;
				case "Stubble room":
					ft.Effect = new Action<Player>((p) =>
                         {
                          	ft.Stubbleroom(p);
                         });
					ft.Cost.Add(new ResourceTab(Resource.Type.Wood, 1));
					ft.Cost.Add(new ResourceTab(Resource.Type.Ore, 1));
					break;
				case "Work room":
					ft.Effect = new Action<Player>((p) =>
                         {
                          	ft.Guestroom(p);
                         });
					ft.Cost.Add(new ResourceTab(Resource.Type.Stone, 1));
					break;
				case "Guest room":
					ft.Effect = new Action<Player>((p) =>
                         {
                          	ft.Guestroom(p);
                         });
					ft.Cost.Add(new ResourceTab(Resource.Type.Wood, 1));
					ft.Cost.Add(new ResourceTab(Resource.Type.Stone, 1));
					break;
				case "Office room":
					ft.Effect = new Action<Player>((p) =>
                         {
                          	ft.Officeroom(p);
                         });
					ft.Cost.Add(new ResourceTab(Resource.Type.Stone, 1));
					break;
				case "Wood supplier":
					ft.Effect = new Action<Player>((p) =>
                         {
                          	ft.Woodsupplier(p);
                         });
					ft.roundsOfEffect = 7;
					ft.Cost.Add(new ResourceTab(Resource.Type.Stone, 1));
					break;
				case "Stone supplier":
					ft.Effect = new Action<Player>((p) =>
                         {
                          	ft.Stonesupplier(p);
                         });
					ft.roundsOfEffect = 5;
					ft.Cost.Add(new ResourceTab(Resource.Type.Wood, 1));
					break;
				case "Ruby supplier":
					ft.Effect = new Action<Player>((p) =>
                         {
                          	ft.Rubysupplier(p);
                         });
					ft.roundsOfEffect = 4;
					ft.Cost.Add(new ResourceTab(Resource.Type.Wood, 2));
					ft.Cost.Add(new ResourceTab(Resource.Type.Stone, 2));
					break;						
				case "Dog school":
					ft.Effect = new Action<Player>((p) =>
                         {
                          	ft.Dogschool(p);
                         });
					break;
				case "Quarry":
					ft.Effect = new Action<Player>((p) =>
                         {
                          	ft.Quarry(p);
                         });
					ft.Cost.Add(new ResourceTab(Resource.Type.Wood, 1));
					break;
				case "Seam":
					ft.Effect = new Action<Player>((p) =>
                         {
                          	ft.Seam(p);
                         });
					ft.Cost.Add(new ResourceTab(Resource.Type.Wood, 2));
					break;
				case "Weaving parlor":
					ft.Effect = new Action<Player>((p) =>
                         {
                          	ft.Weavingparlor(p);
                         });
					ft.Cost.Add(new ResourceTab(Resource.Type.Wood, 2));
					ft.Cost.Add(new ResourceTab(Resource.Type.Stone, 1));
					break;	
				case "Milking parlor":
					ft.Effect = new Action<Player>((p) =>
                         {
					     	ft.Milkingparlor(p);
                         });
					ft.Cost.Add(new ResourceTab(Resource.Type.Wood, 2));
					ft.Cost.Add(new ResourceTab(Resource.Type.Stone, 2));
					break;
				case "State parlor":
					ft.Effect = new Action<Player>((p) =>
                         {
					     	ft.Stateparlor(p);
                         });
					ft.Cost.Add(new ResourceTab(Resource.Type.Stone, 3));
					ft.Cost.Add(new ResourceTab(Resource.Type.Gold, 5));
					break;
				case "Hunting parlor":
					ft.Effect = new Action<Player>((p) =>
                         {
					     	ft.Huntingparlor(p);
                         });
					ft.Cost.Add(new ResourceTab(Resource.Type.Wood, 2));
					break;
				case "Beer parlor":
					ft.Effect = new Action<Player>((p) =>
                         {
					     	ft.Huntingparlor(p);
                         });
					ft.Cost.Add(new ResourceTab(Resource.Type.Wood, 2));
					break;
				case "Blacksmithing parlor":
					ft.Effect = new Action<Player>((p) =>
                         {
					     	ft.Blacksmithingparlor(p);
                         });
					ft.Cost.Add(new ResourceTab(Resource.Type.Ore, 3));
					break;
				case "Broom chamber":
					ft.Effect = new Action<Player>((p) =>
                         {
					     	ft.Broomchamber(p);
                         });
					ft.Cost.Add(new ResourceTab(Resource.Type.Wood, 1));
					break;
				case "Treasure chamber":
					ft.Effect = new Action<Player>((p) =>
                         {
					     	ft.Treasurechamber(p);
                         });
					ft.Cost.Add(new ResourceTab(Resource.Type.Wood, 1));
					ft.Cost.Add(new ResourceTab(Resource.Type.Stone, 1));
					break;
				case "Food chamber":
					ft.Effect = new Action<Player>((p) =>
                         {
					     	ft.Foodchamber(p);
                         });
					ft.Cost.Add(new ResourceTab(Resource.Type.Wood, 2));
					ft.Cost.Add(new ResourceTab(Resource.Type.Vegetable, 2));
					break;
				case "Prayer chamber":
					ft.Effect = new Action<Player>((p) =>
                         {
					     	ft.Prayerchamber(p);
                         });
					ft.Cost.Add(new ResourceTab(Resource.Type.Wood, 2));
					break;	
				case "Writing chamber":
					ft.Effect = new Action<Player>((p) =>
                         {
					     	ft.Writingchamber(p);
                         });
					ft.Cost.Add(new ResourceTab(Resource.Type.Stone, 2));
					break;
				case "Fodder chamber":
					ft.Effect = new Action<Player>((p) =>
                         {
					     	ft.Fodderchamber(p);
                         });
					ft.Cost.Add(new ResourceTab(Resource.Type.Grain, 2));
					ft.Cost.Add(new ResourceTab(Resource.Type.Stone, 1));
					break;
				default:
					found = false;
					break;
			}
			if(found)
			{
				ft.Name = name;
				ft.Img = String.Format("pack://application:,,,/Images/FurnishingTiles/{0}.png", name);
				int index = FurnishingTiles.Count;
				int group = index / 24;
				ft.Column = (index % 24) / 3;
				ft.Row = (index % 3) + 4 * group;
				FurnishingTiles.Add(name, ft);
			}
		}
		
		private static ActionBoardContext instance = new ActionBoardContext();
		
		public static ActionBoardContext Instance {
			get {
				return instance;
			}
		}
		
		private List<ActionCardWrapper> actioncards;
		
		private ObservableCollection<Dwarf> dwarfs;
		
		public ActionBoard control;
		public HarvestEventsCard harvesteventscard;
		public FurnishingWindow furnishingWindow;
		public TradeManager tradeManager;
		public GameStatusBar statusControl;
		
		public List<ActionCardWrapper> ActionCards
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
		
		private List<Player> players = new List<Player>();
		
		public List<Player> Players
		{
			get { return players; }
			set
			{
				players = value;
			}
		}
		
		public List<Player> nonfielders = new List<Player>();
		public List<Player> nonbreeders = new List<Player>();
		
		private ActionBoardContext()
		{
			actioncards = new List<ActionCardWrapper>();
			dwarfs = new ObservableCollection<Dwarf>();
			harvestmarkers = new ObservableCollection<HarvestMarker>();
			Intitialize();
		}
		
		private void Intitialize()
		{
			ActionBoard ab = new ActionBoard();
			control = ab;
			ab.DataContext = this;
			HarvestEventsCard hec = new HarvestEventsCard();
			harvesteventscard = hec;
			hec.DataContext = this;
			FurnishingWindow fw = new FurnishingWindow();
			furnishingWindow = fw;
			tradeManager = new TradeManager();
			fw.DataContext = this;
			GameStatusBar gsb = new GameStatusBar();
			statusControl = gsb;
			gsb.DataContext = this;
		}
		
		public void Replenish()
		{
			foreach(ActionCardWrapper actioncardwrapper in actioncards)
			{
				if(!actioncardwrapper.Hidden)
				{
					actioncardwrapper.Accumulate();
					actioncardwrapper.actionCard.Accumulate();
				}
			}
		}
		
		public void PromptDwarf(Dwarf d)
		{
			Dwarfs.Add(d);
		}

		LinkedList<Player> playersPlaying = new LinkedList<Player>();
		
		public LinkedListNode<Player> currentPlayer;
		
		public Dwarf currentDwarf;
		
		public enum Phase {ActionPhase, ConfirmPhase, NoHarvest, Pay1FoodPerDwarf, FieldPhase, FeedingPhase, BreedingPhase, SkipFieldOrBreed}
		
		public bool readyForNextDwarf = true;
		
		public bool feedingPrompted = false;
		
		private Phase currentPhase;
		
		public Phase CurrentPhase
		{
			get { return currentPhase; }
			set {
				currentPhase = value;
				if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("CurrentPhase"));
			}
		}
		
		public void StartGame()
		{
			PrepareActionCards();	
			
			PrepareHarvestMarkers();
			
			PrepareFurnishingTiles();
			
			NextRound();
			
			Replenish();
			
			QueuePlayers();
			
			CurrentPhase = Phase.ActionPhase;
		}
		
		public void NextTurn()
		{			
			if(CurrentPhase == Phase.ActionPhase)
			{
				if(!readyForNextDwarf)
					return;
				
				currentDwarf = null;
				while(currentDwarf == null)
				{
					if(currentPlayer == null)
					{
						ActionCards.ToList().ForEach(ac => ac.occupied = false);
						
						//TODO: Encapsulate -----------------
						List<Dwarf> orderedDwarfs = ActionBoardContext.Instance.Dwarfs.OrderBy(d => d.Level).Reverse().ToList();
						foreach(Dwarf dw in orderedDwarfs)
						{
							dw.X = 0;
							dw.Y = 0;
							dw.Locked = false;
							dw.player.Dwarfs.Add(dw);
							ActionBoardContext.Instance.Dwarfs.Remove(dw);
						}
						//------------------------------------
						GetNextPhase();
						return;
					}
					
					Player p = currentPlayer.Value;
					currentDwarf = p.GetNextDwarf();
					if(currentDwarf == null)
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
				ActionBoardContext.Instance.AddDwarf(currentDwarf);
				CurrentPhase = Phase.ConfirmPhase;
			}
			else if(CurrentPhase == Phase.ConfirmPhase)
			{
				if(!ConfirmEndTurn())
					return;
				
				if(currentPlayer.Next == null)
				{
					currentPlayer = playersPlaying.First;
				}
				else
				{
					currentPlayer = currentPlayer.Next;
				}
				CurrentPhase = Phase.ActionPhase;
				NextTurn();
			}
			else
			{
				if(CurrentPhase == Phase.SkipFieldOrBreed)
				{
					SkipFieldOrBreed();
					CurrentPhase = Phase.FieldPhase;
					return;
				}
				else if(CurrentPhase == Phase.FieldPhase)
				{
					FieldPhase();
					CurrentPhase = Phase.FeedingPhase;
					return;
				}
				else if(CurrentPhase == Phase.FeedingPhase)
				{
					if(feedingPrompted)
					{
						if(feedingPrompts.All(fp => !fp.FeedingTime))
						{
							CurrentPhase = Phase.BreedingPhase;
							feedingPrompted = false;
						}
					}
					else
					{
						FeedingPhase();
						feedingPrompted = true;
					}
					return;
				}
				else if(CurrentPhase == Phase.Pay1FoodPerDwarf)
				{
					if(feedingPrompted)
					{
						if(feedingPrompts.All(fp => !fp.FeedingTime))
						{
							CurrentPhase = Phase.ActionPhase;
							feedingPrompted = false;
						}
					}
					else
					{
						FeedingPhase(true);
						feedingPrompted = true;
					}
					return;
				}
				else if(CurrentPhase == Phase.BreedingPhase)
				{
					BreedingPhase();
//					if(!ConfirmLayables())
//						return;
				}
				Round++;
				NextRound();
				Replenish();
				QueuePlayers();
				if(CurrentPhase == Phase.SkipFieldOrBreed)
				{
					nonfielders.Clear();
					nonbreeders.Clear();
				}
				CurrentPhase = Phase.ActionPhase;
			}
		}
		
		public delegate void NewRoundEventHandler(object sender, EventArgs args);
		
		public event NewRoundEventHandler NewRound;
		
		private void OnNewRound()
		{
			if (NewRound != null)
				NewRound(this, EventArgs.Empty);
		}
		
		private void GetNextPhase()
		{
			if(HarvestMarkers[Round].type == HarvestMarker.Type.Harvest)
			{
				CurrentPhase = Phase.FieldPhase;
			}
			else if(HarvestMarkers[Round].type == HarvestMarker.Type.QuestionMark)
			{
				if(harvesteventscounter == 1)
					CurrentPhase = Phase.NoHarvest;
				else if(harvesteventscounter == 2)
					CurrentPhase = Phase.Pay1FoodPerDwarf;
				else if(harvesteventscounter == 3)
					CurrentPhase = Phase.SkipFieldOrBreed;
			}
			else if(HarvestMarkers[Round].type == HarvestMarker.Type.Pay1FoodPerDwarf)
			{
				CurrentPhase = Phase.Pay1FoodPerDwarf;
			}
			else if(HarvestMarkers[Round].type == HarvestMarker.Type.NoHarvest)
			{
				CurrentPhase = Phase.NoHarvest;
			}
		}
		
		public Button startButton = new Button() { Height = 30, Width = 60, Content = "Proceed"};
		
		int Round = 0;
		
		public bool Sow;
		
		private bool ConfirmEndTurn()
		{
			var g = currentPlayer.Value.town.Tiles.OfType<Sowable>().ToList();
			var deleteList = currentPlayer.Value.town.Tiles.ToList().Where(layable => layable.row == 0 && layable.column == 0).ToList();
			
			if(deleteList.Count> 0)
			{
				if(!deleteList.All(d => d is Sowable))
				{
					MessageBoxResult result = MessageBox.Show("Are you sure you want to throw the tiles away", "There are tiles not placed", MessageBoxButton.YesNo, MessageBoxImage.Question);
					if (result == MessageBoxResult.No)
					{
					    return false;
					}
				}
				deleteList.ForEach(l => currentPlayer.Value.town.Tiles.Remove(l));
			}
			
			if(Sow == true)
			{
				Sow = false;
				
				List<Sowable> newlyPlanted = currentPlayer.Value.town.Tiles.OfType<Sowable>().Where(t => !t.Locked).ToList();
				
				foreach(Sowable sw in newlyPlanted)
				{
					if(sw.type == Sowable.Type.Grain)
					{
						currentPlayer.Value.town.Tiles.Add(new Sowable(Sowable.Type.Grain){row = sw.row, column = sw.column, X = sw.X, Y = sw.Y - 8});
						currentPlayer.Value.town.Tiles.Add(new Sowable(Sowable.Type.Grain){row = sw.row, column = sw.column, X = sw.X, Y = sw.Y - 16});
					}
					else if(sw.type == Sowable.Type.Vegetable)
					{
						currentPlayer.Value.town.Tiles.Add(new Sowable(Sowable.Type.Vegetable){row = sw.row, column = sw.column, X = sw.X, Y = sw.Y - 8});
					}
				}
			}
			
			var unlockedTiles = currentPlayer.Value.town.Tiles.OfType<Tile>().Where(t => !t.Locked).ToList();
			
			if(ActionBoardContext.Instance.FurnishingTiles["Office room"].player == currentPlayer.Value)
			{
				int overhangingtiles = unlockedTiles.Count(t => t.column == 0 || t.column == 7 || t.row == 0 || t.row == 5);
				currentPlayer.Value.Resources[Resource.Type.Gold].Amount += 2 * overhangingtiles;
			}
			
			//TODO: use a Group By instead 
			//delete covered tiles
			foreach(var unlockedTile in unlockedTiles)
			{
				List<Tile> tiles = currentPlayer.Value.town.Tiles.OfType<Tile>().Where(t => t.column == unlockedTile.column && t.row == unlockedTile.row).OrderBy(t => t.Z).ToList();
				if(tiles.Count > 1)
				{
					tiles.GetRange(0,tiles.Count - 2).ForEach(t => currentPlayer.Value.town.Tiles.Remove(t));
				}
			}
			
			unlockedTiles.OfType<FurnishingTileLayable>().ToList().ForEach(ftl => ftl.furnishingtile.Effect.Invoke(currentPlayer.Value));
			unlockedTiles.OfType<FurnishingTileLayable>().ToList().ForEach(ftl => ftl.furnishingtile.player = currentPlayer.Value);
			
			currentPlayer.Value.town.Tiles.ToList().ForEach(t => t.Locked = true);
			
			return true;
 		}
		
		private void ConfirmLayables()
		{
			foreach(Player p in players)
			{
				var deleteList = p.town.Tiles.ToList().Where(layable => layable.row == 0 && layable.column == 0).ToList();
				
				if(deleteList.Count> 0)
				{
					if(!deleteList.All(d => d is Sowable))
					{
						MessageBoxResult result = MessageBox.Show("Are you sure you want to throw the tiles away", "There are tiles not placed", MessageBoxButton.YesNo, MessageBoxImage.Question);
						if (result == MessageBoxResult.No)
						{
							
						}
					}
					deleteList.ForEach(l => currentPlayer.Value.town.Tiles.Remove(l));
				}
			}
		}
		
		private int harvesteventscounter;
		
		public int HarvestEventsCounter
		{
			get { return harvesteventscounter; }
			set {
				harvesteventscounter = value;
				if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("HarvestEventsCounter"));
			}
		}
		
		private void SkipFieldOrBreed()
		{
			int index = 0;
			foreach(Player p in players)
			{
				SkipFieldOrBreedWindow promptWindow = new SkipFieldOrBreedWindow();
				promptWindow.Owner = LayoutManager.Instance.appWindow;
				promptWindow.WindowStartupLocation = WindowStartupLocation.Manual;
				//hardcode measurements
				promptWindow.Top = LayoutManager.Instance.appWindow.Top + 455;
				promptWindow.Left = LayoutManager.Instance.appWindow.Left + 590*index;
				promptWindow.ShowDialog();
				if( (bool) promptWindow.DialogResult)
				{
					nonfielders.Add(p);
				}
				else
				{
					nonbreeders.Add(p);
				}
				index++;
			}
		}
		
		private void FieldPhase()
		{
			foreach(Player p in players)
			{
				var fields = p.town.Tiles.OfType<Tile>().Where(t => t.type == Tile.Type.Field || t.type == Tile.Type.FieldDummy).ToList();
				foreach(Tile field in fields)
				{
					List<Sowable> occupyingSowables = p.town.Tiles.OfType<Sowable>().Where(s => s.row == field.row && s.column == field.column).ToList();
					if(occupyingSowables.Count > 0)
					{
						Sowable ripe = occupyingSowables.Last();
						if(ripe.type == Sowable.Type.Grain)
							p.Resources[Resource.Type.Grain].Amount++;
						if(ripe.type == Sowable.Type.Vegetable)
							p.Resources[Resource.Type.Vegetable].Amount++;
						p.town.Tiles.Remove(ripe);
					}
				}
			}
		}
		
		public List<FeedingPhase> feedingPrompts = new List<FeedingPhase>();
		
		private void FeedingPhase(bool onePerDwarf = false)
		{
			foreach(FeedingPhase fp in feedingPrompts)
			{
				fp.PromptFeeding();
			}
		}
		
		private void BreedingPhase()
		{
			foreach(Player p in players)
			{
				p.town.BreedAnimals();
			}
		}
		
		private void NextRound()
		{
			if(Round < 12)
			{
				HarvestMarkers[Round].Hidden = false;
				if(HarvestMarkers[Round].type == HarvestMarker.Type.QuestionMark)
					HarvestEventsCounter++;
				ActionCards[Round+12].Hidden = false;
				if(ActionCards[Round+12].actionCard.Name == "Family life")
				{
					ActionCardWrapper ac = ActionCards.Find(acw => acw.actionCard.Name == "Wish for children");
					ac.actionCard = GetActionCard("Urgent wish for children");
					ac.setImage();
				}
				OnNewRound();
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
			currentPlayer = playersPlaying.Find(startingPlayer);
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
			
			AddActionCard(GetActionCard("Wish for children"));
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
			ActionCard ac = new ActionCard(Name);
			ac.Name = Name;
			switch(Name)
			{
				case "Drift mining":
					ac.Accumulators.Add(new ResourceAccumulator(){ResourceType = Resource.Type.Stone, StartingAmount = 1, Accumulation = 1, topYLimit = 10, bottomYLimit = 70, leftXLimit = 35, rightXLimit=101});
					ac.PlayerAction = new Action<Dwarf>((d) =>
                          {
                          	ac.Driftmining(d);
                          });
					break;
				case "Logging":
					ac.Accumulators.Add(new ResourceAccumulator(){ResourceType = Resource.Type.Wood, StartingAmount = 3, Accumulation = 1, topYLimit = 40, bottomYLimit = 80, leftXLimit = 0, rightXLimit=101});
					ac.PlayerAction = new Action<Dwarf>((d) =>
					                          {
					                          	ac.Logging(d);
					                          });
					break;
				case "Wood gathering":
					ac.Accumulators.Add(new ResourceAccumulator(){ResourceType = Resource.Type.Wood, StartingAmount = 1, Accumulation = 1, topYLimit = 30, bottomYLimit = 137, leftXLimit = 0, rightXLimit=101});
					ac.PlayerAction = new Action<Dwarf>((d) =>
			                          {
			                          	ac.Woodgathering(d);
			                          });
					break;
				case "Excavation":
					ac.Accumulators.Add(new ResourceAccumulator(){ResourceType = Resource.Type.Stone, StartingAmount = 1, Accumulation = 1, topYLimit = 10, bottomYLimit = 70, leftXLimit = 35, rightXLimit=101});
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
					ac.Accumulators.Add(new ResourceAccumulator(){ResourceType = Resource.Type.Wood, StartingAmount = 1, Accumulation = 1, topYLimit = 10, bottomYLimit = 70, leftXLimit = 35, rightXLimit=101});
					ac.PlayerAction = new Action<Dwarf>((d) =>
			                          {
			                          	ac.Clearing(d);
			                          });
					break;
				case "Starting player":
					ac.Accumulators.Add(new ResourceAccumulator(){ResourceType = Resource.Type.Food, StartingAmount = 1, Accumulation = 1, topYLimit = 10, bottomYLimit = 90, leftXLimit = 35, rightXLimit=101});
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
				case "Urgent wish for children":
					ac.PlayerAction = new Action<Dwarf>((d) =>
			                          {
			                          	ac.Urgentwishforchildren(d);
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
