using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Ldraw;
using TeethInc.Chantry.Core.Models;

namespace TeethInc.Chantry.Core.Exporters
{
	public class LdrawExporter : IExporter
	{
		private readonly ExportLdrawSettings _exportLdrawSettings;

		public LdrawExporter(ExportLdrawSettings exportLdrawSettings)
		{
			_exportLdrawSettings = exportLdrawSettings;
		}

		public void Export(Mosaic mosaic, Stream stream)
		{
			var ldFile = new LdFile();

			ldFile.Author = "Chantry";

			for (int x = 0; x <= mosaic.Colors.GetUpperBound(0); x++)
			{
				for (int y = 0; y <= mosaic.Colors.GetUpperBound(1); y++)
				{
					//ldFile.Add(mosaic.Part, mosaic.Colors[x, y], x * 20, 0, y * -20);
				}
			}

			using (TextWriter tw = new StreamWriter(stream))
			{
				foreach (string line in ldFile.AsEnumerable())
				{
					tw.WriteLine(line);
				}
			}
		}
	}
}
