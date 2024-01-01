using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Algorithms;
using TeethInc.Chantry.Core.Services;

namespace TeethInc.Chantry.Core.Models
{
	public class AlgorithmSettings
	{
		public int[] AllowedColors { get; set; }

		public IAlgorithm Algorithm { get; set; }
	}
}
