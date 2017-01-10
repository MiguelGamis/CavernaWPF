/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 1/9/2017
 * Time: 6:15 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace CavernaWPF
{
	/// <summary>
	/// Description of ActionCardWrapper.
	/// </summary>
	public class ActionCardWrapper
	{
		public ActionCard actionCard;
		
		public int Row{ get; set;}
		
		public int Column{ get; set;}
		
		public bool FaceUp{ get; set;}
		
		public string Img{ get; set;}
		
		public ActionCardWrapper(ActionCard card)
		{
			actionCard = card;
			setImage();
		}
		
		private void setImage()
		{
			Img = String.Format("C:\\Users\\Miguel\\Desktop\\Caverna\\ActionCards\\{0}.png", actionCard.Name);
		}
	}
}
