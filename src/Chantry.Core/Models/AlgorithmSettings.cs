using Newtonsoft.Json;
using System.Linq;
using TeethInc.Chantry.Core.Algorithms;
using TeethInc.Chantry.Core.Ldraw;
using TeethInc.Chantry.Core.Services;

namespace TeethInc.Chantry.Core.Models
{
	public class AlgorithmSettings
	{
		[JsonIgnore]
		public LdColor[] AllowedColors { get; set; }

		public int[] AllowedColorNumbers
		{
			get
			{
				return AllowedColors.Select(x => x.Number).ToArray();
			}
			set
			{
				AllowedColors = LdrawService.GetColors(value).ToArray();
			}
		}

		public Algorithm Algorithm { get; set; }
	}
}
