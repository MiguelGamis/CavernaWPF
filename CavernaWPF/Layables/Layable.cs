/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 01/07/2017
 * Time: 01:33
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;

namespace CavernaWPF.Layables
{
	/// <summary>
	/// Description of Layable.
	/// </summary>
	public class Layable: INotifyPropertyChanged
	{
		private double x;
    	private double y;
    	private string img;
		
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
		
	    private double height;
	    private double width;
	    
	    public double Height
	    {
	   		get { return height; }
	    	set
	        {
	            height = value;
	            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Height"));
	        }
	    }
	    
	    public double Width
	    {
	   		get { return width; }
	    	set
	        {
	           width = value;
	           ProperyChanged("Width");
	        }
	    }
	    
	    public int row;
	    public int column;
	    
	    private bool locked;
	    public bool Locked
	    {
	   		get { return locked; }
	    	set
	        {
	           locked = value;
	           ProperyChanged("Locked");
	        }
	    }
	    
	    protected void ProperyChanged(string Property)
	    {
	    	if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(Property));
	    }
	    
	    public event PropertyChangedEventHandler PropertyChanged;
	}
}
