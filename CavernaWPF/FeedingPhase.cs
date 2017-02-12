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
		private ObservableCollection<ResourceItem> foodAndStarvation = new ObservableCollection<ResourceItem>();
		
		private bool feedingTime;
		
		public bool FeedingTime
		{
			get { return feedingTime; }
			set {
				feedingTime = value;
				if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("FeedingTime"));
			}
		}
		
		public ObservableCollection<ResourceItem> FoodAndStarvation
		{
			get { return foodAndStarvation; }
			set {
				foodAndStarvation = value;
				if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("FoodAndStarvation"));
			}
		}
		
		private double width = 0;
		
		public double Width
		{
			get { return width; }
			set {
				width = value;
				if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Width"));
			}
		}
		
		private Player player;

		public FeedingPhaseWindow feedingPrompt = new FeedingPhaseWindow();
		
		public FeedingPhase(Player p)
		{
			player = p;
			feedingPrompt.DataContext = this;
		}
		
		private double delta_y = -(35+20+38);
		
		private int foodPerDwarf = 0; 
		
		public void PromptFeeding(bool oneFoodPerDwarf = false)
		{
			FeedingTime = true;
			
			foodPerDwarf = oneFoodPerDwarf ? 1 : 2;
			
			Width = player.Dwarfs.Count * 48;
			
			FurnishingTile workingcave;
			ActionBoardContext.Instance.FurnishingTiles.TryGetValue("Working cave", out workingcave);
			
			int index = 0;
			foreach(Dwarf d in player.Dwarfs)
			{
				int foodForThisDwarf = d.isBaby ? 1 : foodPerDwarf;
				for(int i = 0; i < foodForThisDwarf; i++)
				{
					if(player.Resources[Resource.Type.Food].Amount>0)
					{
						FoodAndStarvation.Add(new ResourceItem(Resource.Type.Food){ X=index*48 + (i * 13), Y=0, row = index, column = 0});
						player.Resources[Resource.Type.Food].Amount--;
					}
					else
					{
						FoodAndStarvation.Add(new ResourceItem(Resource.Type.Starvation){ X=index*48 + (i * 13), Y=0, row = index, column = 0});
					}
				}
				index++;
			}
			
			if(workingcave != null && workingcave.player == player)
			{
				MakeResourceAvailable(Resource.Type.Wood);
				MakeResourceAvailable(Resource.Type.Stone);
				MakeResourceAvailable(Resource.Type.Ore);
			}
			
			MakeResourceAvailable(Resource.Type.Food);
		}
		
		private void MakeResourceAvailable(Resource.Type resourcetype)
		{
			int availableResources = FoodAndStarvation.Count(r => r.type == resourcetype && r.row == -1 && r.column == -1);
			
			if(player.Resources[resourcetype].Amount>0 && availableResources == 0)
			{
				var availableResource = new ResourceItem(resourcetype){Y = delta_y, row = -1, column = -1};
				switch(availableResource.type)
				{
					case Resource.Type.Food:
						availableResource.X = 35 * 5;
						break;
					case Resource.Type.Wood:
						availableResource.X = 0;
						break;
					case Resource.Type.Stone:
						availableResource.X = 35;
						break;
					case Resource.Type.Ore:
						availableResource.X = 35 * 2;
						break;
				}
				FoodAndStarvation.Add(availableResource);
			}
		}
		
		public void PlaceFood(Layable n, int row, int col)
		{
			var resource = n as ResourceItem;
			
			if(resource == null)
				return;
			
			if(resource.type == Resource.Type.Starvation)
			{
				SortFood(resource.row, resource.column);
			}
			else
			{	
				if(resource.column == -1 && resource.row == -1)
				{
					//FROM RESOURCES
					if(row < 0 || row > player.Dwarfs.Count - 1 || col != 0)
					{
						//TO INVALID
						FoodAndStarvation.Remove(resource);
					}
					else
					{
						//TO FEED SLOT
						player.Resources[resource.type].Amount--;						
						int foodWorth = GetFoodWorth(resource);
						PushFood(resource, row, col);
					}
					MakeResourceAvailable(resource.type);
				}
				else
				{
					//FROM A FEED SLOT
					if(row < 0 || row > player.Dwarfs.Count - 1 || col != 0)
					{
						//TO INVALID
						FoodAndStarvation.Remove(resource);
						player.Resources[resource.type].Amount++;
						MakeResourceAvailable(resource.type);
						
						FillStarvation(resource.row, resource.column);
					}
					else
					{
						//TO FEED SLOT
						int oldRow = resource.row;
						int oldColumn = resource.column;
						
						PushFood(resource, row, col);
						
						FillStarvation(oldRow, oldColumn);
					}
				}
			}
		}
		
		private void FillStarvation(int row, int col)
		{
			var resources = FoodAndStarvation.Where(r => r.row == row && r.column == col).ToList();
			
			int totalfoodWorth = 0;
			
			foreach(ResourceItem r in resources)
			{
				totalfoodWorth += GetFoodWorth(r);
			}
				
			var eatingDwarf = player.Dwarfs[row];
			int foodRequired = eatingDwarf.isBaby ? 1 : foodPerDwarf;
			
			for(int i = 0; i < foodRequired - totalfoodWorth; i++)
			{
				var starvation = new ResourceItem(Resource.Type.Starvation);
				starvation.row = row;
				starvation.column = col;
				FoodAndStarvation.Add(starvation);
			}
			
			SortFood(row, col);
		}
		
		private void SortFood(int row, int col)
		{
			var allResources = FoodAndStarvation.Where(r => r.row == row && r.column == col).OrderBy(r => r.type).ToList();
			int counter = 0;
			foreach(ResourceItem res in allResources)
			{
				res.X = row * 48 + 13 * counter;
				res.Y = 0;
				counter++;
			}
		}
		
		private int GetFoodWorth(ResourceItem resource)
		{
			int foodWorth = 0;
			switch(resource.type)
			{
				case Resource.Type.Wood:
					foodWorth = 2;
					break;
				case Resource.Type.Stone:
					foodWorth = 2;
					break;
				case Resource.Type.Ore:
					foodWorth = 2;
					break;
				case Resource.Type.Food:
					foodWorth = 1;
					break;
				case Resource.Type.Starvation:
					foodWorth = 1;
					break;
			}
			return foodWorth;
		}
		
		private void PushFood(ResourceItem resource, int row, int col)
		{
			resource.row = row;
			resource.column = col;
			resource.X = row * 48;
			resource.Y = 0;
			
			int totalFoodWorth = GetFoodWorth(resource);
			var otherResources = FoodAndStarvation.Where(r => r.row == row && r.column == col && r != resource).OrderBy(r => r.type).ToList();
			int counter = 1;
			
			var eatingDwarf = player.Dwarfs[row];
			foreach(ResourceItem res in otherResources)
			{
				int otherFoodWorth = GetFoodWorth(res);
				
				int foodRequired = eatingDwarf.isBaby ? 1 : foodPerDwarf;
				if((totalFoodWorth + otherFoodWorth) > foodRequired)
				{
					FoodAndStarvation.Remove(res);
					if(res.type != Resource.Type.Starvation)
						player.Resources[res.type].Amount++;
				}
				else
				{
					totalFoodWorth += otherFoodWorth;
					res.X = row * 48 + 13 * counter;
					res.Y = 0;
					counter++;
				}
			}
		}
		
		public void Confirm()
		{
			int starvationtokens = FoodAndStarvation.Count(r => r.type == Resource.Type.Starvation);
			player.Resources[Resource.Type.Starvation].Amount+=starvationtokens;
			FoodAndStarvation.Clear();
			FeedingTime = false;
		}
		
		public event PropertyChangedEventHandler PropertyChanged;
	}
}
