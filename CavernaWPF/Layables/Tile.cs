/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 12/27/2016
 * Time: 00:44
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CavernaWPF.Layables
{
	/// <summary>
	/// Description of Tile.
	/// </summary>
	public class Tile : Layable//, INotifyPropertyChanged
	{
		public enum Type {Meadow, Field, Cave, Tunnel, RubyMine, MeadowField, CaveCave, CaveTunnel, OreMine, Fence, BigFence, 
					DeepTunnelDummy, BigFenceDummy, CaveDummy, TunnelDummy, FieldDummy, DwellingDummy};
//		private double x;
//    	private double y;
//    	private string img;
		public bool Twin;
    	
		public Type type;
		
		public Tile(Tile.Type type)
		{
			this.type = type;
			switch(type)
			{
				case Type.Field: 
					Img = "C:\\Users\\Miguel\\Desktop\\Caverna\\Field.png";
					break;
				case Type.Cave: 
					Img = "C:\\Users\\Miguel\\Desktop\\Caverna\\Cave.png";
					break;
				case Type.Tunnel: 
					Img = "C:\\Users\\Miguel\\Desktop\\Caverna\\Tunnel.png";
					break;
				case Type.Meadow: 
					Img = "C:\\Users\\Miguel\\Desktop\\Caverna\\Meadow.png";
					break;								
				case Type.Fence: 
					Img = "C:\\Users\\Miguel\\Desktop\\Caverna\\Fence.png";
					break;
				case Type.RubyMine: 
					Img = "C:\\Users\\Miguel\\Desktop\\Caverna\\RubyMine.png";
					break;
				case Type.BigFence: 
					Img = "C:\\Users\\Miguel\\Desktop\\Caverna\\BigFence.png";
					break;
				case Type.CaveTunnel: 
					Img = "C:\\Users\\Miguel\\Desktop\\Caverna\\CaveTunnel.png";
					break;
				case Type.CaveCave: 
					Img = "C:\\Users\\Miguel\\Desktop\\Caverna\\CaveCave.png";
					break;
				case Type.MeadowField: 
					Img = "C:\\Users\\Miguel\\Desktop\\Caverna\\MeadowField.png";
					break;								
				case Type.OreMine: 
					Img = "C:\\Users\\Miguel\\Desktop\\Caverna\\OreMine.png";
					break;
				default: break;
			}
			switch(type)
			{
				case Type.Field: 
				case Type.Cave: 
				case Type.Tunnel: 
				case Type.Meadow: 							
				case Type.Fence: 
				case Type.RubyMine: 
					Twin = false;
					Width = 70;
					break;
				case Type.BigFence: 
				case Type.CaveTunnel: 
				case Type.CaveCave: 
				case Type.MeadowField: 								
				case Type.OreMine: 
					Twin = true;
					Width = 140;
					break;
			}
			Height = 70;
		}
		
//		public double Y
//	    {
//	        get { return y; }
//	        set
//	        {
//	            y = value;
//	            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Y"));
//	        }
//	    }
//	
//	    public double X
//	    {
//	        get { return x; }
//	        set
//	        {
//	            x = value;
//	            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("X"));
//	        }
//	    }
//	    
//	    public string Img
//	    {
//	    	get { return img; }
//	    	set
//	        {
//	            img = value;
//	            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Img"));
//	        }
//	    }
		
	    private int rot;
		
		public int Rot
		{
			get { return rot; }
			set { 
				rot = value;
				ProperyChanged("Rot");
			}
		}
	    		
		public void Rotate()
		{
			Rot = (Rot+90)%360 ;
		}
		
		public List<Layable> occupants = new List<Layable>();
	    
//	    public event PropertyChangedEventHandler PropertyChanged;
	}
}
