﻿/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 12/23/2016
 * Time: 12:57
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;

namespace CavernaWPF
{
	/// <summary>
	/// Description of Class1.
	/// </summary>
	public class Dwarf : INotifyPropertyChanged
	{
		private int level;
		private double x;
    	private double y;
		public Player player;
    	
		public Dwarf()
		{
			
		}
		
		public void levelUp(int exp = 1)
		{
			level += exp;
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
	    
	    public event PropertyChangedEventHandler PropertyChanged;
	}
}