using TeethInc.Chantry.Core.Ldraw;

namespace TeethInc.Chantry.Core.Models
{
	public class LdrawExportSettings : ExportSettings
	{
		public LdPart Baseplate { get; set; }

		public LdPart Element { get; set; }

		public bool IncludeBaseplate { get; set; }

		public override string DefaultExtension => "ldr";
	}
}
