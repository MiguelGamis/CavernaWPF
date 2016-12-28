/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 12/28/2016
 * Time: 03:41
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.ObjectModel;

namespace CavernaWPF
{
	/// <summary>
	/// Description of ActionBoard.
	/// </summary>
	public sealed class ActionBoardContext
	{
		private static ActionBoardContext instance = new ActionBoardContext();
		
		public static ActionBoardContext Instance {
			get {
				return instance;
			}
		}
		
		private ObservableCollection<ActionCard> actioncards;
		
		private ActionBoardContext()
		{
			object f = actioncards[0];
		}
	}
}
