using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Sources;
using TeethInc.Chantry.Core.Extensions;
using TeethInc.Chantry.Core.Converters;
using Newtonsoft.Json;
using TeethInc.Chantry.Core.Models;
using SkiaSharp;
using System.IO;
using TeethInc.Chantry.Core.Filters;

namespace TeethInc.Chantry.Core.Services
{
    public static class ProjectService
    {

		public static Project CreateSimpleProject(string filename)
		{
			var project = new Project()
			{
				Name = Path.GetFileNameWithoutExtension(filename),
				Source = new ImageSource()
				{
					Image = SKBitmap.Decode(filename)
				}
			};

			project.Filters.Add(new BrightnessContrastFilter());

			return project;
		}
	}
}
