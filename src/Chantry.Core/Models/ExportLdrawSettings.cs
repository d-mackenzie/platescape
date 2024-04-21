using TeethInc.Chantry.Core.Ldraw;

namespace TeethInc.Chantry.Core.Models
{
	public class ExportLdrawSettings : ExportSettings
	{
		public LdPart Baseplate { get; set; }

		public LdPart Element { get; set; }

		public bool IncludeBaseplate { get; set; }
	}
}
