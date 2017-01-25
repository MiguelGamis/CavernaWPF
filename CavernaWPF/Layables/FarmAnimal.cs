/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 12/29/2016
 * Time: 00:47
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;

namespace CavernaWPF.Layables
{
	/// <summary>
	/// Description of Animal.
	/// </summary>
	public class FarmAnimal : Layable//, INotifyPropertyChanged
	{
		public enum Type {Sheep, Donkey, Boar, Cow};
		
		public Type type
		{
			get;
			set;
		}
		
		public FarmAnimal(Type type)
		{
			this.type = type;
			switch(type)
			{
				case Type.Sheep:
					Img = "C:\\Users\\Miguel\\Desktop\\Caverna\\Sheep.png"; 
					break;
				case Type.Donkey:
					Img = "C:\\Users\\Miguel\\Desktop\\Caverna\\Donkey.png"; 
					break;
				case Type.Boar:
					Img = "C:\\Users\\Miguel\\Desktop\\Caverna\\Boar.png"; 
					break;
				case Type.Cow:
					Img = "C:\\Users\\Miguel\\Desktop\\Caverna\\Cow.png"; 
					break;
			}
			Width=35;
			Height=35;
			Z = 7;
		}
		
//		private double y;
//		
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
//		private double x;
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
//	    private string img;
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
//	    
//	    public event PropertyChangedEventHandler PropertyChanged;
	}
}
