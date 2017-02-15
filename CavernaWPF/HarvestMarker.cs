/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 01/12/2017
 * Time: 21:44
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;

namespace CavernaWPF
{
	/// <summary>
	/// Description of HarvestMarker.
	/// </summary>
	public class HarvestMarker : INotifyPropertyChanged
	{
		public enum Type {Pay1FoodPerDwarf, NoHarvest, Harvest, QuestionMark}
		
		private double row;
    	private double column;
    	private bool hidden;
    	private Type _type;
    	
		public HarvestMarker(Type t)
		{
			_type = t;
			switch(t)
			{
				case Type.Harvest:
				case Type.QuestionMark:
					Width = 32;
					break;
				case Type.NoHarvest:
				case Type.Pay1FoodPerDwarf:
					Width = 64;
					break;
			}
			Height = 32;
		}
		
		public double Height
		{
			get;
			set;
		}
		
		public double Width
		{
			get;
			set;
		}
		
		//TODO: Some of these properties don't need to notify if they are changed. Only Hidden needs it
		public double Row
	    {
	        get { return row; }
	        set
	        {
	            row = value;
	            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Row"));
	        }
	    }
	
	    public double Column
	    {
	        get { return column; }
	        set
	        {
	            column = value;
	            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Column"));
	        }
	    }
	    
	    public bool Hidden {
	    	get { return hidden; }
	    	set
	    	{
	    		hidden = value;
	    		if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Hidden"));
	    	}
	    }
	    
	    public Type type {
	    	get { return _type; }
	    	set
	    	{
	    		_type = value;
	    		if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Type"));
	    	}
	    }
	    
	    public event PropertyChangedEventHandler PropertyChanged;
	}
}
