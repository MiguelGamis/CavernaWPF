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
    	
		public HarvestMarker(Type t)
		{
			type = t;
		}
		
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
	    
	    public bool Hidden {get; set;}
	    
	    public Type type {get; set;}
	    
	    public event PropertyChangedEventHandler PropertyChanged;
	}
}
