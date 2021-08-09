using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Filters;
using TeethInc.Chantry.Core.Services;
using TeethInc.Chantry.Core.Sources;

namespace TeethInc.Chantry.Core
{
    public class Project
    {
        private ISource m_source;
        private FilterService m_filterService;

        public string Name { get; set; }

        public ISource Source
        {
            get { return m_source; }
            set { m_source = value; }
        }

        public List<Filter> Filters
        {
            get { return FilterService.Filters; }
        }

        public Bitmap UnfilteredImage
        {
            get { return FilterService.GetUnfilteredImage(); }
        }

        public Bitmap FilteredImage
        {
            get { return FilterService.GetFilteredImage(); }
        }

        private FilterService FilterService
        {
            get
            {
                if (m_filterService is null)
                    m_filterService = new FilterService(Source);

                return m_filterService;
            }
        }
    }
}
