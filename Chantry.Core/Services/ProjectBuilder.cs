using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Extensions;
using TeethInc.Chantry.Core.Filters;
using TeethInc.Chantry.Core.Models;
using TeethInc.Chantry.Core.Sources;

namespace TeethInc.Chantry.Core.Services
{
	public class ProjectBuilder
	{
        private string _filename;

        private SKSizeI _maximumSize;

        private bool _withDefaultFilters;

        public ProjectBuilder WithFilename(string filename)
        {
            _filename = filename;
            return this;
        }

        public ProjectBuilder WithMaximumSize(int maximumSize)
        {
            _maximumSize = new SKSizeI(maximumSize, maximumSize);
            return this;
        }

        public ProjectBuilder WithDefaultFilters()
        {
            _withDefaultFilters = true;
            return this;
        }

        public Project Build()
        {
            var project = new Project()
            {
                Name = Path.GetFileNameWithoutExtension(_filename),
                Source = new ImageSource()
                {
                    Image = GetScaledSourceImage(SKBitmap.Decode(_filename))
                }
            };

            if (_withDefaultFilters)
            {
                project.Filters.Add(new BrightnessContrastFilter());
            }

            return project;
        }

        private SKBitmap GetScaledSourceImage(SKBitmap unscaledImage)
        {
            return unscaledImage.Resize(unscaledImage.Info.Size.GetSizeToFit(_maximumSize), SKFilterQuality.High);
        }
    }
}
