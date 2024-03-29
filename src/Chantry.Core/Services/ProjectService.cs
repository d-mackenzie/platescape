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
			switch (Path.GetExtension(filename))
			{
				case "json":

					string json = File.ReadAllText(filename);
					return Project.Deserialize(json);

				default:

					var absoluteFilename = Path.GetFullPath(filename);

					if (!File.Exists(absoluteFilename))
						throw new FileNotFoundException($"Cannot find file {absoluteFilename}");

					var project = new Project()
					{
						Name = Path.GetFileNameWithoutExtension(absoluteFilename),
						Source = new ScaledImageSource(320)
						{
							Image = SKBitmap.Decode(absoluteFilename)
						}
					};

					project.Filters.Add(new BrightnessContrastFilter());

					return project;
			}
		}
	}
}
