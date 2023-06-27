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
	public class ProjectBuilder
	{
        private string _filename;

        private SKSize _maximumSize;
        private SKSize _minimumSize;

        private bool _withDefaultFilters;
        
        public ProjectBuilder FromFile(string filename)
        {
            _filename = filename;
            return this;
        }

        public ProjectBuilder WithMaximumSize(SKSize maximumSize)
        {
            _maximumSize = maximumSize;
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
                    Image = SKBitmap.Decode(_filename)
                }
            };

            if (_withDefaultFilters)
            {
                project.Filters.Add(new BrightnessContrastFilter());
            }

            return project;
        }
    }
}
