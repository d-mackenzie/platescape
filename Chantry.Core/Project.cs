using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Filters;
using TeethInc.Chantry.Core.Ldraw;
using TeethInc.Chantry.Core.MosaicAlgorithms;
using TeethInc.Chantry.Core.Services;
using TeethInc.Chantry.Core.Sources;

namespace TeethInc.Chantry.Core
{
    public class Project
    {
        private ISource m_source;
        private FilterService m_filterService;
        private MosaicService m_mosaicService;

        public string Name { get; set; }

        // source properties.

        public ISource Source
        {
            get { return m_source; }
            set { m_source = value; }
        }

        // filter properties.

        public List<Filter> Filters => FilterService.Filters;
        public Bitmap UnfilteredImage => FilterService.GetUnfilteredImage();
        public Bitmap FilteredImage => FilterService.GetFilteredImage();

        // mosaic properties.

        public LdPart Baseplate
        {
            get { return MosaicService.Baseplate; }
            set { MosaicService.Baseplate = value; }
        }

        public LdPart Element
        {
            get { return MosaicService.Element; }
            set { MosaicService.Element = value; }
        }

        public Size BaseplateExtent
        {
            get { return MosaicService.BaseplateExtent; }
            set { MosaicService.BaseplateExtent = value; }
        }

        public Size ElementExtent => MosaicService.ElementExtent;

        public List<LdColor> AllowedColors
        {
            get { return MosaicService.AllowedColors; }
            set { MosaicService.AllowedColors = value; }
        }

        public Mosaic Mosaic => MosaicService.GetMosaic(FilteredImage, new FloydSteinberg());

        // private properties.

        private FilterService FilterService
        {
            get
            {
                if (m_filterService is null)
                    m_filterService = new FilterService(Source);

                return m_filterService;
            }
        }

        private MosaicService MosaicService
        {
            get
            {
                if (m_mosaicService is null)
                    m_mosaicService = new MosaicService();

                return m_mosaicService;
            }
        }
    }
}
