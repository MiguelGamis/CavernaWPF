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
using CavernaWPF.Resources;

namespace CavernaWPF
{
	/// <summary>
	/// Description of BoardTile.
	/// </summary>
	public class BoardTile
	{
		public enum Type {Forest, Field, Meadow, Cavern, Fenced};
		
		public Type state;
		
		public ObservableCollection<FarmAnimal> farmAnimals;
		
		public int farmAnimalCapacity = 0;
		
		public BoardTile twin;
		
		public List<Dog> dogs;
		
		public bool hasStable;
		
		public BoardTile()
		{
		}
		
		public bool PlaceAnimal(ref List<FarmAnimal> newFAs, BoardTile origin = null)
		{
			if(farmAnimals.Count > 0 && newFAs[0].type == farmAnimals[0].type)
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
					else if(dogs.Count > 0)
					{
						if(newFAs[0].type == FarmAnimal.Type.Sheep)
						{
							int herdCapacity = dogs.Count;
							while(farmAnimals.Count < herdCapacity && newFAs.Count > 0)
							{
								int lastIndex = newFAs.Count - 1;
								FarmAnimal fa = newFAs[lastIndex];
								newFAs.RemoveAt(lastIndex);
								farmAnimals.Add(fa);
							}
						}
					}
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
			
			return false;
		}
	}
}
