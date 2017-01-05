/*
 * Created by SharpDevelop.
 * User: Miguel
 * Date: 12/31/2016
 * Time: 03:19
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using CavernaWPF.Resources;

namespace CavernaWPF
{
	/// <summary>
	/// Description of ResourceAccumulator.
	/// </summary>
	public class ResourceAccumulator
	{
		public Resource.Type ResourceType;
		public int StartingAmount;
		public int Accumulation;
		public int Amount;
		public double imageTopYLimit;
		public double imageBottomYLimit;
	}
}
