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
		public enum Type {Meadow, Field, Cave, Tunnel, RubyMine, MeadowField, CaveCave, CaveTunnel, OreMine, Fence, BigFence, FurnishingTile, Dwelling,
					DeepTunnelDummy, BigFenceDummy, CaveDummy, TunnelDummy, FieldDummy, DwellingDummy};
//		private double x;
//    	private double y;
//    	private string img;
		public bool Twin;
    	
		public Type type
		{
			get;
			set;
		}
		
		public Tile(Tile.Type type)
		{
			this.type = type;
			switch(type)
			{
				case Type.Field:
					Z = 0;
					Img = "pack://application:,,,/Images/Tiles/Field.png";
					break;
				case Type.Cave:
					Z = 0;
					Img = "pack://application:,,,/Images/Tiles/Cave.png";
					break;
				case Type.Tunnel:
					Z = 0;					
					Img = "pack://application:,,,/Images/Tiles/Tunnel.png";
					break;
				case Type.Meadow:
					Z = 0;					
					Img = "pack://application:,,,/Images/Tiles/Meadow.png";
					break;								
				case Type.Fence:
					Z = 1;					
					Img = "pack://application:,,,/Images/Tiles/Fence.png";
					break;
				case Type.RubyMine:
					Z = 2;
					Img = "pack://application:,,,/Images/Tiles/RubyMine.png";
					break;
				case Type.BigFence:
					Z = 1;					
					Img = "pack://application:,,,/Images/Tiles/BigFence.png";
					twinTile = new Tile(Type.BigFenceDummy);
					twinTile.twinTile = this;
					break;
				case Type.CaveTunnel:
					Z = 0;
					Img = "pack://application:,,,/Images/Tiles/CaveTunnel.png";
					twinTile = new Tile(Type.TunnelDummy);
					twinTile.twinTile = this;
					break;
				case Type.CaveCave:
					Z = 0;
					Img = "pack://application:,,,/Images/Tiles/CaveCave.png";
					twinTile = new Tile(Type.CaveDummy);
					twinTile.twinTile = this;
					break;
				case Type.MeadowField:
					Z = 0;
					Img = "pack://application:,,,/Images/Tiles/MeadowField.png";
					twinTile = new Tile(Type.FieldDummy);
					twinTile.twinTile = this;
					break;								
				case Type.OreMine:
					Z = 1;
					Img = "pack://application:,,,/Images/Tiles/OreMine.png";
					twinTile = new Tile(Type.DeepTunnelDummy);
					twinTile.twinTile = this;
					break;
				case Type.Dwelling:
					Z = 1;
					break;
				case Type.FurnishingTile:
					Z = 1;
					break;
				case Type.CaveDummy:
				case Type.FieldDummy:
				case Type.TunnelDummy:
					Z = 0;
					break;
				case Type.BigFenceDummy:
				case Type.DwellingDummy:
				case Type.DeepTunnelDummy:
					Z = 1;
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
				case Type.Dwelling:
				case Type.FurnishingTile:
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
		
		private Tile twinTile;
		
		public Tile GetTwinTile()
		{
			return twinTile;
		}
	}
}
