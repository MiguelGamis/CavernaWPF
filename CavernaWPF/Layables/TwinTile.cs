/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 12/27/2016
 * Time: 00:44
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;

namespace CavernaWPF.Layables
{
	/// <summary>
	/// Description of TwinTile.
	/// </summary>
	public class TwinTile : /*Tile,*/ INotifyPropertyChanged
	{
		public enum Rotation {None = 0, Once = 1, Twice = 2, Thrice = 3};
		
		private int rot;
		
		public TwinTile(Tile.Type type)
		{
		}
		
		public int Rot
		{
			get { return rot; }
			set { 
				rot = value;
				if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Rot"));
			}
		}
		
		public void Rotate()
		{
			Rot = (Rot+90)%360 ;
		}
		
		public event PropertyChangedEventHandler PropertyChanged;
	}
}
