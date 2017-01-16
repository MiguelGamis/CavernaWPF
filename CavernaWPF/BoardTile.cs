/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 12/29/2016
 * Time: 00:29
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CavernaWPF.Layables;
using CavernaWPF.Resources;
using System.Linq;

namespace CavernaWPF
{
	/// <summary>
	/// Description of BoardTile.
	/// </summary>
	public class BoardTile
	{
		public enum Type {Forest, Field, Meadow, Rock, Cave, Tunnel, DeepTunnel, Fenced, Furnished, Dwelling, OreMine, RubyMine};
		
		public Type state;
		
		public ObservableCollection<FarmAnimal> farmAnimals = new ObservableCollection<FarmAnimal>();
		
		public int farmAnimalCapacity = 0;
		
		public BoardTile twin;
		
		public int dogs;
		
		public bool hasStable;
		
		public Resource.Type bonusResource;
		
		public int bonusResourceAmount;
		
		public int Row {get; set;}
		
		public int Column {get; set;}
		
		public BoardTile()
		{
		}
		
		public bool PlaceAnimal(ref List<FarmAnimal> newFAs, BoardTile origin = null)
		{
//			if(newFAs.All(fa => fa.row == ref newFAs[0].row && fa.column == ref newFAs[0].column))
//				throw new Exception();
			
			if(newFAs.Count == 0)
				return false;
			
			if(farmAnimals.Count > 0 && newFAs[0].type == farmAnimals[0].type)
			{
				
			}
			else if(farmAnimals.Count == 0)
			{
				if(state == Type.Fenced)
				{
					if(twin != null)
					{
						return twin.PlaceAnimal(ref newFAs, origin);
					}
					
					while(farmAnimals.Count < farmAnimalCapacity && newFAs.Count > 0)
					{
						int lastIndex = newFAs.Count - 1;
						FarmAnimal fa = newFAs[lastIndex];
						newFAs.RemoveAt(lastIndex);
						farmAnimals.Add(fa);
					}
					return true;
				}
				else if(state == Type.Meadow)
				{
					if(hasStable)
					{
						while(farmAnimals.Count < farmAnimalCapacity && newFAs.Count > 0)
						{
							int lastIndex = newFAs.Count - 1;
							FarmAnimal fa = newFAs[lastIndex];
							newFAs.RemoveAt(lastIndex);
							farmAnimals.Add(fa);
						}
					}
					else if(dogs > 0)
					{
						if(newFAs[0].type == FarmAnimal.Type.Sheep)
						{
							int herdCapacity = dogs;
							while(farmAnimals.Count < herdCapacity && newFAs.Count > 0)
							{
								int lastIndex = newFAs.Count - 1;
								FarmAnimal fa = newFAs[lastIndex];
								newFAs.RemoveAt(lastIndex);
								farmAnimals.Add(fa);
							}
						}
					}
					return true;
				}
				else if(state == Type.Forest)
				{
					if(hasStable)
					{
						if(newFAs[0].type == FarmAnimal.Type.Boar)
						{
							while(farmAnimals.Count < farmAnimalCapacity && newFAs.Count > 0)
							{
								int lastIndex = newFAs.Count - 1;
								FarmAnimal fa = newFAs[lastIndex];
								newFAs.RemoveAt(lastIndex);
								farmAnimals.Add(fa);
							}
						}
					}
				}
			}
			else if(newFAs[0].type != farmAnimals[0].type)
			{
			}
			
			return false;
		}
				
		public bool HasRoom(ref List<FarmAnimal> newFAs, BoardTile origin = null)
		{
			return false;
		}
	}
}
