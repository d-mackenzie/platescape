using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Models;

namespace TeethInc.Chantry.Core.Exporters
{
	public interface IExporter
	{
		string DefaultExtension { get; }

		bool IsValid { get; }

		void Export(Mosaic mosaic, Stream stream);
	}
}
