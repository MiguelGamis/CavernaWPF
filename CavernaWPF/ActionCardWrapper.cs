/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 1/9/2017
 * Time: 6:15 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;

namespace CavernaWPF
{
	/// <summary>
	/// Description of ActionCardWrapper.
	/// </summary>
	public class ActionCardWrapper : INotifyPropertyChanged
	{
		public ActionCard actionCard;
		
		public bool occupied;
		
		public int Row{ get; set;}
		
		public int Column{ get; set;}
		
		private bool hidden;
		
		public bool Hidden
		{ 
			get { return hidden; }
			set
			{
				hidden = value;
				if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Hidden"));
			}
		}
		
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
		
		public event PropertyChangedEventHandler PropertyChanged;
	}
}
