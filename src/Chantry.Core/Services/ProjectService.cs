using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Filters;
using TeethInc.Chantry.Core.Models;
using TeethInc.Chantry.Core.Sources;

namespace TeethInc.Chantry.Core.Services
{
	public static class ProjectService
	{
		public static Project Load(string filename)
		{
			if (!File.Exists(filename))
				throw new FileNotFoundException($"Cannot find file {filename}");

			try
			{
				switch (Path.GetExtension(filename))
				{
					case ".json":

						string json = File.ReadAllText(filename);
						return Project.Deserialize(json);

					default:

						SKBitmap image = SKBitmap.Decode(filename);

						if (image is null)
							throw new Exception($"'{filename}' is not in a recognisable image format.");

						var project = new Project()
						{
							Name = Path.GetFileNameWithoutExtension(filename),
							Source = new ScaledImageSource(320)
							{
								Image = image,
								OriginalFilename = filename
							}
						};

						project.Filters.Add(new BrightnessContrastFilter());
						project.PngExporter.PopulateOutputSettings(project.Source);

						return project;
				}
			}
			catch (Exception ex)
			{
				throw;
			}
		}
	}
}
