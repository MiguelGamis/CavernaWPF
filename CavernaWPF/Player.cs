/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 12/30/2016
 * Time: 02:47
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.ObjectModel;

namespace CavernaWPF
{
	/// <summary>
	/// Description of Player.
	/// </summary>
	public class Player
	{
		public Player()
		{
		}
		
		public TownContext town;
		
		public ObservableCollection<Dwarf> Dwarfs = new ObservableCollection<Dwarf>();
		
		public string name;
		
		public string color;
	}
}
