/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 2/4/2017
 * Time: 3:20 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Controls;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CavernaWPF.Layables;
using CavernaWPF.Resources;

namespace CavernaWPF
{
	/// <summary>
	/// Description of FeedingPhase.
	/// </summary>
	public class FeedingPhase : INotifyPropertyChanged
	{
		private ObservableCollection<FeedingOption> foodAndStarvation = new ObservableCollection<FeedingOption>();// { new ResourceItem(Resource.Type.Food) };
		
		private bool feedingTime;
		
		public bool FeedingTime
		{
			get { return feedingTime; }
			set {
				feedingTime = value;
				if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("FeedingTime"));
			}
		}
		
		public ObservableCollection<FeedingOption> FoodAndStarvation
		{
			get { return foodAndStarvation; }
			set {
				foodAndStarvation = value;
				if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("FoodAndStarvation"));
			}
		}
		
		public double Width
		{
			get;
			set;
		}
		
		private Player player;

		public FeedingPhaseWindow feedingPrompt = new FeedingPhaseWindow();
		
		public FeedingPhase(Player p)
		{
			player = p;
			feedingPrompt.DataContext = this;
		}
		
		public void PromptFeeding(bool oneFoodPerDwarf = false)
		{
			Width = player.Dwarfs.Count * 48;
			
			FeedingTime = true;
			
			int index = 0;
			
			foreach(Dwarf d in player.Dwarfs)
			{
				if(oneFoodPerDwarf || d.isBaby)
				{
					FoodAndStarvation.Add(new FeedingOption(this){  X = index * 48 + 17.5 });
				}
				else
				{
					FoodAndStarvation.Add(new FeedingOption(this) { X = index * 48 } );
					FoodAndStarvation.Add(new FeedingOption(this) { X = index * 48 + (48 - 35) } );
				}
				index++;
			}
			
			RefreshFeedingOptions();
		}
		
		private int WorkingCaveFeedingOptions = 1;
		
		public class DwarfMeal
		{
			public Dwarf dwarf;
			public List<FeedingOption> meal = new List<FeedingOption>();
		}
		
		public void RefreshFeedingOptions()
		{
			var feedingOptions = new List<Resource.Type>();
			feedingOptions.Add(Resource.Type.Starvation);
			if(player.Resources[Resource.Type.Food].Amount > 0)
			{
				feedingOptions.Add(Resource.Type.Food);
			}
			FeedingOptions = feedingOptions;
			
			FurnishingTile workingCave;
			ActionBoardContext.Instance.FurnishingTiles.TryGetValue("Working Cave", out workingCave);
//			if(/*workingCave != null && workingCave.player == player && */WorkingCaveFeedingOptions > 0)
//			{
//				FeedingOptions.Add(Resource.Type.Wood);
//				FeedingOptions.Add(Resource.Type.Stone);
//				FeedingOptions.Add(Resource.Type.Ore);
//			}
		}
		
		private List<Resource.Type> feedingOptions = new List<Resource.Type>();
		
		public List<Resource.Type> FeedingOptions
		{
			get { return feedingOptions; }
			set {
				feedingOptions = value;
				if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("FeedingOptions"));
			}
		}
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		public class FeedingOption : INotifyPropertyChanged
		{
			public FeedingOption(FeedingPhase fp)
			{
				feedingphase = fp;
			}
			
			private FeedingPhase feedingphase;
			private double x;
    		private double y;
    		
    		public double Y
		    {
		        get { return y; }
		        set
		        {
		            y = value;
		            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Y"));
		        }
		    }
		
		    public double X
		    {
		        get { return x; }
		        set
		        {
		            x = value;
		            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("X"));
		        }
		    }
		    
		    private Resource.Type feedingOptionChosen;
		    
		    public Resource.Type FeedingOptionChosen
		    {
		    	get { return feedingOptionChosen; }
		    	set {
		    		if(feedingOptionChosen != value)
		    		{
		    			if(feedingOptionChosen == Resource.Type.Starvation)
		    				feedingphase.player.Resources[feedingOptionChosen].Amount--;
		    			else
		    				feedingphase.player.Resources[feedingOptionChosen].Amount++;
		    			
		    			feedingOptionChosen = value;
		    			
		    			if(feedingOptionChosen == Resource.Type.Starvation)
		    				feedingphase.player.Resources[feedingOptionChosen].Amount++;
		    			else
		    				feedingphase.player.Resources[feedingOptionChosen].Amount--;
		    			feedingphase.RefreshFeedingOptions();
		    		}
		    	}
		    }
		    
		    public event PropertyChangedEventHandler PropertyChanged;
		}
	}
}
