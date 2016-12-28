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

namespace CavernaWPF.Layable
{
	/// <summary>
	/// Description of Tile.
	/// </summary>
	public class Tile : INotifyPropertyChanged
	{
		private enum Type {Meadow, Field, Cave, Tunnel, DeepTunnel, RubyMine, MeadowField, CaveCave, CaveTunnel, OreMine};
		private double x;
    	private double y;
    	private string img;
		
		public Tile()
		{
			img = "C:\\Users\\Miguel\\Desktop\\Caverna\\MeadowField.png";
		}
		
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
	    
	    public string Img
	    {
	    	get { return img; }
	    	set
	        {
	            img = value;
	            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Img"));
	        }
	    }
		
	    private int rot;
		
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
