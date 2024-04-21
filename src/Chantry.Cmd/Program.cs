using TeethInc.Chantry.Core.Exporters;
using TeethInc.Chantry.Core.Models;
using TeethInc.Chantry.Core.Services;

namespace Chantry.Cmd
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var project = ProjectService.Load("c:\\temp\\eric-avatar.jpg");

			var mosaic = project.Mosaic;

			var pngExporter = new PngExporter(new ExportPngSettings()
			{
				PixelsPerStud = 50,
				DrawStuds = false
			});

			using (var stream = new FileStream("c:\\temp\\eric-avatar-mosaic.png", FileMode.OpenOrCreate))
			{
				pngExporter.Export(mosaic, stream);
			}
		}
	}
}
